namespace Rise.Web.Services.IServices
{
	public interface IOrderService : IBaseService
	{
		Task<T> GetOrderDetails<T>(string token);
		Task<T> GetOrderByUserId<T>(string id, string token);
		Task<T> GetOrderHeadersByUserId<T>(string id, string token);
		Task<T> GetOrderByOrderDetailsId<T>(int id, string token);
		Task<T> GetOrderByOrderHeadersId<T>(int id, string token);
		
		
	}
}
