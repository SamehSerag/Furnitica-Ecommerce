using AngularAPI.Dtos;
using AngularProject.Models;
using AutoMapper;

namespace DotNetWebAPI.DTOs.Helpers
{
    public class ProductOwnerResolver<T> : IValueResolver<Product,
        T, OwnerDto>
    {
        //public OwnerDto Resolve(Product source, ProductToReturnDto destination, 
        //    OwnerDto destMember, ResolutionContext context)
        //{
        //    return new OwnerDto()
        //    {
        //        Id = source.Owner.Id,
        //        UserName = source.Owner.UserName
        //    };
        //}

        public OwnerDto Resolve(Product source, T destination, OwnerDto destMember, ResolutionContext context)
        {
            if (source.Owner != null)
                return new OwnerDto()
                {
                    Id = source.Owner.Id,
                    UserName = source.Owner.UserName
                };
            else
                return new OwnerDto();
        }
    }
}
