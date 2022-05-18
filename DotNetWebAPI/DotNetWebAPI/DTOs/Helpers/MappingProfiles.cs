using AngularProject.Models;
using AutoMapper;
using DotNetWebAPI.DTOs;
using DotNetWebAPI.Models;

namespace AngularAPI.Dtos.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d => d.Category, o => o.MapFrom(s => s.Category.Name))
                .ForMember(d => d.Images, o => o.MapFrom<ProductUrlResolver>());
            //.ForMember(d => d.Images, o => o.MapFrom(s => s.Images.Select(i => i.Src)));

            CreateMap<Product, AdminProductDto>()
                .ForMember(d => d.OrderProduct, o => o.MapFrom(s => s.OrderProducts))
                .ForMember(d => d.Image, o => o.MapFrom<AdminProductUrlResolver>());

            CreateMap<Category, CategoryDto>()
              .ForMember(d => d.count, o => o.MapFrom(s => s.Products.Count));

            CreateMap<Review, ReviewDto>()
             .ForMember(d => d.UserName, o => o.MapFrom(s => s.user.UserName))
             .ForMember(d => d.UserImg, o => o.MapFrom(s => s.user.Image.Src));

        }
    }
}
