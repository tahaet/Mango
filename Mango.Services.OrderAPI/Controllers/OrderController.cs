using AutoMapper;
using Mango.MessageBus;
using Mango.Services.OrderAPI.Data;
using Mango.Services.OrderAPI.Models.Dto;
using Mango.Services.OrderAPI.Service.IService;
using Microsoft.AspNetCore.Mvc;


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
        public OrderAPIController(AppDbContext db,
            IProductService productService, IMapper mapper)
        {
            _db = db;
            this._response = new ResponseDto();
            _productService = productService;
            _mapper = mapper;
        }
    }
}
