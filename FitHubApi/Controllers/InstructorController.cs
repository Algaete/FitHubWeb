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
    public class InstructorController : ControllerBase
    {
        private readonly IInstructorRepository _instructorRepository;

        public InstructorController(IInstructorRepository instructorRepository)
        {
            _instructorRepository = instructorRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InstructorDto>>> GetAll()
        {
            var instructors = await _instructorRepository.GetAllAsync();
            var instructorDtos = new List<InstructorDto>();
            foreach (var instructor in instructors)
            {
                instructorDtos.Add(ToDto(instructor));
            }
            return Ok(instructorDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InstructorDto>> GetById(Guid id)
        {
            var instructor = await _instructorRepository.GetByIdAsync(id);
            if (instructor == null)
            {
                return NotFound();
            }
            return Ok(ToDto(instructor));
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<InstructorDto>> GetByUserId(Guid userId)
        {
            var instructor = await _instructorRepository.GetByUserIdAsync(userId);
            if (instructor == null)
            {
                return NotFound();
            }
            return Ok(ToDto(instructor));
        }

        [HttpGet("gym/{gymId}")]
        public async Task<ActionResult<IEnumerable<InstructorDto>>> GetByGymId(Guid gymId)
        {
            var instructors = await _instructorRepository.GetByGymIdAsync(gymId);
            var instructorDtos = new List<InstructorDto>();
            foreach (var instructor in instructors)
            {
                instructorDtos.Add(ToDto(instructor));
            }
            return Ok(instructorDtos);
        }

        [HttpPost]
        public async Task<ActionResult<InstructorDto>> Create(Instructor instructor)
        {
            var createdInstructor = await _instructorRepository.CreateAsync(instructor);
            if (createdInstructor == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(GetById), new { id = createdInstructor.Id }, ToDto(createdInstructor));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Instructor instructor)
        {
            if (id != instructor.Id)
            {
                return BadRequest();
            }

            var updatedInstructor = await _instructorRepository.UpdateAsync(instructor);
            if (updatedInstructor == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _instructorRepository.DeleteAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        private static InstructorDto ToDto(Instructor instructor)
        {
            return new InstructorDto
            {
                Id = instructor.Id,
                FullName = instructor.FullName,
                Bio = instructor.Bio,
                Phone = instructor.Phone,
                CreatedAt = instructor.CreatedAt,
                UserId = instructor.UserId
            };
        }
    }
} 