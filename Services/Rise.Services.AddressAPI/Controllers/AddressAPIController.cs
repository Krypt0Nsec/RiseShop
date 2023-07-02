using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rise.Services.AddressAPI.Models.Dto;
using Rise.Services.AddressAPI.Repository.IRepository;

namespace Rise.Services.AddressAPI.Controllers
{
    [Route("api/address")]
    [ApiController]
    public class AddressAPIController : ControllerBase
    {
        protected ResponseDto _response;
        private IAddressRepository _addressRepository;

        public AddressAPIController(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
            this._response = new ResponseDto();
        }

        [HttpGet]
        public async Task<object> Get()
        {
            try
            {
                IEnumerable<AddressDto> addresstDtos = await _addressRepository.GetAddress();
                _response.Result = addresstDtos;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpGet]
        [Route("{AddressId}")]
        public async Task<object> Get(int AddressId)
        {
            try
            {
                AddressDto addressDto = await _addressRepository.GetAddressById(AddressId);
                _response.Result = addressDto;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }


        [HttpPost]
        [Authorize]
        public async Task<object> Post([FromBody] AddressDto addressDto)
        {
            try
            {
                AddressDto model = await _addressRepository.CreateUpdateAddress(addressDto);
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


        [HttpPut]
        [Authorize]
        public async Task<object> Put([FromBody] AddressDto addressDto)
        {
            try
            {
                AddressDto model = await _addressRepository.CreateUpdateAddress(addressDto);
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

        [HttpDelete]
        [Authorize]
        [Route("{id}")]
        public async Task<object> Delete(int id)
        {
            try
            {
                bool isSuccess = await _addressRepository.DeleteAddress(id);
                _response.Result = isSuccess;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpGet("GetAddress/{userId}")]
        [Authorize]
        public async Task<object> GetAddressById(string userId)
        {
            try
            {
                IEnumerable<AddressDto> addressDtos = await _addressRepository.GetAddressByUserId(userId);
                _response.Result = addressDtos;
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
