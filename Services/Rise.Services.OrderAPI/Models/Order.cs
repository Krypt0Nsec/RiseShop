using Rise.Service.OrderAPI.Models;

namespace Rise.Services.OrderAPI.Models
{
	public class Order
	{
		public OrderHeader OrderHeader { get; set; }
		public IEnumerable<OrderDetails> OrderDetails { get; set; }

			
	}
}
