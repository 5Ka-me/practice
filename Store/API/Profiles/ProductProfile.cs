using API.ViewModels;
using AutoMapper;
using BLL.Models;
using DAL.Entities;

namespace API.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductViewModel, ProductModel>().ReverseMap();
            CreateMap<Product, ProductModel>().ReverseMap();
        }
    }
}
