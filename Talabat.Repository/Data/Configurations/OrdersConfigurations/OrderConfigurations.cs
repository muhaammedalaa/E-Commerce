using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Orders;
using Order = Talabat.Core.Entities.Orders.Order;

namespace Talabat.Repository.Data.Configurations.OrdersConfigurations
{
    public class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(o => o.Subtotal).HasColumnType("decimal(18,4)");

            builder.OwnsOne(o => o.ShipToAddress, sh => sh.WithOwner());

            builder.HasMany(o => o.OrderItems).WithOne().OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(o => o.DeliveryMethod).WithMany().OnDelete(DeleteBehavior.SetNull);

            builder.Property(o => o.Subtotal).HasColumnType("decimal(18,4)");

            builder.Property(o => o.PaymentStatus).HasConversion(
                ps => ps.ToString(), ps => Enum.Parse<OrderPaymentStatus>(ps));
        }
    }
}
