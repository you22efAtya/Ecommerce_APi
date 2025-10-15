using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
    public interface ICacheService
    {
        Task<string?> GetCachedValueAsync(string cacheKey);
        Task SetCacheValueAsync(string cacheKey, object value, TimeSpan duration);
    }
}
