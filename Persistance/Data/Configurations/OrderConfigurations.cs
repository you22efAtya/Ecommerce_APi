using Domain.Entities.OrderEntity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Data.Configurations
{
    public class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(o => o.ShippingAddress, s => s.WithOwner());
            builder.HasMany(o => o.OrderItems).WithOne();
            builder.Property(o => o.OrderPaymnetStatus).HasConversion(paymentstatus => paymentstatus.ToString(),
            s => Enum.Parse<OrderPaymnetStatus>(s));
            builder.HasOne(o => o.DeliveryMethod).WithMany().OnDelete(DeleteBehavior.SetNull);
            builder.Property(o => o.Subtotal).HasColumnType("decimal(18,3)");
        }
    }
}
