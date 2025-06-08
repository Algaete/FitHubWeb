using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreMain.Models;

namespace CoreMain.Interfaces
{
    public interface IClassBookingRepository
    {
        Task<IEnumerable<ClassBooking>> GetAllAsync();
        Task<ClassBooking?> GetByIdAsync(Guid id);
        Task<IEnumerable<ClassBooking>> GetByUserIdAsync(Guid userId);
        Task<IEnumerable<ClassBooking>> GetByClassScheduleIdAsync(Guid classScheduleId);
        Task<ClassBooking?> CreateAsync(ClassBooking classBooking);
        Task<ClassBooking?> UpdateAsync(ClassBooking classBooking);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> CancelBookingAsync(Guid id);
        Task<bool> MarkAsAttendedAsync(Guid id);
    }
} 