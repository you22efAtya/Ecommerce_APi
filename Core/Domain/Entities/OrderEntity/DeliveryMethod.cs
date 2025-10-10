using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.OrderEntity
{
    public class DeliveryMethod : BaseEntity<int>
    {
        public DeliveryMethod() { }

        public DeliveryMethod(string shortName, string description, decimal price, string delivaryTime)
        {
            ShortName = shortName;
            Description = description;
            Price = price;
            DelivaryTime = delivaryTime;
        }

        public string ShortName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string DelivaryTime { get; set; }
    }
}
