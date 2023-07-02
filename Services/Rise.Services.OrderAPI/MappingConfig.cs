using AutoMapper;
using Rise.Service.OrderAPI.Models;
using Rise.Services.OrderAPI.Models;
using Rise.Services.OrderAPI.Models.Dto;

namespace Rise.Services.OrderAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<OrderDetailsDto, OrderDetails>();
                config.CreateMap<OrderDetails, OrderDetailsDto>();
				config.CreateMap<OrderHeadersDto, OrderHeader>();
				config.CreateMap<OrderHeader, OrderHeadersDto>();
				config.CreateMap<OrderDto, Order>();
				config.CreateMap<Order, OrderDto>();
			});

            return mappingConfig;
        }
    }
}
