using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Rise.Services.AddressAPI.DbContexts;
using Rise.Services.AddressAPI.Models;
using Rise.Services.AddressAPI.Models.Dto;
using Rise.Services.AddressAPI.Repository.IRepository;

namespace Rise.Services.AddressAPI.Repository
{
	public class AddressRepository : IAddressRepository
	{
		private readonly ApplicationDbContext _context;
		private IMapper _mapper;

		//Constructor Injection 
		public AddressRepository(ApplicationDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}
		public async Task<AddressDto> CreateUpdateAddress(AddressDto addressDto)
		{
			Address address = _mapper.Map<AddressDto, Address>(addressDto);
			if (address.Id > 0)
			{
				_context.Address.Update(address);
			}
			else
			{
				_context.Address.Add(address);
			}
			await _context.SaveChangesAsync();

			return _mapper.Map<Address, AddressDto>(address);
		}

		public async Task<bool> DeleteAddress(int AddressId)
		{
			try
			{
				Address address = await _context.Address.FirstOrDefaultAsync(u => u.Id == AddressId);
				if (address == null)
				{
					return false;
				}
				_context.Address.Remove(address); //delete from Product where Id=productId
				await _context.SaveChangesAsync();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public async Task<IEnumerable<AddressDto>> GetAddressByUserId(string UserId)
		{
			List<Address> addressList = await _context.Address.Where(x=>x.UserId == UserId).ToListAsync();
            return _mapper.Map<List<AddressDto>>(addressList);


		}

		public async Task<AddressDto> GetAddressById(int AddressId)
		{
			Address address = await _context.Address.Where(x => x.Id == AddressId).FirstOrDefaultAsync();
			return _mapper.Map<AddressDto>(address);

		}

		public async Task<IEnumerable<AddressDto>> GetAddress()
		{
			List<Address> addressList = await _context.Address.ToListAsync();
            return _mapper.Map<List<AddressDto>>(addressList);

		}

		public async Task<IEnumerable<CityDto>> GetCity()
		{
			List<City> cityList = await _context.City.ToListAsync();
			return _mapper.Map<List<CityDto>>(cityList);
		}
	}
}
