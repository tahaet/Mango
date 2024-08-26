using Mango.Web.Models;
using Mango.Web.Service.IService;
using Mango.Web.Utility;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Mango.Web.ViewComponents
{
	public class ShoppingCartViewComponent : ViewComponent
	{
		private readonly ICartService cartService;

		public ShoppingCartViewComponent(ICartService cartService)
		{
			this.cartService = cartService;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			var identity = (ClaimsIdentity)User.Identity;

			var userId = identity.FindFirst(x => x.Type == JwtRegisteredClaimNames.Sub)?.Value;

			if (userId is not null)
			{

				if (HttpContext.Session.GetInt32(SD.CartSession) is null)
				{
					var responseDto = await cartService.GetCartByUserIdAsnyc(userId);
					if (responseDto is not null && responseDto.IsSuccess)
					{
						HttpContext.Session.SetInt32(
							SD.CartSession, JsonConvert.DeserializeObject<CartDto>
							(Convert.ToString(responseDto.Result)).CartDetails.Count());

					}
				}
				return View(HttpContext.Session.GetInt32(SD.CartSession));

			}
			else
			{

				HttpContext.Session.Clear();
				return View(0);
			}
		}
	}
}
