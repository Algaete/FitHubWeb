using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreMain.Models;
using CoreMain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FitHubApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ManagerController : ControllerBase
    {
        private readonly IGymRepository _gymRepository;

        public ManagerController(IGymRepository gymRepository)
        {
            _gymRepository = gymRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Gym>>> GetAllGyms()
        {
            var gyms = await _gymRepository.GetAllAsync();
            return Ok(gyms);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Gym?>> GetGymById(Guid id)
        {
            var gym = await _gymRepository.GetByIdAsync(id);
            if (gym == null)
            {
                return NotFound();
            }
            return Ok(gym);
        }

        [HttpPost]
        public async Task<ActionResult<Gym?>> CreateGym(Gym gym)
        {
            var createdGym = await _gymRepository.CreateAsync(gym);
            return CreatedAtAction(nameof(GetGymById), new { id = createdGym?.Id }, createdGym);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGym(Guid id, Gym gym)
        {
            if (id != gym.Id)
            {
                return BadRequest();
            }
            var updatedGym = await _gymRepository.UpdateAsync(gym);
            if (updatedGym == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGym(Guid id)
        {
            var result = await _gymRepository.DeleteAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
