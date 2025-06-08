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
    public class PlanController : ControllerBase
    {
        private readonly IPlanRepository _planRepository;

        public PlanController(IPlanRepository planRepository)
        {
            _planRepository = planRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlanDto>>> GetAll()
        {
            var plans = await _planRepository.GetAllAsync();
            var planDtos = new List<PlanDto>();
            foreach (var plan in plans)
            {
                planDtos.Add(ToDto(plan));
            }
            return Ok(planDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PlanDto>> GetById(Guid id)
        {
            var plan = await _planRepository.GetByIdAsync(id);
            if (plan == null)
            {
                return NotFound();
            }
            return Ok(ToDto(plan));
        }

        [HttpGet("gym/{gymId}")]
        public async Task<ActionResult<IEnumerable<PlanDto>>> GetByGymId(Guid gymId)
        {
            var plans = await _planRepository.GetByGymIdAsync(gymId);
            var planDtos = new List<PlanDto>();
            foreach (var plan in plans)
            {
                planDtos.Add(ToDto(plan));
            }
            return Ok(planDtos);
        }

        [HttpPost]
        public async Task<ActionResult<PlanDto>> Create(Plan plan)
        {
            var createdPlan = await _planRepository.CreateAsync(plan);
            if (createdPlan == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(GetById), new { id = createdPlan.Id }, ToDto(createdPlan));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Plan plan)
        {
            if (id != plan.Id)
            {
                return BadRequest();
            }

            var updatedPlan = await _planRepository.UpdateAsync(plan);
            if (updatedPlan == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _planRepository.DeleteAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        private static PlanDto ToDto(Plan plan)
        {
            return new PlanDto
            {
                Id = plan.Id,
                GymId = plan.GymId,
                Name = plan.Name,
                Description = plan.Description,
                Price = plan.Price,
                DurationDays = plan.DurationDays,
                CreatedAt = plan.CreatedAt,
                ValidityPeriod = plan.ValidityPeriod
            };
        }
    }
} 