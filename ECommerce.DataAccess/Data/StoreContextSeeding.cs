using ECommerce.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerce.DataAccess.Data
{
    public static class StoreContextSeeding
    {
        public async static Task SeedAsync(ApplicationDbContext context)
        {
            //if(!context.Roles.Any())
            //{
            //    var rolesData = File.ReadAllText("../ECommerce.DataAccess/Data/SeedingFiles/AspNetRoles.Json");
            //    var roles = JsonSerializer.Deserialize<Roles>
            //}

            if(!context.Categories.Any())
            {
                var categoriesData = File.ReadAllText("/ECommerce.DataAccess/Data/SeedingFiles/Categories.Json");
                var categories = JsonSerializer.Deserialize<List<Category>>(categoriesData);

                foreach (var item in categories)
                {
                    await context.Categories.AddAsync(item);
                    await context.SaveChangesAsync();
                }
            }
            if (!context.Products.Any())
            {
                var ProductsData = File.ReadAllText("/ECommerce.DataAccess/Data/SeedingFiles/Products.Json");
                var Products = JsonSerializer.Deserialize<List<Product>>(ProductsData);

                foreach (var item in Products)
                {
                    await context.Products.AddAsync(item);
                    await context.SaveChangesAsync();
                }
            }
            if (!context.ApplicationUsers.Any())
            {
                var ApplicationUsersData = File.ReadAllText("/ECommerce.DataAccess/Data/SeedingFiles/AspNetUsers.Json");
                var ApplicationUsers = JsonSerializer.Deserialize<List<ApplicationUser>>(ApplicationUsersData);

                foreach (var item in ApplicationUsers)
                {
                    await context.ApplicationUsers.AddAsync(item);
                    await context.SaveChangesAsync();
                }
            }
            if (!context.ShoppingCarts.Any())
            {
                var ShoppingCartsData = File.ReadAllText("/ECommerce.DataAccess/Data/SeedingFiles/ShoppingCarts.Json");
                var ShoppingCarts = JsonSerializer.Deserialize<List<ShoppingCart>>(ShoppingCartsData);

                foreach (var item in ShoppingCarts)
                {
                    await context.ShoppingCarts.AddAsync(item);
                    await context.SaveChangesAsync();
                }
            }
            if (!context.OrderHeaders.Any())
            {
                var OrderHeadersData = File.ReadAllText("/ECommerce.DataAccess/Data/SeedingFiles/ShoppingCarts.Json");
                var OrderHeaders = JsonSerializer.Deserialize<List<OrderHeader>>(OrderHeadersData);

                foreach (var item in OrderHeaders)
                {
                    await context.OrderHeaders.AddAsync(item);
                    await context.SaveChangesAsync();
                }
            }
            if (!context.OrderDetails.Any())
            {
                var OrderDetailsData = File.ReadAllText("/ECommerce.DataAccess/Data/SeedingFiles/ShoppingCarts.Json");
                var OrderDetails = JsonSerializer.Deserialize<List<OrderDetail>>(OrderDetailsData);

                foreach (var item in OrderDetails)
                {
                    await context.OrderDetails.AddAsync(item);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
