using Rise.Web.Models;
using Rise.Web.Services.IServices;

namespace Rise.Web.Services
{
	public class AddressService : BaseService, IAddressService
	{
		private readonly IHttpClientFactory _clientFactory;

		public AddressService(IHttpClientFactory clientFactory) : base(clientFactory)
		{
			_clientFactory = clientFactory;
		}
		public async Task<T> CreateAddressAsync<T>(AddressDto addressDto, string token)
		{
			return await this.SendAsync<T>(new ApiRequest()
			{
				ApiType = SD.ApiType.POST,
				Data = addressDto,
				Url = SD.AddressAPIBase + "/api/address",
				AccessToken = token
			});
		}

		public async Task<T> DeleteAddressAsync<T>(int id, string token)
		{
			return await this.SendAsync<T>(new ApiRequest()
			{
				ApiType = SD.ApiType.DELETE,
				Url = SD.AddressAPIBase + "/api/address/" + id,
				AccessToken = token
			});
		}

		public async Task<T> GetAddressByIdAsync<T>(int id, string token)
		{
			return await this.SendAsync<T>(new ApiRequest()
			{
				ApiType = SD.ApiType.GET,
				Url = SD.AddressAPIBase + "/api/address/" + id,
				AccessToken = token
			});
		}

		public async Task<T> GetAddressByUserIdAsnyc<T>(string id, string token)
		{
			return await this.SendAsync<T>(new ApiRequest()
			{
				ApiType = SD.ApiType.GET,
				Url = SD.AddressAPIBase + "/api/address/GetAddress/" + id,
				AccessToken = token
			});
		}

		public async Task<T> UpdateAddressAsync<T>(AddressDto addressDto, string token)
		{
			return await this.SendAsync<T>(new ApiRequest()
			{
				ApiType = SD.ApiType.PUT,
				Data = addressDto,
				Url = SD.AddressAPIBase + "/api/address",
				AccessToken = token
			});
		}
	}
}
