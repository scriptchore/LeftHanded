using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using CORE.Interfaces;
using StackExchange.Redis;

namespace INFRASTRUCTURE.Services
{
    public class ResponseCacheService : IResponseCacheService
    {
        private readonly IDatabase _database;
        public ResponseCacheService(IConnectionMultiplexer redis)
        {
            //redis = ConnectionMultiplexer.Connect("redis-11523.c12.us-east-1-4.ec2.redns.redis-cloud.com:11523,password=C8nCpOJaz8d3ujkoqqfoELQa4c5tfYxp");
            _database = redis.GetDatabase();
        }

        public async Task CacheResponseAsync(string cachekey, object response, TimeSpan timeToLive)
        {
           if (response == null)
            {
                return;
            }

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var serialisedResponse = JsonSerializer.Serialize(response, options);

            await _database.StringSetAsync(cachekey, serialisedResponse, timeToLive);

        }

        public async Task<string> GetCachedResponseAsync(string cachekey)
        {
            var cachedResponse = await _database.StringGetAsync(cachekey);

            if (cachedResponse.IsNullOrEmpty)
            {
                return null;
            }

            return cachedResponse;
        }
    }
}