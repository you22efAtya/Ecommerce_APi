using Shared.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IOrderService
    {
        Task<OrderResult> GetOrderByIdAsync(Guid id);
        Task<IEnumerable<OrderResult>> GetAllOrdersByEmailAsync(string userEmail);
        Task<OrderResult> CreateOrderAsync(OrderRequest request, string userEmail);
        Task<IEnumerable<DelivaryMethodResult>> GetDelivaryMethodAsync();


    }
}
