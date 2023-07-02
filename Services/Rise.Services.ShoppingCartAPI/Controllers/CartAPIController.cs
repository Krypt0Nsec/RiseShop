using Rise.Service.ShoppingCartAPI.Messages;
using Rise.Service.ShoppingCartAPI.RabbitMQSender;
using Rise.Service.ShoppingCartAPI.Repository;
using Rise.MessageBus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Rise.Service.ShoppingCartAPI.Models.Dto;
using Azure;

namespace Rise.Service.ShoppingCartAPI.Controllers
{
    //[ApiController]
    [Route("api/cart")]
    public class CartAPIController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICouponRepository _couponRepository;
        // private readonly IMessageBus _messageBus;
        protected ResponseDto _response;
        private readonly IRabbitMQCartMessageSender _rabbitMQCartMessageSender;
        // IMessageBus messageBus,
        public CartAPIController(ICartRepository cartRepository,
            ICouponRepository couponRepository, IRabbitMQCartMessageSender rabbitMQCartMessageSender)
        {
            _cartRepository = cartRepository;
            _couponRepository = couponRepository;
            _rabbitMQCartMessageSender = rabbitMQCartMessageSender;
            //_messageBus = messageBus;
            this._response = new ResponseDto();
        }

        [HttpGet("GetCart/{userId}")]
        public async Task<object> GetCart(string userId)
        {
            try
            {
                CartDto cartDto = await _cartRepository.GetCartByUserId(userId);
                _response.Result = cartDto;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }



        [HttpPost]
        [Authorize]
        public async Task<object> Post([FromBody] CartDto cartDto)
        {
            try
            {
                if (cartDto.CartHeader.CouponCode == null)
                {
                    cartDto.CartHeader.CouponCode = ""; //
                }
                CartDto model = await _cartRepository.CreateUpdateCart(cartDto);
                _response.Result = model;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost("UpdateCart")]
        public async Task<object> UpdateCart(CartDto cartDto)
        {
            try
            {
                CartDto cartDt = await _cartRepository.CreateUpdateCart(cartDto);
                _response.Result = cartDt;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost("RemoveCart")]
        public async Task<object> RemoveCart([FromBody] int cartId)
        {
            try
            {
                bool isSuccess = await _cartRepository.RemoveFromCart(cartId);
                _response.Result = isSuccess;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost("ApplyCoupon")]
        public async Task<object> ApplyCoupon([FromBody] CartDto cartDto)
        {
            try
            {
                bool isSuccess = await _cartRepository.ApplyCoupon(cartDto.CartHeader.UserId,
                    cartDto.CartHeader.CouponCode);
                _response.Result = isSuccess;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost("RemoveCoupon")]
        public async Task<object> RemoveCoupon([FromBody] string userId)
        {
            try
            {
                bool isSuccess = await _cartRepository.RemoveCoupon(userId);
                _response.Result = isSuccess;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }




        [HttpPost("Checkout2")]
        public async Task<object> Checkout2(CartDto cartDto)
        {
            try
            {
                //CartDto cartDto = await _cartRepository.GetCartByUserId(cartDto.CartHeader.UserId);
                //if (cartDto == null)
                //{
                //    return BadRequest();
                //}

                if (!string.IsNullOrEmpty(cartDto.CartHeader.CouponCode))
                {
                    CouponDto coupon = await _couponRepository.GetCoupon(cartDto.CartHeader.CouponCode);
                    //if (cartDto.CartHeader.Dis != coupon.DiscountAmount)
                    //{
                    //    _response.IsSuccess = false;
                    //    _response.ErrorMessages = new List<string>() { "Coupon Price has changed, please confirm" };
                    //    _response.DisplayMessage = "Coupon Price has changed, please confirm";
                    //    return _response;
                    //}
                }

                // cartDto.CartDetails = cartDto.CartDetails;
                //logic to add message to process order.
                // await _messageBus.PublishMessage(checkoutHeader, "checkoutqueue");

                ////rabbitMQ
                // _rabbitMQCartMessageSender.SendMessage(cartDto.CartHeader.ToString, "checkoutqueue");
                await _cartRepository.ClearCart(cartDto.CartHeader.UserId);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }
    }
}

