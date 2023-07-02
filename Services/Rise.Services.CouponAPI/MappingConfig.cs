using AutoMapper;
using Rise.Services.CouponAPI.Models;
using Rise.Services.CouponAPI.Models.Dto;

namespace Rise.Services.CouponAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CouponDto, Coupon>().ReverseMap();
              
            });

            return mappingConfig;
        }
    }
}
