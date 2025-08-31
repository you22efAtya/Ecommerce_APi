
using System.Text.Json;

namespace Persistance.Data.DataSeeding
{
    public class DpIntializer : IDbIntializer
    {
        private readonly AppDbContext _dbContext;

        public DpIntializer(AppDbContext dbContext)
        {
            _dbContext = dbContext;
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
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
