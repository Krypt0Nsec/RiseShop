using Rise.Services.OrderAPI.Models.Dto;

namespace Rise.Services.OrderAPI.Repository.IRepository
{
	public interface IOrderAdminRepository
	{
		Task<IEnumerable<OrderDetailsDto>> GetOrderDetails();

		Task<OrderDto> GetOrderByUserId(string userId);

		Task<OrderHeadersDto> GetOrderHeadersByUserId(string userId);

		Task<OrderDetailsDto> GetOrderByOrderDetailsId(int orderDetailsId);
		Task<OrderHeadersDto> GetOrderByOrderHeadersId(int orderHeadersId);



	}
}
