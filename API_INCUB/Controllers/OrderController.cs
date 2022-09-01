using InClub.Application.Interfaces;
using InClub.Core.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_INCUB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        // GET: api/<OrderController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await orderRepository.GetAllAsync();
            return Ok(data);
        }

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var data = await orderRepository.GetByIdAsync(id);
            if (data == null) return Ok();
            return Ok(data);
        }

        // POST api/<OrderController>
        [HttpPost]
        public async Task<IActionResult> Post(Order order)
        {
            var data = await orderRepository.AddAsync(order);
            return Ok("Pedido generado existosamente #" + data);
        }

        // PUT api/<OrderController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Order order)
        {
            var data = await orderRepository.UpdateAsync(order);
            return Ok(data);
        }

        // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await orderRepository.DeleteAsync(id);
            return Ok(data);
        }
    }
}
