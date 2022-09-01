using InClub.Application.Interfaces;
using InClub.Core.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_INCUB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepository;

        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        // GET: api/<ProductController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await productRepository.GetAllAsync();
            return Ok(data);
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var data = await productRepository.GetByIdAsync(id);
            if (data == null) return Ok();
            return Ok(data);
        }

        // POST api/<ProductController>
        [HttpPost]
        public async Task<IActionResult> Post(Product product)
        {
            var data = await productRepository.AddAsync(product);
            return Ok(data);
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Product product)
        {
            var data = await productRepository.UpdateAsync(product);
            return Ok(data);
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await productRepository.DeleteAsync(id);
            return Ok(data);
        }
    }
}
