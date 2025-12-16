using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Orders;

namespace Talabat.Repository.Data.Configurations.OrdersConfigurations
{
    public class OrderItmeConfigurations : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.Property(o => o.Price).HasColumnType("decimal(18,4)");
            builder.Property(o => o.Quantity);
            builder.OwnsOne(o => o.ItemOrdered, io =>
            {
                io.WithOwner();
                io.Property(i => i.ProductId).IsRequired();
                io.Property(i => i.ProductName).IsRequired().HasMaxLength(100);
                io.Property(i => i.PictureUrl).IsRequired();
            });
        }
    }
}
