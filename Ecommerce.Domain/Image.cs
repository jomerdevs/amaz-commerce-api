using Ecommerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain
{
    public class Image : BaseDomainModel
    {
        [Column(TypeName = "NVARCHAR(4000)")]
        public string? Url { get; set; }
        public int ProductId { get; set; }
        public virtual Product? Product { get; set; }
        // PublicCode será el código que nos devolverá el proveedor cloud a la hora de almacenar la imagen
        public string? PublicCode { get; set; }
    }
}
