
using Domain.Entities.OrderEntity;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;

namespace Persistance.Data
{
    public class DpIntializer : IDbIntializer
    {
        private readonly AppDbContext _dbContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;

        public DpIntializer(AppDbContext dbContext, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task InitializeAsync()
        {
            try
            {

                if (_dbContext.Database.GetPendingMigrations().Any())
                {
                    await _dbContext.Database.MigrateAsync();
                    if (!_dbContext.ProductTypes.Any())
                    {
                        var typesData = File.ReadAllText(@"../Persistance/Data/DataSeeding/types.json");
                        var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                        if (types != null && types.Any())
                        {
                            await _dbContext.AddRangeAsync(types);
                            await _dbContext.SaveChangesAsync();
                        }
                    }
                    if (!_dbContext.ProductBrands.Any())
                    {
                        //E:\Routes\APIs\Ecommerce\EcommerceSolution\Persistance\Data\DataSeeding\brands.json
                        var brandsData = File.ReadAllText(@"../Persistance/Data/DataSeeding/brands.json");
                        var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                        if (brands != null && brands.Any())
                        {
                            await _dbContext.AddRangeAsync(brands);
                            await _dbContext.SaveChangesAsync();
                        }
                    }
                    if (!_dbContext.Products.Any())
                    {
                        var productsData = File.ReadAllText(@"../Persistance/Data/DataSeeding/products.json");
                        var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                        if (products != null && products.Any())
                        {
                            await _dbContext.AddRangeAsync(products);
                            await _dbContext.SaveChangesAsync();
                        }
                    }
                    if (!_dbContext.DeliveryMethods.Any())
                    {
                    //E:\Routes\APIs\Ecommerce\EcommerceSolution\Persistance\Data\DataSeeding\
                    var methodsData = await File.ReadAllTextAsync(@"../Persistance/Data/DataSeeding/delivery.json");
                        var methods = JsonSerializer.Deserialize<List<DeliveryMethod>>(methodsData);
                        if (methods is not null && methods.Any())
                        {
                            await _dbContext.AddRangeAsync(methods);
                            await _dbContext.SaveChangesAsync();
                        }
                    }
            }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task InitializeIdentityAsync()
        {
            if(!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
                await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
            }

            if(!_userManager.Users.Any())
            {
                var adminUser = new User()
                {
                    DisplayName = "Admin",
                    UserName = "Admin",
                    Email = "Admin@gamil.com",
                    PhoneNumber = "1234567890",
                };
                var superAdminUser = new User()
                {
                    DisplayName = "SuperAdmin",
                    UserName = "SuperAdmin",
                    Email = "SuperAdmin@gamil.com",
                    PhoneNumber = "1234567890",
                };

                await _userManager.CreateAsync(adminUser, "P@ssw0rd");
                await _userManager.CreateAsync(superAdminUser, "Passw0rd@");
                await _userManager.AddToRoleAsync(adminUser, "Admin");
                await _userManager.AddToRoleAsync(superAdminUser, "SuperAdmin");

            }
        }
    }
}
