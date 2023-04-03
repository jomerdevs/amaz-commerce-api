using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        // Otra forma de crear relaciones fuera del DbContext usando EntityFrameworkCore
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            // relación de uno a uno entre order y orderAddress
            builder.OwnsOne(o => o.OrderAddress, x =>
            {
                x.WithOwner();
            });

            builder.HasMany(o => o.OrderItems).WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            // convertir el status de la orden que es un enum a un formato string
            builder.Property(s => s.Status).HasConversion(
                    o => o.ToString(),
                    o => (OrderStatus)Enum.Parse(typeof(OrderStatus), o)
                );
        }
    }
}
