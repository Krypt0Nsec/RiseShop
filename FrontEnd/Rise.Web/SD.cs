using Duende.IdentityServer.Models;
using Duende.IdentityServer;

namespace Rise.Web
{
    public static class SD
    {
        public static string ProductAPIBase { get; set; }
        public static string ShoppingCartAPIBase { get; set; }
        public static string CouponAPIBase { get; set; }
        public static string OrderAPIBase { get; set; }

        public static string AddressAPIBase { get; set; }
        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE
        }

		
	}
}
