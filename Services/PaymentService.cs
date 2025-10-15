using Domain.Entities.OrderEntity;
using Domain.Exceptions.NotFoundExceptions;
using Services.Specifications;
using Shared.Dtos;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product = Domain.Entities.Product;
using Order = Domain.Entities.OrderEntity.Order;


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

        public async Task UpdateOrderPaymentStatus(string request, string header)
        {
           
                var endPointSecret = configuration.GetSection("StripeSettings")["EndPointSecret"];
                var stripeEvent = EventUtility.ConstructEvent(request, header
                    , endPointSecret, throwOnApiVersionMismatch: false);
                
                var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
            switch (stripeEvent.Type)
            {
                case EventTypes.PaymentIntentSucceeded:
                {
                        await UpdatePaymentSuccesseded(paymentIntent!.Id);
                        break;
                }
                case EventTypes.PaymentIntentPaymentFailed:
                {
                        await UpdatePaymentFailed(paymentIntent!.Id);
                        break;
                }
            }
        }

        private async Task UpdatePaymentFailed(string paymentIntentId)
        {
            var orderRepo = unitOfWork.GetRepository<Order, Guid>();
            var order = await orderRepo.GetByIdAsync(new OrderWithPaymentIntentIdSpecifications(paymentIntentId)) ?? throw new Exception();
            order.OrderPaymnetStatus = OrderPaymnetStatus.PaymentFailed;
            orderRepo.Update(order);
            await unitOfWork.SaveChangesAsync();
        }

        private async Task UpdatePaymentSuccesseded(string paymentIntentId)
        {
            var orderRepo = unitOfWork.GetRepository<Order, Guid>();
            var order = await orderRepo.GetByIdAsync(new OrderWithPaymentIntentIdSpecifications(paymentIntentId)) ?? throw new Exception();
            order.OrderPaymnetStatus = OrderPaymnetStatus.PaymentRecived;
            orderRepo.Update(order);
            await unitOfWork.SaveChangesAsync();
        }
    }
}
