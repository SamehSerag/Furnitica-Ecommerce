using AngularProject.Models;
using AutoMapper;

namespace DotNetWebAPI.DTOs.Helpers
{
    public class CategoryResolver : IValueResolver<Category, CategoryDto, int>
    {
        public int Resolve(Category source, CategoryDto destination, int destMember, ResolutionContext context)
        {
            
            return source.Products.Count;
        }
    }
}
