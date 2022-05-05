using AngularProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace AngularAPI.Data
{
    public class PriceRange
    {
        public  Test test { get; set; }
        public PriceRange()
        {

        }
    
        public PriceRange(decimal minPrice, decimal maxPrice)
        {
            MinPrice = minPrice;
            MaxPrice = maxPrice;
        }
        public PriceRange(IEnumerable<Product> list)
        {
            MaxPrice = list.MaxBy(l => l.price)?.price ?? 0;
            MinPrice = list.MinBy(l => l.price)?.price ?? 0;
        }

        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }

        public bool IsValidRange => MaxPrice > MinPrice ;

        //public static PriceRange CreatePriceRange(IEnumerable<Product> list)
        //{
           
        //    return new PriceRange(list.MaxBy(l => l.price)?.price ?? 0,
        //        list.MinBy(l => l.price)?.price ?? 0
        //        );
        //}
    }

    public class Test
    {
        public string testObj { get; set; }
    }
}


