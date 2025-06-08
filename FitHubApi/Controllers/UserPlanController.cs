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
    public class UserPlanController : ControllerBase
    {
        private readonly IUserPlanRepository _userPlanRepository;

        public UserPlanController(IUserPlanRepository userPlanRepository)
        {
            _userPlanRepository = userPlanRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserPlanDto>>> GetAll()
        {
            var userPlans = await _userPlanRepository.GetAllAsync();
            var userPlanDtos = new List<UserPlanDto>();
            foreach (var userPlan in userPlans)
            {
                userPlanDtos.Add(ToDto(userPlan));
            }
            return Ok(userPlanDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserPlanDto>> GetById(Guid id)
        {
            var userPlan = await _userPlanRepository.GetByIdAsync(id);
            if (userPlan == null)
            {
                return NotFound();
            }
            return Ok(ToDto(userPlan));
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<UserPlanDto>>> GetByUserId(Guid userId)
        {
            var userPlans = await _userPlanRepository.GetByUserIdAsync(userId);
            var userPlanDtos = new List<UserPlanDto>();
            foreach (var userPlan in userPlans)
            {
                userPlanDtos.Add(ToDto(userPlan));
            }
            return Ok(userPlanDtos);
        }

        [HttpGet("plan/{planId}")]
        public async Task<ActionResult<IEnumerable<UserPlanDto>>> GetByPlanId(Guid planId)
        {
            var userPlans = await _userPlanRepository.GetByPlanIdAsync(planId);
            var userPlanDtos = new List<UserPlanDto>();
            foreach (var userPlan in userPlans)
            {
                userPlanDtos.Add(ToDto(userPlan));
            }
            return Ok(userPlanDtos);
        }

        [HttpPost]
        public async Task<ActionResult<UserPlanDto>> Create(UserPlan userPlan)
        {
            var createdUserPlan = await _userPlanRepository.CreateAsync(userPlan);
            if (createdUserPlan == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(GetById), new { id = createdUserPlan.Id }, ToDto(createdUserPlan));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UserPlan userPlan)
        {
            if (id != userPlan.Id)
            {
                return BadRequest();
            }

            var updatedUserPlan = await _userPlanRepository.UpdateAsync(userPlan);
            if (updatedUserPlan == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _userPlanRepository.DeleteAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        private static UserPlanDto ToDto(UserPlan userPlan)
        {
            return new UserPlanDto
            {
                Id = userPlan.Id,
                UserId = userPlan.UserId,
                PlanId = userPlan.PlanId,
                StartDate = userPlan.StartDate,
                EndDate = userPlan.EndDate,
                Status = userPlan.Status,
                AutoRenew = userPlan.AutoRenew,
                CreatedAt = userPlan.CreatedAt,
                UpdatedAt = userPlan.UpdatedAt
            };
        }
    }
} 