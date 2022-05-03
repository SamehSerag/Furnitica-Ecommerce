using AngularProject.Models;
using AutoMapper;

namespace AngularAPI.Dtos.Helpers
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d => d.Category, o => o.MapFrom(s => s.Category.Name))
                .ForMember(d => d.Images, o => o.MapFrom<ProductUrlResolver>());
                //.ForMember(d => d.Images, o => o.MapFrom(s => s.Images.Select(i => i.Src)));
        }
    }
}
