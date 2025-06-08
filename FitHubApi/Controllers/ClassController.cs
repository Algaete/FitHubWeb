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
    public class ClassController : ControllerBase
    {
        private readonly IClassRepository _classRepository;

        public ClassController(IClassRepository classRepository)
        {
            _classRepository = classRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClassDto>>> GetAll()
        {
            var classes = await _classRepository.GetAllAsync();
            var classDtos = new List<ClassDto>();
            foreach (var classEntity in classes)
            {
                classDtos.Add(ToDto(classEntity));
            }
            return Ok(classDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClassDto>> GetById(Guid id)
        {
            var classEntity = await _classRepository.GetByIdAsync(id);
            if (classEntity == null)
            {
                return NotFound();
            }
            return Ok(ToDto(classEntity));
        }

        [HttpGet("gym/{gymId}")]
        public async Task<ActionResult<IEnumerable<ClassDto>>> GetByGymId(Guid gymId)
        {
            var classes = await _classRepository.GetByGymIdAsync(gymId);
            var classDtos = new List<ClassDto>();
            foreach (var classEntity in classes)
            {
                classDtos.Add(ToDto(classEntity));
            }
            return Ok(classDtos);
        }

        [HttpGet("instructor/{instructorId}")]
        public async Task<ActionResult<IEnumerable<ClassDto>>> GetByInstructorId(Guid instructorId)
        {
            var classes = await _classRepository.GetByInstructorIdAsync(instructorId);
            var classDtos = new List<ClassDto>();
            foreach (var classEntity in classes)
            {
                classDtos.Add(ToDto(classEntity));
            }
            return Ok(classDtos);
        }

        [HttpPost]
        public async Task<ActionResult<ClassDto>> Create(Class classEntity)
        {
            var createdClass = await _classRepository.CreateAsync(classEntity);
            if (createdClass == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(GetById), new { id = createdClass.Id }, ToDto(createdClass));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Class classEntity)
        {
            if (id != classEntity.Id)
            {
                return BadRequest();
            }

            var updatedClass = await _classRepository.UpdateAsync(classEntity);
            if (updatedClass == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _classRepository.DeleteAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        private static ClassDto ToDto(Class classEntity)
        {
            return new ClassDto
            {
                Id = classEntity.Id,
                GymId = classEntity.GymId,
                InstructorId = classEntity.InstructorId,
                Title = classEntity.Title,
                Description = classEntity.Description,
                StartTime = classEntity.StartTime,
                Capacity = classEntity.Capacity,
                CreatedAt = classEntity.CreatedAt
            };
        }
    }
} 