using Basket.API.Entities;
using Newtonsoft.Json;
using RedisSE2Project.Data.Interfaces;
using RedisSE2Project.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IBasketContext _context;

        public BasketRepository(IBasketContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteBasket(string name)
        {
            return await _context.Redis.KeyDeleteAsync(name);
        }

        public async Task<BasketCart> GetBasket(string username)
        {
            var get = await _context.Redis.StringGetAsync(username);
            if (get.IsNullOrEmpty)
            {
                return null;
            }
            try
            {
                return JsonConvert.DeserializeObject<BasketCart>(get);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<BasketCart> UpdateBasket(BasketCart item)
        {
            var updated = await _context.Redis.StringSetAsync(item.Username, JsonConvert.SerializeObject(item));
            if (!updated)
            {
                return null;
            }

            return await GetBasket(item.Username);
        }
    }
}
