using Domain.Entities.OrderEntity;
using Domain.Exceptions.NotFoundExceptions;
using Shared.Dtos;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product = Domain.Entities.Product;

namespace Services
{
    public class PaymentService(IBasketRepository basketRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IConfiguration configuration) : IPaymentService
    {
        public async Task<BasketDto> CreateOrUpdatePaymentIntentAsync(string basketId)
        {
            StripeConfiguration.ApiKey = configuration.GetSection("StripeSettings")["SecretKey"];
            var basket = await basketRepository.GetBasketAsync(basketId) ?? throw new BasketNotFoundException(basketId);
            foreach (var item in basket.Items)
            {
                var product = await unitOfWork.GetRepository<Product, int>().GetByIdAsync(item.Id)
                    ?? throw new ProductNotFoundException(item.Id);
                item.Price = product.Price;
            }
            if (!basket.DelivaryMethodId.HasValue) throw new Exception("No Delivery method was selected");
            var delivaryMethod = await unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(basket.DelivaryMethodId.Value)
                ?? throw new DelivaryMethodNotFoundException(basket.DelivaryMethodId.Value);
            basket.ShippingPrice = delivaryMethod.Price;
            var amount = (long)(basket.Items.Sum(i => i.Price * i.Quantity) + basket.ShippingPrice) * 100;
            var service = new PaymentIntentService();
            if(string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var createOptions = new PaymentIntentCreateOptions
                {
                    Amount = amount,
                    Currency = "USD",
                    PaymentMethodTypes = new List<string> {"card"}
                };
                var paymentIntent = await service.CreateAsync(createOptions);
                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                var updateOptions = new PaymentIntentUpdateOptions
                {
                    Amount = amount
                };
                await service.UpdateAsync(basket.PaymentIntentId, updateOptions);
            }
            await basketRepository.UpdateBasket(basket);
            return mapper.Map<BasketDto>(basket);
        }
    }
}
