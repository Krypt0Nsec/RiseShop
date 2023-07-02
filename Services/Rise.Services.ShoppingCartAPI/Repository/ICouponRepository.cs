using Rise.Service.ShoppingCartAPI.Models.Dto;

namespace Rise.Service.ShoppingCartAPI.Repository
{
    public interface ICouponRepository
    {
        Task<CouponDto> GetCoupon(string couponName);
    }
}
