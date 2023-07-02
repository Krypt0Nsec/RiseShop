using Rise.Web.Models;
using Rise.Web.Services.IServices;

namespace Rise.Web.Services
{
	public class OrderService : BaseService, IOrderService
	{
		private readonly IHttpClientFactory _clientFactory;

		public OrderService(IHttpClientFactory clientFactory) : base(clientFactory)
		{
			_clientFactory = clientFactory;
		}

		public async Task<T> GetOrderByOrderDetailsId<T>(int id, string token)
		{
			return await this.SendAsync<T>(new ApiRequest()
			{
				ApiType = SD.ApiType.GET,
				Url = SD.OrderAPIBase + "/api/order/GetDetails/" + id,
				AccessToken = token
			});
		}

		public async Task<T> GetOrderByOrderHeadersId<T>(int id, string token)
		{
			return await this.SendAsync<T>(new ApiRequest()
			{
				ApiType = SD.ApiType.GET,
				Url = SD.OrderAPIBase + "/api/order/GetHeaders/" + id ,
				AccessToken = token
			});
		}

		public async Task<T> GetOrderByUserId<T>(string id, string token)
		{
			return await this.SendAsync<T>(new ApiRequest()
			{
				ApiType = SD.ApiType.GET,
				Url = SD.OrderAPIBase + "/api/order/GetOrders/" + id,
				AccessToken = token
			});
		}

		public async Task<T> GetOrderDetails<T>(string token)
		{
			return await this.SendAsync<T>(new ApiRequest()
			{
				ApiType = SD.ApiType.GET,
				Url = SD.OrderAPIBase + "/api/order/GetOrders/",
				AccessToken = token
			});
		}

		public async Task<T> GetOrderHeadersByUserId<T>(string id, string token)
		{

			return await this.SendAsync<T>(new ApiRequest()
			{
				ApiType = SD.ApiType.GET,
				Url = SD.OrderAPIBase + "/api/order/GetHeader/" + id,
				AccessToken = token
			}); 
		}
	}
}
