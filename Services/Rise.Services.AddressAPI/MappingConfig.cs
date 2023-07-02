using AutoMapper;
using Rise.Services.AddressAPI.Models.Dto;
using Rise.Services.AddressAPI.Models;


namespace Rise.Services.AddressAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<AddressDto, Address>();
                config.CreateMap<Address, AddressDto>();
				config.CreateMap<City, CityDto>();
				config.CreateMap<CityDto, City>();
			});

            return mappingConfig;
        }
    }
}
