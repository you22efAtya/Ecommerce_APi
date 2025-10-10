using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.OrderModels
{
    public record DelivaryMethodResult
    {
        public int Id { get; init; }
        public string ShortName { get; init; }
        public string Description { get; init; }
        public decimal Price { get; init; }
        public string DelivaryTime { get; init; }
    }
}
