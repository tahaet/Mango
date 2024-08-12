using Mango.Web.Models;
using Mango.Web.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace Mango.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        public async Task<IActionResult> CartIndex()
        {
            return View( await LoadCartDtoBasednLoggedInUser());
        }
        private async Task<CartDto?> LoadCartDtoBasednLoggedInUser()
        {
            var userId = User.Claims.Where(x => x.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;
            ResponseDto? response = await _cartService.GetCartByUserIdAsnyc(userId);
            if( response != null && response.IsSuccess )
            {
                return JsonConvert.DeserializeObject<CartDto?>(Convert.ToString(response.Result)!);
            }
            return new CartDto();
        }
    }
}
