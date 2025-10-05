using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    internal class ProductWithBrandAndTypeSpecifications : Specifications<Product>
    {
        public ProductWithBrandAndTypeSpecifications(ProductSpecificationsParameters parameters) : 
            base(product =>
            (!parameters.BrandId.HasValue || product.BrandId == parameters.BrandId.Value) &&
            (!parameters.TypeId.HasValue || product.TypeId == parameters.TypeId.Value) && 
            (string.IsNullOrWhiteSpace(parameters.Search) || product.Name.ToLower().Contains(parameters.Search.ToLower()))
            )
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
            if (parameters.Sort is not null)
            {
                switch (parameters.Sort)
                {
                    case ProductSort.PriceAsc:
                        AddOrderBy(p => p.Price);
                        break;
                    case ProductSort.PriceDesc:
                        AddOrderByDescending(p => p.Price);
                        break;
                    case ProductSort.NameDesc:
                        AddOrderByDescending(p => p.Name);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }
            ApplyPagination(parameters.PageIndex, parameters.PageSize);
        }
        public ProductWithBrandAndTypeSpecifications(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
    }
}
