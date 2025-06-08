using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreMain.Models;

namespace CoreMain.Interfaces
{
    public interface IUserPlanRepository
    {
        Task<IEnumerable<UserPlan>> GetAllAsync();
        Task<UserPlan?> GetByIdAsync(Guid id);
        Task<IEnumerable<UserPlan>> GetByUserIdAsync(Guid userId);
        Task<IEnumerable<UserPlan>> GetByPlanIdAsync(Guid planId);
        Task<UserPlan?> CreateAsync(UserPlan userPlan);
        Task<UserPlan?> UpdateAsync(UserPlan userPlan);
        Task<bool> DeleteAsync(Guid id);
    }
} 