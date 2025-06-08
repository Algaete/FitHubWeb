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
    public class ClassScheduleController : ControllerBase
    {
        private readonly IClassScheduleRepository _scheduleRepository;

        public ClassScheduleController(IClassScheduleRepository scheduleRepository)
        {
            _scheduleRepository = scheduleRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClassScheduleDto>>> GetAll()
        {
            var schedules = await _scheduleRepository.GetAllAsync();
            var scheduleDtos = new List<ClassScheduleDto>();
            foreach (var schedule in schedules)
            {
                scheduleDtos.Add(ToDto(schedule));
            }
            return Ok(scheduleDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClassScheduleDto>> GetById(Guid id)
        {
            var schedule = await _scheduleRepository.GetByIdAsync(id);
            if (schedule == null)
            {
                return NotFound();
            }
            return Ok(ToDto(schedule));
        }

        [HttpGet("class/{classId}")]
        public async Task<ActionResult<IEnumerable<ClassScheduleDto>>> GetByClassId(Guid classId)
        {
            var schedules = await _scheduleRepository.GetByClassIdAsync(classId);
            var scheduleDtos = new List<ClassScheduleDto>();
            foreach (var schedule in schedules)
            {
                scheduleDtos.Add(ToDto(schedule));
            }
            return Ok(scheduleDtos);
        }

        [HttpGet("instructor/{instructorId}")]
        public async Task<ActionResult<IEnumerable<ClassScheduleDto>>> GetByInstructorId(Guid instructorId)
        {
            var schedules = await _scheduleRepository.GetByInstructorIdAsync(instructorId);
            var scheduleDtos = new List<ClassScheduleDto>();
            foreach (var schedule in schedules)
            {
                scheduleDtos.Add(ToDto(schedule));
            }
            return Ok(scheduleDtos);
        }

        [HttpGet("daterange")]
        public async Task<ActionResult<IEnumerable<ClassScheduleDto>>> GetByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var schedules = await _scheduleRepository.GetByDateRangeAsync(startDate, endDate);
            var scheduleDtos = new List<ClassScheduleDto>();
            foreach (var schedule in schedules)
            {
                scheduleDtos.Add(ToDto(schedule));
            }
            return Ok(scheduleDtos);
        }

        [HttpPost]
        public async Task<ActionResult<ClassScheduleDto>> Create(ClassSchedule schedule)
        {
            var createdSchedule = await _scheduleRepository.CreateAsync(schedule);
            if (createdSchedule == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(GetById), new { id = createdSchedule.Id }, ToDto(createdSchedule));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, ClassSchedule schedule)
        {
            if (id != schedule.Id)
            {
                return BadRequest();
            }

            var updatedSchedule = await _scheduleRepository.UpdateAsync(schedule);
            if (updatedSchedule == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _scheduleRepository.DeleteAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        private static ClassScheduleDto ToDto(ClassSchedule schedule)
        {
            return new ClassScheduleDto
            {
                Id = schedule.Id,
                ClassId = schedule.ClassId,
                SessionStart = schedule.SessionStart,
                SessionEnd = schedule.SessionEnd,
                InstructorId = schedule.InstructorId,
                Capacity = schedule.Capacity,
                CreatedAt = schedule.CreatedAt
            };
        }
    }
} 