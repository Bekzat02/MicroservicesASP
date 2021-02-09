using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Catalog.API.Models;
using Catalog.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Catalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        private readonly ILogger<CatalogController> _logger;

        public CatalogController(IProductRepository repository, ILogger<CatalogController> logger)
        {
            _productRepository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var list = await _productRepository.GetProducts();
            return Ok(list ?? new List<Product>()
            {
                new Product(){ Name="Lol",Category="gachimuchi",
                    Summary="dee",Description="dwokodwk",ImageFile="rfrfr",Price=777}
            });
        }
        
        [HttpPost]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            await _productRepository.Create(product);
            return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
        }

        [HttpGet("{id:length(24)}", Name = "GetProduct")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> GetProduct(string id)
        {
            var getProductById = await _productRepository.GetProduct(id);
            return getProductById;
        }

        [Route("[action]/{category}")]
        [HttpGet]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByCategory(string category)
        {
            var list = await _productRepository.GetProductByCategory(category);
            return list.ToList();
        }
        
        [HttpPut]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProduct([FromBody] Product value)
        {
            bool updating = await _productRepository.Update(value);
            if (updating == false)
            {
                throw new InvalidOperationException();
            }
            return Ok(value);
        }

        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProductById(string id)
        {
            await _productRepository.Delete(id);
            return Ok(id);
        }
    }
}
