using AutoMapper;
using Shopbridge_base.Domain.Models;
using System.Diagnostics.CodeAnalysis;

namespace Shopbridge_base.Core.Implementations
{
    [ExcludeFromCodeCoverage]
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, ProductDTO>();
            CreateMap<ProductDTO, Product>();
        }
    }
}
