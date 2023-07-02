using Rise.Services.CouponAPI.Models.Dto;

namespace Rise.Services.CouponAPI.Repository
{
    public interface ICouponRepository
    {
        Task<CouponDto> GetCouponByCode(string couponCode);
    }
}
