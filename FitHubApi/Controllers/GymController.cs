using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreMain.Interfaces;
using CoreMain.Models;
using Microsoft.AspNetCore.Mvc;

namespace FitHubApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GymController : ControllerBase
    {
        private readonly IGymRepository _gymRepository;

        public GymController(IGymRepository gymRepository)
        {
            _gymRepository = gymRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GymDto>>> GetAll()
        {
            var gyms = await _gymRepository.GetAllAsync();
            var gymDtos = new List<GymDto>();
            foreach (var gym in gyms)
            {
                gymDtos.Add(ToDto(gym));
            }
            return Ok(gymDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GymDto>> GetById(string id)
        {
            var gym = await _gymRepository.GetByIdAsync(id);
            if (gym == null)
            {
                return NotFound();
            }
            return Ok(ToDto(gym));
        }

        [HttpPost]
        public async Task<ActionResult<GymDto>> Create(Gym gym)
        {
            var createdGym = await _gymRepository.CreateAsync(gym);
            if (createdGym == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(GetById), new { id = createdGym.Id }, ToDto(createdGym));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Gym gym)
        {
            if (id != gym.Id)
            {
                return BadRequest();
            }

            var updatedGym = await _gymRepository.UpdateAsync(id, gym);
            if (updatedGym == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _gymRepository.DeleteAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        private static GymDto ToDto(Gym gym)
        {
            return new GymDto
            {
                Id = gym.Id,
                Name = gym.Name,
                Description = gym.Description,
                Address = gym.Address,
                Phone = gym.Phone,
                Email = gym.Email,
                CreatedAt = gym.CreatedAt
            };
        }
    }
} 