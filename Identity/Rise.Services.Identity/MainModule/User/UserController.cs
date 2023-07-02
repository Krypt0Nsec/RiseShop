using Microsoft.AspNetCore.Mvc;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Duende.IdentityServer;
using Duende.IdentityServer.Events;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Stores;
using Duende.IdentityServer.Test;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using Rise.Services.Identity.Models;
using Rise.Services.Identity.MainModule.Account;
using Rise.Services.Identity.MainModule.User;

namespace IdentityServerHost.Quickstart.UI
{
    /// <summary>
    /// This sample controller implements a typical login/logout/provision workflow for local and external accounts.
    /// The login service encapsulates the interactions with the user data store. This data store is in-memory only and cannot be used for production!
    /// The interaction service provides a way for the UI to communicate with identityserver for validation and context retrieval
    /// </summary>
    [SecurityHeaders]
	[AllowAnonymous]
	public class UserController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly IIdentityServerInteractionService _interaction;
		private readonly IClientStore _clientStore;
		private readonly IAuthenticationSchemeProvider _schemeProvider;
		private readonly IEventService _events;
		private readonly IUserRepository _userRepository;

		public UserController(
			IIdentityServerInteractionService interaction,
			IClientStore clientStore,
			IAuthenticationSchemeProvider schemeProvider,
			IEventService events,
			UserManager<ApplicationUser> userManager,
			SignInManager<ApplicationUser> signInManager,
			RoleManager<IdentityRole> roleInManager,
			IUserRepository userRepository
			)
		{
			// if the TestUserStore is not in DI, then we'll just use the global users collection
			// this is where you would plug in your own custom identity management library (e.g. ASP.NET Identity)
			_interaction = interaction;
			_clientStore = clientStore;
			_schemeProvider = schemeProvider;
			_events = events;
			_roleManager = roleInManager;
			_userManager = userManager;
			_signInManager = signInManager;
			_userRepository = userRepository;
		}
		[HttpGet("users")]
		public async Task<IActionResult> Index()
		{
			var users = await _userRepository.GetAllUsers();
			List<UserViewModel> result = new List<UserViewModel>();
			foreach (var user in users)
			{
				var userViewModel = new UserViewModel()
				{
					Id = user.Id,
					MailAddress = user.Email,
					FirstName = user.FirstName,
					LastName = user.LastName,
					UserName = user.UserName,
					PhoneNumber = user.PhoneNumber

				};
				result.Add(userViewModel);
			}
			return View(result);
		}

		[HttpGet]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Detail(string id)
		{
			var user = await _userRepository.GetUserById(id);
			if (user == null)
			{
				return RedirectToAction("Index", "Users");
			}

			var userDetailViewModel = new UserDetailViewModel()
			{
				MailAddress = user.Email,
				FirstName = user.FirstName,
				LastName = user.LastName,
				UserName = user.UserName,
				PhoneNumber = user.PhoneNumber
			};
			return View(userDetailViewModel);
		}

		[HttpGet]
		[Authorize(Roles = "Admin")]

		public async Task<IActionResult> EditProfile()
		{
			var user = await _userManager.GetUserAsync(User);

			if (user == null)
			{
				return View("Error");
			}

			var editMV = new EditProfileViewModel()
			{
				MailAddress = user.Email,
				FirstName = user.FirstName,
				LastName = user.LastName,
				UserName = user.UserName,
				PhoneNumber = user.PhoneNumber
			};
			return View(editMV);
		}

		[HttpPost]
		[Authorize(Roles = "Admin")]

		public async Task<IActionResult> EditProfile(EditProfileViewModel editVM)
		{
			if (!ModelState.IsValid)
			{
				ModelState.AddModelError("", "Failed to edit profile");
				return View("EditProfile", editVM);
			}

			var user = await _userManager.GetUserAsync(User);

			if (user == null)
			{
				return View("Error");
			}

			

			user.FirstName = editVM.FirstName;
			user.LastName = editVM.LastName;
			user.UserName = editVM.UserName;
			user.PhoneNumber = editVM.PhoneNumber;
			user.Email = editVM.MailAddress;

			await _userManager.UpdateAsync(user);

			return RedirectToAction("Detail", "User", new { user.Id });
		}
	}
}