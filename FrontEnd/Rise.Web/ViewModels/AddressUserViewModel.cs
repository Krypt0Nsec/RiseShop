using Rise.Web.Models;

namespace Rise.Web.ViewModels
{
	public class AddressUserViewModel
	{
		
		public string? Id { get; set; }

		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public string? PhoneNumber { get; set; }
		public string? Email { get; set; }

		public IEnumerable<AddressDto> Address { get; set; }
		
	}
}

