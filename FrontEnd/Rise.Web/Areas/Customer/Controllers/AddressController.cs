using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Rise.Web.Models;
using Rise.Web.Services;
using Rise.Web.Services.IServices;
using Rise.Web.ViewModels;
using System.Data;

namespace Rise.Web.Areas.Customer.Controllers
{
	[Area("Customer")]
	public class AddressController : Controller
	{

		private readonly IAddressService _addressService;
		public IWebHostEnvironment _environment;
		public AddressController(IAddressService addressService, IWebHostEnvironment environment)
		{
			_addressService = addressService;
			_environment = environment;
		}

		[Authorize(Roles = "Customer")]
		public async Task<IActionResult> MyAddress(string userId)
		{
			List<AddressDto> model = new();
			var accessToken = await HttpContext.GetTokenAsync("access_token");
			var response = await _addressService.GetAddressByUserIdAsnyc<ResponseDto>(userId, accessToken);
			if (response != null && response.IsSuccess)
			{
				model = JsonConvert.DeserializeObject<List<AddressDto>>(Convert.ToString(response.Result));
			}
			return View(model);
		}

		[Authorize(Roles = "Customer")]
		public async Task<IActionResult> CreateAddress()
		{


			return View();
		}

		[HttpPost]
		[Authorize(Roles = "Customer")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateAddress(AddAddressViewModel model)
		{
			var UserId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;
			//if (ModelState.IsValid)
			//  {
			var accessToken = await HttpContext.GetTokenAsync("access_token");


			AddressDto addressDto = new AddressDto
			{
				Id = model.AddressId,
				UserId = UserId,
				AddressName = model.AddressName,
				Address1 = model.Address1,
				Address2 = model.Address2,
				ZipCode = model.ZipCode,
				CityName = model.CityName
			};

			var response = await _addressService.CreateAddressAsync<ResponseDto>(addressDto, accessToken);
			if (response != null && response.IsSuccess)
			{
				return RedirectToAction("Index","Home");
			}
			//}
			return View(addressDto);
		}



		[Authorize(Roles = "Customer")]
		public async Task<IActionResult> AddressDelete(int AddressId)
		{
			if (AddressId == null)
			{
				return NotFound();
			}


			var accessToken = await HttpContext.GetTokenAsync("access_token");
			var response = await _addressService.DeleteAddressAsync<ResponseDto>(AddressId, accessToken);

			if (response.IsSuccess)
			{
				return RedirectToAction("Index", "Home");
			}
			return RedirectToAction("Index", "Home");
		}

	}
}
