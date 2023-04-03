using Ecommerce.Application.Models.Authorization;
using Ecommerce.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.AppContext
{
    public class AppDbContextData
    {
        
    public static async Task LoadDataAsync(AppDbContext context, 
            UserManager<Usuario> usuarioManager,
            RoleManager<IdentityRole> roleManager,
            ILoggerFactory loggerFactory)
        {
            try
            {
                // crear roles si la tabla roles está vacía
                if (!roleManager.Roles.Any())
                {
                    await roleManager.CreateAsync(new IdentityRole(Role.ADMIN));
                    await roleManager.CreateAsync(new IdentityRole(Role.USER));
                }                

                if (!usuarioManager.Users.Any())
                {
                    var usuarioAdmin = new Usuario
                    {
                        Nombre = "Jomer",
                        Apellido = "Sanchez",
                        Email = "jomersanchez30@gmail.com",
                        UserName = "jomersanchez",
                        Telefono = "8090001133",
                        AvatarUrl = "https://firebasestorage.googleapis.com/v0/b/edificacion-app.appspot.com/o/avatar-1.webp?alt=media&token=58da3007-ff21-494d-a85c-25ffa758ff6d"
                    };

                    await usuarioManager.CreateAsync(usuarioAdmin, "Jomersanchez123$");
                    await usuarioManager.AddToRoleAsync(usuarioAdmin, Role.ADMIN);

                    var usuario = new Usuario
                    {
                        Nombre = "Juan",
                        Apellido = "Perez",
                        Email = "juan.perez@test.com",
                        UserName = "juanperez",
                        Telefono = "8090001136",
                        AvatarUrl = "https://firebasestorage.googleapis.com/v0/b/edificacion-app.appspot.com/o/avatar-1.webp?alt=media&token=58da3007-ff21-494d-a85c-25ffa758ff6d"
                    };

                    await usuarioManager.CreateAsync(usuario, "Juanperez123$");
                    await usuarioManager.AddToRoleAsync(usuario, Role.USER);
                }                

                if (!context.Categories.Any())
                {
                    var categoryData = File.ReadAllText("../Ecommerce.Infrastructure/Data/category.json");
                    var categories = JsonConvert.DeserializeObject<List<Category>>(categoryData);
                    
                    await context.Categories.AddRangeAsync(categories!);
                    await context.SaveChangesAsync();
                }

                if (!context.Products.Any())
                {
                    var productData = File.ReadAllText("../Ecommerce.Infrastructure/Data/product.json");
                    var products = JsonConvert.DeserializeObject<List<Product>>(productData);

                    await context.Products.AddRangeAsync(products!);
                    await context.SaveChangesAsync();
                }

                if (!context.Images.Any())
                {
                    var imageData = File.ReadAllText("../Ecommerce.Infrastructure/Data/image.json");
                    var images = JsonConvert.DeserializeObject<List<Image>>(imageData);

                    await context.Images.AddRangeAsync(images!);
                    await context.SaveChangesAsync();
                }

                if (!context.Reviews.Any())
                {
                    var reviewData = File.ReadAllText("../Ecommerce.Infrastructure/Data/review.json");
                    var reviews = JsonConvert.DeserializeObject<List<Review>>(reviewData);

                    await context.Reviews.AddRangeAsync(reviews!);
                    await context.SaveChangesAsync();
                }

                if (!context.Countries!.Any())
                {
                    var countryData = File.ReadAllText("../Ecommerce.Infrastructure/Data/countries.json");
                    var countries = JsonConvert.DeserializeObject<List<Country>>(countryData);

                    await context.Countries!.AddRangeAsync(countries!);
                    await context.SaveChangesAsync();
                }
            }
            catch(Exception ex)
            {
                // logger para los errores
                var logger = loggerFactory.CreateLogger<AppDbContextData>();
                logger.LogError(ex.Message);
            }            
        }
    }
}
