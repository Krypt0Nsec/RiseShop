using Azure.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Rise.Web.Models;
using Rise.Web.Services;
using Rise.Web.Services.IServices;
using System.Data;

namespace Rise.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class OrderController : Controller
	{
		private readonly IOrderService _orderService;
		public IWebHostEnvironment _environment;
		public OrderController(IOrderService orderService, IWebHostEnvironment environment)
		{
			_orderService = orderService;
			_environment = environment;
		}

		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> OrderIndex()
		{
			List<OrderDetailsDto> list = new();
			var accessToken = await HttpContext.GetTokenAsync("access_token");
			var response = await _orderService.GetOrderDetails<ResponseDto>(accessToken);
			if (response != null && response.IsSuccess)
			{
				list = JsonConvert.DeserializeObject<List<OrderDetailsDto>>(Convert.ToString(response.Result));
			}
			return View(list);
		}

		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> HeaderById(int headerId)
		{
			OrderHeadersDto model = new();
			var accessToken = await HttpContext.GetTokenAsync("access_token");
			var response = await _orderService.GetOrderByOrderHeadersId<ResponseDto>(headerId, accessToken);
			if (response != null && response.IsSuccess)
			{
				model = JsonConvert.DeserializeObject<OrderHeadersDto>(Convert.ToString(response.Result));
			}
			return View(model);

		}


		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> OrderByUserId(string userId)
		{
			OrderDto model = new();
			var accessToken = await HttpContext.GetTokenAsync("access_token");
			var response = await _orderService.GetOrderByUserId<ResponseDto>(userId, accessToken);
			if (response != null && response.IsSuccess)
			{
				model = JsonConvert.DeserializeObject<OrderDto>(Convert.ToString(response.Result));
			}
			return View(model);
		}

	}
}
