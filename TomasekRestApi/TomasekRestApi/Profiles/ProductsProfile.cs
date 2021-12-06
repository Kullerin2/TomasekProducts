using AutoMapper;
using TomasekRestApi.Model.Dto;
using TomasekRestApi.Model.Models;

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
