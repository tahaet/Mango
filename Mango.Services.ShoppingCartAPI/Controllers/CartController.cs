using AutoMapper;
using Mango.Services.ShoppingCartAPI.Data;
using Mango.Services.ShoppingCartAPI.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.ShoppingCartAPI.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private ResponseDto _responseDto;
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public CartController(AppDbContext db,IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        [HttpPost("CartUpsert")]
        public async Task<ResponseDto> CartUpsert([FromBody] CartDto cartDto)
        {

        }
    }
}
