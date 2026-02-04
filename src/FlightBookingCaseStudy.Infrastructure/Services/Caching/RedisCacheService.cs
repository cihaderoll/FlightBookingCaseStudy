using FlightBookingCaseStudy.Application.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace FlightBookingCaseStudy.Infrastructure.Services.Caching
{
    public class RedisCacheService<T>(IDistributedCache distributedCache) : ICachingService<T> where T : ICacheable
    {
        public async Task<List<T>?> GetListAsync(string key, CancellationToken cancellationToken = default)
        {
            var cachedData = await distributedCache.GetStringAsync(key, cancellationToken);

            if (string.IsNullOrEmpty(cachedData))
                return default;

            return JsonSerializer.Deserialize<List<T>>(cachedData);
        }

        public async Task<T?> GetAsync(string key, CancellationToken cancellationToken = default)
        {
            var cachedData = await distributedCache.GetStringAsync(key, cancellationToken);

            if (string.IsNullOrEmpty(cachedData))
                return default;

            return JsonSerializer.Deserialize<T>(cachedData);
        }

        public async Task SetListAsync(string key, List<T> value, TimeSpan? expiration = null, CancellationToken cancellationToken = default)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration ?? TimeSpan.FromMinutes(15)
            };

            var serializedData = JsonSerializer.Serialize(value);
            await distributedCache.SetStringAsync(key, serializedData, options, cancellationToken);
        }

        public async Task SetAsync(List<T> valueList, TimeSpan? expiration = null, CancellationToken cancellationToken = default)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration ?? TimeSpan.FromMinutes(15)
            };

            foreach (var value in valueList)
            {
                var serializedData = JsonSerializer.Serialize(value);
                await distributedCache.SetStringAsync(value.FlightId, serializedData, options, cancellationToken);
            }
        }

        public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
        {
            await distributedCache.RemoveAsync(key, cancellationToken);
        }
    }
}
