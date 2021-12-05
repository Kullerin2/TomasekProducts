using AutoMapper;
using TomasekRestApi.Dtos;
using TomasekRestApi.Models;

namespace TomasekRestApi.Profiles
{
    public class ProductsProfile : Profile
    {
        public ProductsProfile()
        {
            CreateMap<Product, ProductReadDto>();
        }

    }    

}
