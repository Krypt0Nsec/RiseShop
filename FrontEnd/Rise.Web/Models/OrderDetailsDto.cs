namespace Rise.Web.Models
{
	public class OrderDetailsDto
	{
		public int OrderDetailsId { get; set; }
		public int OrderHeaderId { get; set; }

		public virtual OrderHeadersDto OrderHeadersDto { get; set; }
		public int ProductId { get; set; }

		public string ImageUrl { get; set; }

		public int Count { get; set; }
		public string ProductName { get; set; }
		public double Price { get; set; }
	}
}
