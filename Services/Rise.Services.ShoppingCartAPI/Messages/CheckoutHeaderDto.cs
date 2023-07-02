using Iyzipay.Model;
using Rise.MessageBus;
using Rise.Service.ShoppingCartAPI.Models.Dto;

namespace Rise.Service.ShoppingCartAPI.Messages
{

    public class CheckoutHeaderDto : BaseMessage
    {
        public int CartHeaderId { get; set; }
        public string UserId { get; set; }
        public string CouponCode { get; set; }
        public double OrderTotal { get; set; }
        public double DiscountTotal { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime PickupDateTime { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string CardNumber { get; set; }
        public string CVV { get; set; }
        public string ExpiryMonth { get; set; }

        public string ExpiryYear { get; set; }
        public int CartTotalItems { get; set; }

        public int AddressId { get; set; }
        public Address Address { get; set; }
        public IEnumerable<CartDetailsDto> CartDetails { get; set; }
    }
}
