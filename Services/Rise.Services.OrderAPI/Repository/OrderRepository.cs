using Rise.Service.OrderAPI.DbContexts;
using Rise.Service.OrderAPI.Models;
using Microsoft.EntityFrameworkCore;
using Rise.Services.OrderAPI.Models.Dto;
using AutoMapper;
using Rise.Services.OrderAPI.Repository.IRepository;

namespace Rise.Service.OrderAPI.Repository
{
    public class OrderRepository : IOrderRepository
    {
		private readonly DbContextOptions<ApplicationDbContext> _dbContext;

		public OrderRepository(DbContextOptions<ApplicationDbContext> dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<bool> AddOrder(OrderHeader orderHeader)
		{
			if (orderHeader.CouponCode == null)
			{
				orderHeader.CouponCode = "";
			}
			await using var _db = new ApplicationDbContext(_dbContext);
			_db.OrderHeaders.Add(orderHeader);
			await _db.SaveChangesAsync();
			return true;
		}

		public async Task UpdateOrderPaymentStatus(int orderHeaderId, bool paid)
		{
			await using var _db = new ApplicationDbContext(_dbContext);
			var orderHeaderFromDb = await _db.OrderHeaders.FirstOrDefaultAsync(u => u.OrderHeaderId == orderHeaderId);
			if (orderHeaderFromDb != null)
			{
				orderHeaderFromDb.PaymentStatus = paid;
				await _db.SaveChangesAsync();
			}
		}
	}
    
}
