using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.Core.Dtos.Basket;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contarct;

namespace Talabat.APIs.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository _basket;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basket, IMapper mapper)
        {
            _basket = basket;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasket(string? id)
        {
            if (id == null)
                return BadRequest(new ApiResponse(400));

            var basket = await _basket.GetBasketAsync(id);

            return Ok(basket ?? new CustomerBasket(id));
        }
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> CreateOrUpdateBasket(CustomerBasketDto basket)
        {
            var mappedBasket = _mapper.Map<CustomerBasket>(basket);

            var CreatedOrUpdated = await _basket.UpdateBasketAsync(mappedBasket);

            if (CreatedOrUpdated is null)
                return BadRequest(new ApiResponse(400));

            return Ok(CreatedOrUpdated);
        }
        [HttpPost("Item")]
        public async Task<ActionResult<CustomerBasket>> AddItem(string basketId, BasketItemDto item)
        {
            var mappedItem = _mapper.Map<BasketItem>(item);
            await _basket.AddItem(basketId, mappedItem);
            var basket = await _basket.GetBasketAsync(basketId);
            return Ok(basket);
        }
       
       

        [HttpDelete]
        public async Task DeleteBasket(string id)
        {
            await _basket.DeleteBasketAsync(id);


        }
    }
}
