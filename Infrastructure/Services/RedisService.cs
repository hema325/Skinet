using Core.Interfaces.Services;
using StackExchange.Redis;
using System.Text.Json;

namespace Infrastructure.Services
{
    internal class RedisService : ICacheService
    {
        private readonly IDatabase _database;

        public RedisService(IConnectionMultiplexer connectionMultiplexer)
        {
            _database = connectionMultiplexer.GetDatabase();
        }

        public async Task SetAsync<TValue>(string key, TValue value, TimeSpan? expiry = null, bool isCamelCase = false)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = isCamelCase ? JsonNamingPolicy.CamelCase : null
            };

            await _database.StringSetAsync(key, JsonSerializer.Serialize(value, options), expiry);
        }

        public async Task<TValue?> GetAsync<TValue>(string key)
        {
            var result = await _database.StringGetAsync(key);

            if (result.IsNullOrEmpty)
                return default;

            return JsonSerializer.Deserialize<TValue>(result.ToString());
        }

        public async Task DeleteAsync(string key)
        {
            await _database.KeyDeleteAsync(key);
        }
    }
}
