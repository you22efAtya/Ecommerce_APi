using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
    public interface IProductService
    {
        public Task<IEnumerable<ProductResultDto>> GetAllProductsAsync(string? sort, int? brandId, int? typeId);
        public Task<ProductResultDto> GetProductByIdAsync(int id);
        public Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync();
        public Task<IEnumerable<TypeResultDto>> GetAllTypesAsync();

    }
}
