using InClub.Application.Interfaces;
using InClub.Core.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_INCUB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        // GET: api/<UserController>
        [HttpGet]
        public IActionResult Get()
        {
            var data = userRepository.GetAllAsync().Result;
            return Ok(data);
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var data = await userRepository.GetByIdAsync(id);
            if (data == null) return Ok();
            return Ok(data);
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult> Add(User user)
        {
            var data = await userRepository.AddAsync(user);
            return Ok(data);
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(User user)
        {
            var data = await userRepository.UpdateAsync(user);
            return Ok(data);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await userRepository.DeleteAsync(id);
            return Ok(data);
        }
    }
}
