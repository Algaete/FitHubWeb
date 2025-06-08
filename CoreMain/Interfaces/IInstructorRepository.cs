using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreMain.Models;

namespace CoreMain.Interfaces
{
    public interface IInstructorRepository
    {
        Task<IEnumerable<Instructor>> GetAllAsync();
        Task<Instructor?> GetByIdAsync(Guid id);
        Task<Instructor?> GetByUserIdAsync(Guid userId);
        Task<IEnumerable<Instructor>> GetByGymIdAsync(Guid gymId);
        Task<Instructor?> CreateAsync(Instructor instructor);
        Task<Instructor?> UpdateAsync(Instructor instructor);
        Task<bool> DeleteAsync(Guid id);
    }
} 