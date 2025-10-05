using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class ProductSpecificationsParameters
    {
        public int? TypeId { get; set; }
        public int? BrandId { get; set; }
        public ProductSort? Sort { get; set; }
        public int PageIndex { get; set; } = 1;
        private const int maxPageSize = 10;
        private const int defaultPageSize = 5;
        private int _pageSize = defaultPageSize;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > maxPageSize) ? maxPageSize : value;
        }
        public string? Search { get; set; }
    }
    public enum ProductSort
    {
        NameAsc,
        NameDesc,
        PriceAsc,
        PriceDesc
    }
}
