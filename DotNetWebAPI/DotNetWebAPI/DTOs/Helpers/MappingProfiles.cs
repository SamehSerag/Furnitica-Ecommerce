using AngularAPI.DTOs;
using AngularProject.Models;
using AutoMapper;
using DotNetWebAPI.DTOs;
using DotNetWebAPI.DTOs.Helpers;
using DotNetWebAPI.Models;

namespace AngularAPI.Dtos.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d => d.Category, o => o.MapFrom(s => s.Category.Name))
                .ForMember(d => d.Owner, o => o.MapFrom<ProductOwnerResolver<ProductToReturnDto>>())
                .ForMember(d => d.Images, o => o.MapFrom<ProductUrlResolver>());
                //.ForMember(d => d)
            //.ForMember(d => d.Images, o => o.MapFrom(s => s.Images.Select(i => i.Src)));

            CreateMap<Product, AdminProductDto>()
                .ForMember(d => d.OrderProduct, o => o.MapFrom(s => s.OrderProducts))
                //.ForMember(d => d.Owner, o => o.MapFrom<ProductOwnerResolver<AdminProductDto>>())
                .ForMember(d => d.Image, o => o.MapFrom<AdminProductUrlResolver>());

            //CreateMap<AdminProductDto, Product>()
            //   .ForMember(d => d.OrderProducts, o => o.MapFrom(s => s.OrderProduct))
            //   //.ForMember(d => d.Owner, o => o.MapFrom<ProductOwnerResolver<AdminProductDto>>())
            //   .ForMember(d => d.Image, o => o.MapFrom<AdminProductUrlResolver>());

            CreateMap<ProductToAdd, Product>()
                .ForMember(d => d.Color, o => o.MapFrom(s => s.Color));


            CreateMap<User, UserProfileDto>();
            CreateMap<RegisterDto, User>();
            CreateMap<UserProfileDto, User>();

            CreateMap<Category, ProductCategoryDto>();
            CreateMap<Category, CategoryDto>()
              .ForMember(d => d.count, o => o.MapFrom(s => s.Products.Count));

            CreateMap<Review, ReviewDto>()
             .ForMember(d => d.UserName, o => o.MapFrom(s => s.user.UserName))
             .ForMember(d => d.UserImg, o => o.MapFrom(s => s.user.Image.Src));

        }
    }
}
