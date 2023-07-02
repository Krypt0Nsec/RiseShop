using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Rise.Service.OrderAPI.DbContexts;
using Rise.Service.OrderAPI.Models;
using Rise.Services.OrderAPI.Models;
using Rise.Services.OrderAPI.Models.Dto;
using Rise.Services.OrderAPI.Repository.IRepository;

namespace Rise.Services.OrderAPI.Repository
{
	public class OrderAdminRepository : IOrderAdminRepository
	{

		private IMapper _mapper;
		private readonly ApplicationDbContext _context;

		public OrderAdminRepository(IMapper mapper, ApplicationDbContext context)
		{

			_mapper = mapper;
			_context = context;
		}

		public async Task<IEnumerable<OrderDetailsDto>> GetOrderDetails()
		{
			List<OrderDetails> orderList = await _context.OrderDetails.ToListAsync();
			return _mapper.Map<List<OrderDetailsDto>>(orderList);
		}

		public async Task<OrderDto> GetOrderByUserId(string userId)
		{
			Order order = new()
			{
				OrderHeader = await _context.OrderHeaders.FirstOrDefaultAsync(x => x.UserId == userId)
			};
			order.OrderDetails = _context.OrderDetails.Where(x => x.OrderHeaderId == order.OrderHeader.OrderHeaderId);

			return _mapper.Map<OrderDto>(order);
		}

		public async Task<OrderHeadersDto> GetOrderHeadersByUserId(string userId)
		{
			OrderHeader orderHeader = await _context.OrderHeaders.FirstOrDefaultAsync(x => x.UserId == userId);
			return _mapper.Map<OrderHeadersDto>(orderHeader);
		}

		public async Task<OrderDetailsDto> GetOrderByOrderDetailsId(int orderDetailsId)
		{

			OrderDetails orderDetails = await _context.OrderDetails.FirstOrDefaultAsync(x => x.OrderDetailsId == orderDetailsId);

			return _mapper.Map<OrderDetailsDto>(orderDetails);

		}

		public async Task<OrderHeadersDto> GetOrderByOrderHeadersId(int orderHeadersId)
		{
			OrderHeader orderHeader = await _context.OrderHeaders.FirstOrDefaultAsync(x=>x.OrderHeaderId==orderHeadersId);
			return _mapper.Map<OrderHeadersDto>(orderHeader);

		}

		
	}
}
