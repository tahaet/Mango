using AutoMapper;
using Mango.MessageBus;
using Mango.Services.OrderAPI.Data;
using Mango.Services.OrderAPI.Models;
using Mango.Services.OrderAPI.Models.Dto;
using Mango.Services.OrderAPI.Service.IService;
using Mango.Services.OrderAPI.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;
using Stripe.Checkout;


namespace Mango.Services.OrderAPI.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderAPIController : ControllerBase
    {
        protected ResponseDto _response;
        private IMapper _mapper;
        private readonly AppDbContext _db;
        private readonly IProductService _productService;
        private readonly IMessageBus _messageBus;
        private readonly IConfiguration _configuration;
        public OrderAPIController(AppDbContext db,
            IProductService productService, IMapper mapper, IConfiguration configuration, IMessageBus messageBus)
        {
            _db = db;
            this._response = new ResponseDto();
            _productService = productService;
            _mapper = mapper;
            _configuration = configuration;
            _messageBus = messageBus;
        }

        [Authorize]
        [HttpPost("CreateOrder")]
        public async Task<ResponseDto> CreateOrder([FromBody] CartDto cartDto)
        {
            try
            {
                OrderHeaderDto orderHeaderDto = _mapper.Map<OrderHeaderDto>(cartDto.CartHeader);
                orderHeaderDto.OrderTime = DateTime.Now;
                orderHeaderDto.Status = SD.Status_Pending;
                orderHeaderDto.OrderDetails = _mapper.Map<IEnumerable<OrderDetailsDto>>(cartDto.CartDetails);
                
                OrderHeader orderCreated = _db.OrderHeaders.Add(_mapper.Map<OrderHeader>(orderHeaderDto)).Entity;
                await _db.SaveChangesAsync();

                orderHeaderDto.OrderHeaderId = orderCreated.OrderHeaderId;
                _response.Result = orderHeaderDto;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [Authorize]
        [HttpGet("GetOrder/{id:int}")]
        public async Task<ResponseDto?> Get(int id)
        {
            try
            {
                OrderHeader orderHeader = await _db.OrderHeaders.Include(u => u.OrderDetails).FirstOrDefaultAsync(u => u.OrderHeaderId == id);
                _response.Result = _mapper.Map<OrderHeaderDto>(orderHeader);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        [Authorize]
        [HttpGet("GetOrders")]
        public async Task<ResponseDto?> Get(string? userId = "")
        {
            try
            {
                IEnumerable<OrderHeader> orders;
                if (User.IsInRole(SD.RoleAdmin))
                {
                    orders = await _db.OrderHeaders.Include(o=>o.OrderDetails).OrderByDescending(o=>o.OrderHeaderId).ToListAsync();
                }
                else
                {
                    orders = await _db.OrderHeaders.Include(o=>o.OrderDetails).Where(o=>o.UserId==userId).
                        OrderByDescending(o => o.OrderHeaderId).ToListAsync();
                }
                _response.Result = _mapper.Map<IEnumerable<OrderHeaderDto>>(orders);
            
            }
            catch (Exception ex)
            {
                _response.Message=ex.Message;
                _response.IsSuccess = false;
            }
            return _response;
        }

        [Authorize]
        [HttpPost("UpdateOrderStatus/{orderId:int}")]
        public async Task<ResponseDto> UpdateOrderStatus(int orderId, [FromBody] string newStatus)
        {
            try
            {
                OrderHeader orderHeader = await _db.OrderHeaders.FirstOrDefaultAsync(o=>o.OrderHeaderId == orderId);
                if (orderHeader != null)
                {
                    if (newStatus == SD.Status_Cancelled)
                    {
                        //refund logic
                        var options = new RefundCreateOptions
                        {
                            Reason = RefundReasons.RequestedByCustomer,
                            PaymentIntent = orderHeader.PaymentIntentId
                        };

                        var service = new RefundService();
                        Refund refund = service.Create(options);
                    }
                    orderHeader.Status = newStatus;
                    await _db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }

            return _response;
        }

        [Authorize]
        [HttpPost("CreateStripeSession")]
        public async Task <ResponseDto> CreateStripeSession([FromBody] StripeRequestDto stripeRequestDto)
        {
            try
            {
                var options = new SessionCreateOptions
                {
                    SuccessUrl = stripeRequestDto.ApprovedUrl,
                    CancelUrl = stripeRequestDto.CancelUrl,
                    LineItems = new List<SessionLineItemOptions>(),
                    Mode = "payment",
                };
                var discountsObj = new List<SessionDiscountOptions>()
                {
                    new SessionDiscountOptions()
                    {
                        Coupon=stripeRequestDto.OrderHeader.CouponCode
                    }
                };
                foreach(var item in stripeRequestDto.OrderHeader.OrderDetails)
                {
                    var sessionLineItem = new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long)(item.Price * 100), // $20.99 -> 2099
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.Product.Name
                            }
                        },
                        Quantity = item.Count
                    };

                    options.LineItems.Add(sessionLineItem);
                }

                if (stripeRequestDto.OrderHeader.Discount > 0)
                {
                    options.Discounts = discountsObj;
                }
                var service = new SessionService();
                Session session = service.Create(options);
                stripeRequestDto.StripeSessionUrl = session.Url;
                OrderHeader orderHeader = await _db.OrderHeaders.
                    FirstOrDefaultAsync(x => x.OrderHeaderId == stripeRequestDto.OrderHeader.OrderHeaderId);
                orderHeader.StripeSessionId = session.Id;

                _db.OrderHeaders.Update(orderHeader);
                await _db.SaveChangesAsync();
                _response.Result = stripeRequestDto;

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message; 
            }
            return _response;

        }
        [Authorize]
        [HttpPost("ValidateStripeSession")]
        public async Task<ResponseDto> ValidateStripeSession( [FromBody] int orderHeaderId)
        {
            try
            {
                var orderHeader = await _db.OrderHeaders.FirstOrDefaultAsync(x=>x.OrderHeaderId == orderHeaderId);
                var service = new SessionService();
                var session = await service.GetAsync(orderHeader.StripeSessionId);
                var paymentIntentService = new PaymentIntentService();
                var paymentIntent = await paymentIntentService.GetAsync(session.PaymentIntentId);

                if(paymentIntent.Status == "succeeded")
                {
                    orderHeader.PaymentIntentId = paymentIntent.Id;
                    orderHeader.Status = SD.Status_Approved;
                    _db.Update(orderHeader);
                    await _db.SaveChangesAsync();
                    RewardDto rewardDto = new RewardDto
                    {
                        OrderId = orderHeader.OrderHeaderId,
                        RewardsActivity = Convert.ToInt32(orderHeader.OrderTotal),
                        UserId = orderHeader.UserId
                    };
                    string topicName = _configuration.GetValue<string>("TopicAndQueueNames:OrderCreatedTopic");
                    string queueName = _configuration.GetValue<string>("TopicAndQueueNames:OrderCreatedQueue");
                    await _messageBus.PublishMessage(rewardDto, topicName);
                    await _messageBus.PublishMessage(orderHeader, queueName);
                    _response.Result = _mapper.Map<OrderHeaderDto>(orderHeader);
                }
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message; 
            }
            return _response;
        }
    }
}
