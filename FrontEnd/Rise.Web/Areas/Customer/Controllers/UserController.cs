using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rise.Services.Identity.DbContexts;
using Rise.Services.Identity.Models;
using Rise.Web.ViewModels;
using System.Data;

namespace Rise.Web.Areas.Customer.Controllers
{
	[Area("Customer")]
	public class UserController : Controller
	{
		private readonly ApplicationDbContext _context;
		public UserController(ApplicationDbContext context)
		{
			_context = context;


		}

		[HttpGet]
		[Authorize(Roles = "Customer")]
		public async Task<IActionResult> ViewProfile(string UserId)
		{
			var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == UserId);
			if (user == null)
			{
				return RedirectToAction("Index", "Home");
			}

			var applicationUser = new ApplicationUser()
			{
				Id = user.Id,
				FirstName = user.FirstName,
				LastName = user.LastName,
				Email = user.Email,
				PhoneNumber = user.PhoneNumber,
				UserName = user.UserName,
				AboutMe = user.AboutMe,
				
			};
			return View(applicationUser);
		}

		[HttpGet]
		[Authorize]
		public async Task<IActionResult> EditProfile(string UserId)
		{
			var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == UserId);

			if (user == null)
			{
				return View("Error");
			}

			var applicationUser = new EditProfileViewModel()
			{
				
				FirstName = user.FirstName,
				LastName = user.LastName,
				PhoneNumber = user.PhoneNumber,
				Email = user.Email,
				UserName = user.UserName,
				AboutMe = user.AboutMe,
				
			};
			return View(applicationUser);
		}

		[HttpPost]
		[Authorize]
		public async Task<IActionResult> EditProfile(EditProfileViewModel editVM)
		{
			var UserId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;

			var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == UserId);
			//var user = await _userManager.GetUserAsync(User);
			if (!ModelState.IsValid)
			{
				ModelState.AddModelError("", "Failed to edit profile");
				return View("ViewProfile", UserId);
			}

			if (editVM == null)
			{
				return View("Error");
			}

			user.FirstName = editVM.FirstName;
			user.LastName = editVM.LastName;
			user.PhoneNumber = editVM.PhoneNumber;
			user.Email = editVM.Email;
			user.UserName = editVM.UserName;
			user.AboutMe = editVM.AboutMe;

			await _context.SaveChangesAsync();
			

			return RedirectToAction("ViewProfile", "User", new { user.Id });

		}
	}
}
