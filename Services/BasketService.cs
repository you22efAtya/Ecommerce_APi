using Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Shared.Dtos;

namespace Services
{
    public class BasketService(IBasketRepository _basketRepository, IMapper _mapper) : IBasketService
    {
        public async Task<bool> DeleteBasketAsync(string id)
        => await _basketRepository.DeleteBasketAsync(id);

        public async Task<BasketDto?> GetBasketAsync(string id)
        {
            var basket = await _basketRepository.GetBasketAsync(id);
            return basket is null ? throw new BasketNotFoundException(id) :_mapper.Map<BasketDto?>(basket);
        }

        public async Task<BasketDto?> UpdateBasketAsync(BasketDto basket)
        {
            var customerBasket = await _basketRepository.UpdateBasket(_mapper.Map<CustomerBasket>(basket));
            return customerBasket is null ? throw new Exception("Can not update the basket now !!") : _mapper.Map<BasketDto>(customerBasket);
        }
    }
}
