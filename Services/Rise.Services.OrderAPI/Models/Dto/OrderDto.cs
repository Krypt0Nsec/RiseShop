using Rise.Service.OrderAPI.Models;

namespace Rise.Services.OrderAPI.Models.Dto
{
	public class OrderDto
	{
		public OrderHeadersDto OrderHeader { get; set; }
		public IEnumerable<OrderDetailsDto> OrderDetails { get; set; }
	}
}
