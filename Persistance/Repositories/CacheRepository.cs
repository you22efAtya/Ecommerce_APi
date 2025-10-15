using StackExchange.Redis;
using System.Text.Json;
namespace Persistance.Repositories
{
    public class CacheRepository(IConnectionMultiplexer connectionMultiplexer) : ICacheRepository
    {
        private readonly IDatabase _database = connectionMultiplexer.GetDatabase();

        public async Task<string?> GetAsync(string key)
        {
            var value = await _database.StringGetAsync(key);
            return value.IsNullOrEmpty ? default : value;
        }

        public async Task SetAsync(string key, object value, TimeSpan duration)
        {
            var serializedObj = JsonSerializer.Serialize(value);
            await _database.StringSetAsync(key, serializedObj, duration);

        }
    }
}
