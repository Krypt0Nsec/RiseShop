namespace Rise.Web.Models
{
	public class OrderDto
	{
		public OrderHeadersDto OrderHeader { get; set; }
		public IEnumerable<OrderDetailsDto> OrderDetails { get; set; }


	}
}
