using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IBasketRepository
    {
        Task<CustomerBasket?> GetBasketAsync(string Id);
        Task<bool> DeleteBasketAsync(string Id);
        Task<CustomerBasket?> UpdateBasket(CustomerBasket basket,TimeSpan? timeToLive = null);
    }
}
