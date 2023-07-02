using Rise.Service.OrderAPI.Models;
using Rise.Services.OrderAPI.Models.Dto;

namespace Rise.Services.OrderAPI.Repository.IRepository
{
    public interface IOrderRepository
    {
        Task<bool> AddOrder(OrderHeader orderHeader);
        Task UpdateOrderPaymentStatus(int orderHeaderId, bool paid);



    }
}
