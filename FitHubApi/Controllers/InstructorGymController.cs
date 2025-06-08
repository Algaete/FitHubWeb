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
    public class InstructorGymController : ControllerBase
    {
        private readonly IInstructorGymRepository _instructorGymRepository;

        public InstructorGymController(IInstructorGymRepository instructorGymRepository)
        {
            _instructorGymRepository = instructorGymRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InstructorGymDto>>> GetAll()
        {
            var instructorGyms = await _instructorGymRepository.GetAllAsync();
            var instructorGymDtos = new List<InstructorGymDto>();
            foreach (var instructorGym in instructorGyms)
            {
                instructorGymDtos.Add(ToDto(instructorGym));
            }
            return Ok(instructorGymDtos);
        }

        [HttpGet("instructor/{instructorId}")]
        public async Task<ActionResult<IEnumerable<InstructorGymDto>>> GetByInstructorId(Guid instructorId)
        {
            var instructorGyms = await _instructorGymRepository.GetByInstructorIdAsync(instructorId);
            var instructorGymDtos = new List<InstructorGymDto>();
            foreach (var instructorGym in instructorGyms)
            {
                instructorGymDtos.Add(ToDto(instructorGym));
            }
            return Ok(instructorGymDtos);
        }

        [HttpGet("gym/{gymId}")]
        public async Task<ActionResult<IEnumerable<InstructorGymDto>>> GetByGymId(Guid gymId)
        {
            var instructorGyms = await _instructorGymRepository.GetByGymIdAsync(gymId);
            var instructorGymDtos = new List<InstructorGymDto>();
            foreach (var instructorGym in instructorGyms)
            {
                instructorGymDtos.Add(ToDto(instructorGym));
            }
            return Ok(instructorGymDtos);
        }

        [HttpPost]
        public async Task<ActionResult<InstructorGymDto>> Create(InstructorGym instructorGym)
        {
            var createdInstructorGym = await _instructorGymRepository.CreateAsync(instructorGym);
            if (createdInstructorGym == null)
            {
                return BadRequest();
            }
            return Ok(ToDto(createdInstructorGym));
        }

        [HttpDelete("{instructorId}/{gymId}")]
        public async Task<IActionResult> Delete(Guid instructorId, Guid gymId)
        {
            var result = await _instructorGymRepository.DeleteAsync(instructorId, gymId);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        private static InstructorGymDto ToDto(InstructorGym instructorGym)
        {
            return new InstructorGymDto
            {
                InstructorId = instructorGym.InstructorId,
                GymId = instructorGym.GymId
            };
        }
    }
} 