using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Basket.API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedisSE2Project.Repositories.Interfaces;

namespace Basket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketItemController : ControllerBase
    {
        private readonly IBasketRepository _repository;

        public BasketItemController(IBasketRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(BasketCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<BasketCart>> GetBasketItem(string name)
        {
            var group = await _repository.GetBasket(name);
            return Ok(group ?? new BasketCart(name));
        }

        [HttpPost]
        [ProducesResponseType(typeof(BasketCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<BasketCart>> UpdateItem(BasketCart item)
        {
            var updatedGroup = await _repository.UpdateBasket(item);
            return Ok(updatedGroup);
        }

        [HttpDelete("{name}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> DeleteBasket(string name)
        {
            return Ok(await _repository.DeleteBasket(name));
        }
    }
}
