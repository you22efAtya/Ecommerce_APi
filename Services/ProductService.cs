global using AutoMapper;
global using Domain.Contracts;
global using Domain.Entities;
global using Services.Abstraction;
global using Shared;
using Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Services.Specifications;
using Shared.Dtos;

namespace Services
{
    
    public class ProductService(IUnitOfWork _unitOfWork,IMapper _mapper) : IProductService
    {
       
        public async Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync()
        {
           var brands =  await _unitOfWork.GetRepository<ProductBrand,int>().GetAllAsync();
           return _mapper.Map<IEnumerable<BrandResultDto>>(brands);
        }

        public async Task<PaginatedResult<ProductResultDto>> GetAllProductsAsync(ProductSpecificationsParameters parameters)
        {
            var products =  await _unitOfWork.GetRepository<Product,int>().GetAllAsync(new ProductWithBrandAndTypeSpecifications(parameters));
            var totalCount = await _unitOfWork.GetRepository<Product,int>().CountAsync(new ProductCountSpecifications(parameters));
            var productsResult =  _mapper.Map<IEnumerable<ProductResultDto>>(products);
            var result = new PaginatedResult<ProductResultDto>(
                products.Count(),
                parameters.PageIndex,
                totalCount,
                productsResult
                );
            return result;
        }

        public async Task<IEnumerable<TypeResultDto>> GetAllTypesAsync()
        {
            var types =  await _unitOfWork.GetRepository<ProductType,int>().GetAllAsync();
            return _mapper.Map<IEnumerable<TypeResultDto>>(types);
        }

        public async Task<ProductResultDto> GetProductByIdAsync(int id)
        {
            var product =  await _unitOfWork.GetRepository<Product,int>().GetByIdAsync(new ProductWithBrandAndTypeSpecifications(id));
            return product is null ? throw new ProductNotFoundException(id) : _mapper.Map<ProductResultDto>(product);
        }
    }
}
