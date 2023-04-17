using AutoMapper;
using Ecommerce.Application.Features.Images.Queries.ViewModels;
using Ecommerce.Application.Features.Products.Queries.ViewModels;
using Ecommerce.Application.Features.Reviews.Queries.ViewModels;
using Ecommerce.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Mappings
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductViewModel>()
                .ForMember(p => p.CategoryNombre, x => x.MapFrom(a => a.Category!.Nombre))
                .ForMember(p => p.NumeroReviews, x => x.MapFrom(a => a.Reviews == null ? 0 : a.Reviews.Count));

            CreateMap<Image, ImageViewModel>();
            CreateMap<Review, ReviewViewModel>();
        }
    }
}
