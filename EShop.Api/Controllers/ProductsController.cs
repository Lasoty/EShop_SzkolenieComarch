using EShop.Data.Entities;
using EShop.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IKeyRepository<Product, int> productsRepository;

        public ProductsController(IKeyRepository<Product, int> productsRepository)
        {
            this.productsRepository = productsRepository;
        }
        // GET: api/<ProductsController>
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return productsRepository.GetAll().ToList();
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public async Task<Product> Get(int id)
        {
            return await productsRepository.GetById(id);
        }

        // POST api/<ProductsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Product value)
        {
            await productsRepository.AddAsync(value);
            return Ok();
        }

        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Product value)
        {
            if (productsRepository.GetAll().Any(x => x.Id == id))
            {
                await productsRepository.UpdateAsync(value);
                return Ok();
            }

            return BadRequest();
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (productsRepository.GetAll().Any(x => x.Id == id))
            {
                await productsRepository.DeleteAsync(id);
                return Ok();
            }

            return NotFound();
        }
    }
}
