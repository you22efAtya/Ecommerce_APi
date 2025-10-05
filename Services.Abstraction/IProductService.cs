using Shared;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
    public interface IProductService
    {
        public Task<PaginatedResult<ProductResultDto>> GetAllProductsAsync(ProductSpecificationsParameters parameters);
        public Task<ProductResultDto> GetProductByIdAsync(int id);
        public Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync();
        public Task<IEnumerable<TypeResultDto>> GetAllTypesAsync();

    }
}
