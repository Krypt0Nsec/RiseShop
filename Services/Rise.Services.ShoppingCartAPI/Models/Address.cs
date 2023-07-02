using System.ComponentModel.DataAnnotations;

namespace Rise.Services.ShoppingCartAPI.Models
{
	public class Address
	{
		[Key]
		public int Id { get; set; }
		public string UserId { get; set; }

		public string AddressName { get; set; }
		public string Address1 { get; set; }
		public string? Address2 { get; set; }

		public string ZipCode { get; set; }

		public string CityName { get; set; }
	}
}
