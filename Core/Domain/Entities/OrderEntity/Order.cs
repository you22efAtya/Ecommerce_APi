using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.OrderEntity
{
    public class Order : BaseEntity<Guid>
    {
        public Order() {}
        public Order(string userEmail,
            ShippingAddress shippingAddress,
            ICollection<OrderItem> orderItems,
            DeliveryMethod deliveryMethod,
            decimal subtotal)
        {
            Id = Guid.NewGuid();
            UserEmail = userEmail;
            ShippingAddress = shippingAddress;
            OrderItems = orderItems;
            DeliveryMethod = deliveryMethod;
            Subtotal = subtotal;
        }

        public string UserEmail { get; set; }
        public ShippingAddress ShippingAddress { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public OrderPaymnetStatus OrderPaymnetStatus { get; set; } = OrderPaymnetStatus.Pending;
        public DeliveryMethod DeliveryMethod { get; set; }
        public int? DeliveryMethodId { get; set; }
        public decimal Subtotal { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public string PaymentIntentId { get; set; } = string.Empty;
    }
}
