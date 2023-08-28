using AutoMapper;
using Practice.ApiKeyAuthentication.Models;

namespace Practice.ApiKeyAuthentication
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Product, ProductDto>();
        }
    }
}
