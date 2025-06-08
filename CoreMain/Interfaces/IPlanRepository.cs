using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreMain.Models;

namespace CoreMain.Interfaces
{
    public interface IPlanRepository
    {
        Task<IEnumerable<Plan>> GetAllAsync();
        Task<Plan?> GetByIdAsync(Guid id);
        Task<IEnumerable<Plan>> GetByGymIdAsync(Guid gymId);
        Task<Plan?> CreateAsync(Plan plan);
        Task<Plan?> UpdateAsync(Plan plan);
        Task<bool> DeleteAsync(Guid id);
    }
} 