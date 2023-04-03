using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Configuration
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        // otra forma de hacer validaciones de propiedades de una tabla sin usar anotaciones
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.Property(x => x.Precio).HasColumnType("decimal(10,2)");
        }
    }
}
