using Basket.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisSE2Project.Repositories.Interfaces
{
    public interface IBasketRepository
    {
        Task<BasketCart> GetBasket(string username);
        Task<BasketCart> UpdateBasket(BasketCart item);
        Task<bool> DeleteBasket(string name);
    }
}
