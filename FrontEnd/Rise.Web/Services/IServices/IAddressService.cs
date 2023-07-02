using Rise.Web.Models;

namespace Rise.Web.Services.IServices
{
	public interface IAddressService
	{
		Task<T> GetAddressByUserIdAsnyc<T>(string userId, string token);
		Task<T> GetAddressByIdAsync<T>(int id, string token);
		Task<T> CreateAddressAsync<T>(AddressDto addressDto, string token);
		Task<T> UpdateAddressAsync<T>(AddressDto addressDto, string token);
		Task<T> DeleteAddressAsync<T>(int id, string token);

	}
}
