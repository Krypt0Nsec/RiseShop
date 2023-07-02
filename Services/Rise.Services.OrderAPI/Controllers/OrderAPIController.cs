using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using Rise.Services.OrderAPI.Models.Dto;
using Rise.Services.OrderAPI.Repository.IRepository;
using System.Data;

namespace Rise.Services.OrderAPI.Controllers
{
	[Route("api/order")]
	[ApiController]
	public class OrderAPIController : ControllerBase
	{
		protected ResponseDto _response;
		private IOrderAdminRepository _orderAdminRepository;

		public OrderAPIController(IOrderAdminRepository orderAdminRepository)
		{
			_orderAdminRepository = orderAdminRepository;
			this._response = new ResponseDto();
		}

		[HttpGet("GetOrders/")]
		//[Authorize(Roles = "Admin")]
		public async Task<object> GetOrders()
		{
			try
			{
				IEnumerable<OrderDetailsDto> orderDetailsDtos = await _orderAdminRepository.GetOrderDetails();
				_response.Result = orderDetailsDtos;
			}
			catch (Exception ex)
			{

				_response.IsSuccess = false;
				_response.ErrorMessages = new List<string>() { ex.ToString() };
			}
			return _response;
		}

		[HttpGet("GetOrders/{userId}")]
		//[Authorize]
		public async Task<object> GetOrderById(string userId)
		{
			try
			{
				OrderDto orderDto = await _orderAdminRepository.GetOrderByUserId(userId);
				_response.Result = orderDto;
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages = new List<string>() { ex.ToString() };
			}
			return _response;
		}

		[HttpGet("GetHeaders/{headerId}")]
		//[Authorize(Roles = "Admin")]
		public async Task<object> GetOrderHeadersById(int headerId)
		{
			try
			{
				OrderHeadersDto orderDto = await _orderAdminRepository.GetOrderByOrderHeadersId(headerId);
				_response.Result = orderDto;
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages
					 = new List<string>() { ex.ToString() };
			}
			return _response;
		}

		[HttpGet("GetHeader/{userId}")]
		//[Authorize]
		public async Task<object> GetOrderHeadersByUserId(string userId)
		{
			try
			{
				OrderHeadersDto orderHeadersDto = await _orderAdminRepository.GetOrderHeadersByUserId(userId);
				_response.Result = orderHeadersDto;
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages
					 = new List<string>() { ex.ToString() };
			}
			return _response;


		}

		[HttpGet("GetDetails/{detailsId}")]
		//[Authorize(Roles = "Admin")]

		public async Task<object> GetOrderDetailsById(int detailsId)
		{
			try
			{
				OrderDetailsDto orderDetailsDto = await _orderAdminRepository.GetOrderByOrderDetailsId(detailsId);
				_response.Result = orderDetailsDto;
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages
					 = new List<string>() { ex.ToString() };
			}

			return _response;
		}


	}
	 
}
