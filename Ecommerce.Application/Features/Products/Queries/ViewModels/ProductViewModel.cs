using Ecommerce.Application.Features.Images.Queries.ViewModels;
using Ecommerce.Application.Features.Reviews.Queries.ViewModels;
using Ecommerce.Application.Models.Product;
using Ecommerce.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.Products.Queries.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public decimal precio { get; set; }
        public int Rating { get; set; }
        public string? Vendedor { get; set; }
        public int Stock { get; set; }

        public virtual ICollection<ReviewViewModel>? Reviews { get; set; }
        public virtual ICollection<ImageViewModel>? Images { get; set; }

        public int CategoryId { get; set; }
        public string? CategoryNombre { get; set; }
        public int NumeroReviews { get; set; }
        public ProductStatus Status { get; set; }
        public string? StatusLabel {
            get
            {
                switch (Status)
                {
                    case ProductStatus.Activo:
                        {
                            return ProductStatusLabel.ACTIVO;
                        }
                    case ProductStatus.Inactivo:
                        {
                            return ProductStatusLabel.INACTIVO;
                        }
                    default: return ProductStatusLabel.INACTIVO;
                }
            }
            set { }
        }
    }
}
