using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using CORE.Entities;
using CORE.Interfaces;
using StackExchange.Redis;

namespace INFRASTRUCTURE.Data
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer redis)
        {
            //redis = ConnectionMultiplexer.Connect("redis-11523.c12.us-east-1-4.ec2.redns.redis-cloud.com:11523,password=C8nCpOJaz8d3ujkoqqfoELQa4c5tfYxp");
            _database = redis.GetDatabase();
        }
        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await _database.KeyDeleteAsync(basketId);
        }

        public async Task<CustomerBasket> GetBasketAsync(string basketId)
        {
           var data = await _database.StringGetAsync(basketId);
           return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(data);
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        {
            var created = await _database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket), TimeSpan.FromDays(30 ));

            if(!created) return null;

            return await GetBasketAsync(basket.Id);



           
        }
    }
}