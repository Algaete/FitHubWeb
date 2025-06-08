using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreMain.Interfaces;
using CoreMain.Models;
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

        private GymDto ToDto(Gym gym)
        {
            return new GymDto
            {
                Id = gym.Id,
                Name = gym.Name,
                Address = gym.Address,
                Phone = gym.Phone,
                CreatedAt = gym.CreatedAt,
                Logo = gym.Logo,
                City = gym.City,
                Country = gym.Country,
                State = gym.State,
                Description = gym.Description,
                Photo1 = gym.Photo1,
                Photo2 = gym.Photo2,
                Photo3 = gym.Photo3,
                LongDescription = gym.LongDescription,
                UrlGym = gym.UrlGym
            };
        }

        [HttpGet("gyms")]
        public async Task<ActionResult<IEnumerable<GymDto>>> GetAllGyms()
        {
            try
            {
                var gyms = await _gymRepository.GetAllAsync();
                return Ok(gyms.Where(g => g != null).Select(ToDto));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpGet("gyms/{id}")]
        public async Task<ActionResult<GymDto>> GetGymById(string id)
        {
            try
            {
                var gym = await _gymRepository.GetByIdAsync(id);
                if (gym == null)
                {
                    return NotFound($"Gimnasio con ID {id} no encontrado");
                }
                return Ok(ToDto(gym));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPost("gyms")]
        public async Task<ActionResult<GymDto>> CreateGym(Gym gym)
        {
            try
            {
                if (gym == null)
                {
                    return BadRequest("Los datos del gimnasio son requeridos");
                }

                var createdGym = await _gymRepository.CreateAsync(gym);
                return CreatedAtAction(nameof(GetGymById), new { id = createdGym.Id }, ToDto(createdGym));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPut("gyms/{id}")]
        public async Task<ActionResult<GymDto>> UpdateGym(string id, Gym gym)
        {
            try
            {
                if (gym == null)
                {
                    return BadRequest("Los datos del gimnasio son requeridos");
                }

                var existingGym = await _gymRepository.GetByIdAsync(id);
                if (existingGym == null)
                {
                    return NotFound($"Gimnasio con ID {id} no encontrado");
                }

                var updatedGym = await _gymRepository.UpdateAsync(id, gym);
                return Ok(ToDto(updatedGym));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpDelete("gyms/{id}")]
        public async Task<ActionResult> DeleteGym(string id)
        {
            try
            {
                var existingGym = await _gymRepository.GetByIdAsync(id);
                if (existingGym == null)
                {
                    return NotFound($"Gimnasio con ID {id} no encontrado");
                }

                await _gymRepository.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}
