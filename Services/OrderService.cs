using Domain.Entities.OrderEntity;
using Domain.Exceptions;
using Domain.Exceptions.NotFoundExceptions;
using Services.Specifications;
using Shared.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    internal class OrderService(IMapper _mapper, IBasketRepository _basketRepository, IUnitOfWork _unitOfWork) : IOrderService
    {
        public async Task<OrderResult> CreateOrderAsync(OrderRequest request, string userEmail)
        {
            var shippingAddress = _mapper.Map<ShippingAddress>(request.ShippingAddress);
            var basket = await _basketRepository.GetBasketAsync(request.BasketId) ?? throw new BasketNotFoundException(request.BasketId);
            var orderItems = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(item.Id)
                    ?? throw new ProductNotFoundException(item.Id);
                orderItems.Add(createOrderItem(item, product));
            }
            var delivaryMethod = await _unitOfWork.GetRepository<DeliveryMethod,int>()
                .GetByIdAsync(request.DelivaryMethodId) ?? throw new DelivaryMethodNotFoundException(request.DelivaryMethodId);
            var subTotal = orderItems.Sum(item => item.Price * item.Quantity);
            var order = new Order(userEmail,shippingAddress,orderItems, delivaryMethod, subTotal);
            await _unitOfWork.GetRepository<Order,Guid>().AddAsync(order);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<OrderResult>(order);
        }

        private OrderItem createOrderItem(BasketItem item, Product product)
        => new OrderItem(new ProductInOrderItem(product.Id, product.Name, product.PictureUrl), item.Quantity, product.Price);

        public async Task<IEnumerable<OrderResult>> GetAllOrdersByEmailAsync(string userEmail)
        {
            var orders = await _unitOfWork.GetRepository<Order, Guid>()
                .GetAllAsync(new OrderWithIncludesSpecifications(userEmail));
            return _mapper.Map<IEnumerable<OrderResult>>(orders);
        }

        public async Task<IEnumerable<DelivaryMethodResult>> GetDelivaryMethodAsync()
        {
            var method = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<DelivaryMethodResult>>(method);
        }

        public async Task<OrderResult> GetOrderByIdAsync(Guid id)
        {
            var order = await _unitOfWork.GetRepository<Order, Guid>()
                .GetByIdAsync(new OrderWithIncludesSpecifications(id)) ?? throw new OrderNotFoundException(id);
            return _mapper.Map<OrderResult>(order);
        }
    }
}
