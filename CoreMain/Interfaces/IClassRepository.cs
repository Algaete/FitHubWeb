using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreMain.Models;

namespace CoreMain.Interfaces
{
    public interface IClassRepository
    {
        Task<IEnumerable<Class>> GetAllAsync();
        Task<Class?> GetByIdAsync(Guid id);
        Task<IEnumerable<Class>> GetByGymIdAsync(Guid gymId);
        Task<IEnumerable<Class>> GetByInstructorIdAsync(Guid instructorId);
        Task<Class?> CreateAsync(Class classEntity);
        Task<Class?> UpdateAsync(Class classEntity);
        Task<bool> DeleteAsync(Guid id);
    }
} 