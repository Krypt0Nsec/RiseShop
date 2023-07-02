using Rise.Services.AddressAPI.Models.Dto;

namespace Rise.Services.AddressAPI.Repository.IRepository
{
	public interface IAddressRepository
	{
		Task<IEnumerable<AddressDto>> GetAddress();
		Task<IEnumerable<CityDto>> GetCity();
		Task<AddressDto> GetAddressById(int AddressId);
		Task<AddressDto> CreateUpdateAddress(AddressDto addressDto);
		Task<IEnumerable<AddressDto>> GetAddressByUserId(string UserId);
		Task<bool> DeleteAddress(int AddressId);
	}
}
