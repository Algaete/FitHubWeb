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
    public class ClassBookingController : ControllerBase
    {
        private readonly IClassBookingRepository _classBookingRepository;

        public ClassBookingController(IClassBookingRepository classBookingRepository)
        {
            _classBookingRepository = classBookingRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClassBookingDto>>> GetAll()
        {
            var classBookings = await _classBookingRepository.GetAllAsync();
            var classBookingDtos = new List<ClassBookingDto>();
            foreach (var classBooking in classBookings)
            {
                classBookingDtos.Add(ToDto(classBooking));
            }
            return Ok(classBookingDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClassBookingDto>> GetById(Guid id)
        {
            var classBooking = await _classBookingRepository.GetByIdAsync(id);
            if (classBooking == null)
            {
                return NotFound();
            }
            return Ok(ToDto(classBooking));
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<ClassBookingDto>>> GetByUserId(Guid userId)
        {
            var classBookings = await _classBookingRepository.GetByUserIdAsync(userId);
            var classBookingDtos = new List<ClassBookingDto>();
            foreach (var classBooking in classBookings)
            {
                classBookingDtos.Add(ToDto(classBooking));
            }
            return Ok(classBookingDtos);
        }

        [HttpGet("schedule/{classScheduleId}")]
        public async Task<ActionResult<IEnumerable<ClassBookingDto>>> GetByClassScheduleId(Guid classScheduleId)
        {
            var classBookings = await _classBookingRepository.GetByClassScheduleIdAsync(classScheduleId);
            var classBookingDtos = new List<ClassBookingDto>();
            foreach (var classBooking in classBookings)
            {
                classBookingDtos.Add(ToDto(classBooking));
            }
            return Ok(classBookingDtos);
        }

        [HttpPost]
        public async Task<ActionResult<ClassBookingDto>> Create(ClassBooking classBooking)
        {
            var createdClassBooking = await _classBookingRepository.CreateAsync(classBooking);
            if (createdClassBooking == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(GetById), new { id = createdClassBooking.Id }, ToDto(createdClassBooking));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, ClassBooking classBooking)
        {
            if (id != classBooking.Id)
            {
                return BadRequest();
            }

            var updatedClassBooking = await _classBookingRepository.UpdateAsync(classBooking);
            if (updatedClassBooking == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _classBookingRepository.DeleteAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPost("{id}/cancel")]
        public async Task<IActionResult> CancelBooking(Guid id)
        {
            var result = await _classBookingRepository.CancelBookingAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPost("{id}/attend")]
        public async Task<IActionResult> MarkAsAttended(Guid id)
        {
            var result = await _classBookingRepository.MarkAsAttendedAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        private static ClassBookingDto ToDto(ClassBooking classBooking)
        {
            return new ClassBookingDto
            {
                Id = classBooking.Id,
                UserId = classBooking.UserId,
                ClassScheduleId = classBooking.ClassScheduleId,
                Status = classBooking.Status,
                BookedAt = classBooking.BookedAt,
                CancelledAt = classBooking.CancelledAt,
                AttendedAt = classBooking.AttendedAt
            };
        }
    }
} 