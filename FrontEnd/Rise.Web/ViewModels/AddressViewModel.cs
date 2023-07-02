using Rise.Web.Models;

namespace Rise.Web.ViewModels
{
	public class AddressViewModel
	{
		public AddressDto Address { get; set; }

		public IEnumerable<CityDto> City { get; set; }
	}
}
