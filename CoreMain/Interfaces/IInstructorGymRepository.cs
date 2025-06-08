using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreMain.Models;

namespace CoreMain.Interfaces
{
    public interface IInstructorGymRepository
    {
        Task<IEnumerable<InstructorGym>> GetAllAsync();
        Task<IEnumerable<InstructorGym>> GetByInstructorIdAsync(Guid instructorId);
        Task<IEnumerable<InstructorGym>> GetByGymIdAsync(Guid gymId);
        Task<InstructorGym?> CreateAsync(InstructorGym instructorGym);
        Task<bool> DeleteAsync(Guid instructorId, Guid gymId);
    }
} 