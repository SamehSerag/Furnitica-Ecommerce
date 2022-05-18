using AngularProject.Data;
using AngularProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ShoppingContextSeed
    {
        public static async Task SeedAsync(ShoppingDbContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.Categories.Any())
                {
                    var categoriesData =
                        File.ReadAllText("../DotNetWebAPI/Data/SeedData/categories.json");
                    var categories = JsonSerializer.Deserialize<List<Category>>(categoriesData);

                    foreach (var item in categories)
                    {
                        context.Categories.Add(item);
                    }
                    await context.SaveChangesAsync();
                }

                if (!context.Roles.Any())
                {
                    var roleData =
                        File.ReadAllText("../DotNetWebAPI/Data/SeedData/role.json");
                    var roles = JsonSerializer.Deserialize<List<IdentityRole>>(roleData);

                    foreach (var item in roles)
                    {
                        context.Roles.Add(item);
                    }
                    await context.SaveChangesAsync();
                }

                if (!context.Products.Any())
                {
                    var productData =
                        File.ReadAllText("../DotNetWebAPI/Data/SeedData/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productData);

                    foreach (var item in products)
                    {
                        context.Products.Add(item);
                    }
                    await context.SaveChangesAsync();

                }


            }
            catch (Exception ex)
            {

                var logger = loggerFactory.CreateLogger<ShoppingContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}
