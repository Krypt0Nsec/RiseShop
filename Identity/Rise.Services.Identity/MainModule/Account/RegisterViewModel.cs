using IdentityServerHost.Quickstart.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Rise.Services.Identity.MainModule.Account
{
    public class RegisterViewModel
    {
        [Required]
        public string Username { get; set; }

        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Email address is required")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password do not match")]
        public string ConfirmPassword { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }


        public string ReturnUrl { get; set; }
        public string RoleName { get; set; }

        public bool AllowRememberLogin { get; set; } = true;
        public bool EnableLocalLogin { get; set; } = true;

        //public IEnumerable<ExternalProvider> ExternalProviders { get; set; } = Enumerable.Empty<ExternalProvider>();
        //public IEnumerable<ExternalProvider> VisibleExternalProviders => ExternalProviders.Where(x => !String.IsNullOrWhiteSpace(x.DisplayName));

        //public bool IsExternalLoginOnly => EnableLocalLogin == true && ExternalProviders?.Count() == 1;
        //public string ExternalLoginScheme => ExternalProviders?.SingleOrDefault()?.AuthenticationScheme; /*IsExternalLoginOnly ? ExternalProviders?.SingleOrDefault()?.AuthenticationScheme : null;*/

		public IEnumerable<ExternalProvider> ExternalProviders { get; set; } = Enumerable.Empty<ExternalProvider>();
		public bool IsExternalLoginOnly => EnableLocalLogin == false && ExternalProviders?.Count() == 1;

		public string? ExternalLoginScheme => IsExternalLoginOnly ? ExternalProviders?.SingleOrDefault()?.AuthenticationScheme : null;

	}
}
