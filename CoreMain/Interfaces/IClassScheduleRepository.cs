using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreMain.Models;

namespace CoreMain.Interfaces
{
    public interface IClassScheduleRepository
    {
        Task<IEnumerable<ClassSchedule>> GetAllAsync();
        Task<ClassSchedule?> GetByIdAsync(Guid id);
        Task<IEnumerable<ClassSchedule>> GetByClassIdæAsync(Guid classId);
        Task<IEnumerable<ClassSchedule>> GetByInstructorIdAsync(Guid instructorId);
        Task<IEnumerable<ClassSchedule>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<ClassSchedule?> CreateAsync(ClassSchedule schedule);
        Task<ClassSchedule?> UpdateAsync(ClassSchedule schedule);
        Task<bool> DeleteAsync(Guid id);
    }
} 