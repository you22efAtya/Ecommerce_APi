global using AutoMapper;
global using Domain.Contracts;
global using Domain.Entities;
global using Services.Abstraction;
global using Shared;
using Services.Specifications;

namespace Services
{
    public class ProductService(IUnitOfWork _unitOfWork,IMapper _mapper) : IProductService
    {
       
        public async Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync()
        {
           var brands =  await _unitOfWork.GetRepository<ProductBrand,int>().GetAllAsync();
           return _mapper.Map<IEnumerable<BrandResultDto>>(brands);
        }

        public async Task<IEnumerable<ProductResultDto>> GetAllProductsAsync(string? sort, int? brandId, int? typeId)
        {
            var products =  await _unitOfWork.GetRepository<Product,int>().GetAllAsync(new ProductWithBrandAndTypeSpecifications(sort,brandId,typeId));
            return _mapper.Map<IEnumerable<ProductResultDto>>(products);
        }

        public async Task<IEnumerable<TypeResultDto>> GetAllTypesAsync()
        {
            var types =  await _unitOfWork.GetRepository<ProductType,int>().GetAllAsync();
            return _mapper.Map<IEnumerable<TypeResultDto>>(types);
        }

        public async Task<ProductResultDto> GetProductByIdAsync(int id)
        {
            var product =  await _unitOfWork.GetRepository<Product,int>().GetByIdAsync(new ProductWithBrandAndTypeSpecifications(id));
            return _mapper.Map<ProductResultDto>(product);
        }
    }
}
