using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreMain.Interfaces;
using CoreMain.Models;
using Supabase;

namespace CoreMain.Repositories.Implementations
{
    public class ClassBookingRepository : IClassBookingRepository
    {
        private readonly Client _supabase;

        public ClassBookingRepository(Client supabase)
        {
            _supabase = supabase;
        }

        public async Task<IEnumerable<ClassBooking>> GetAllAsync()
        {
            var response = await _supabase
                .From<ClassBooking>()
                .Get();
            return response.Models;
        }

        public async Task<ClassBooking?> GetByIdAsync(Guid id)
        {
            var response = await _supabase
                .From<ClassBooking>()
                .Where(cb => cb.Id == id)
                .Single();
            return response;
        }

        public async Task<IEnumerable<ClassBooking>> GetByUserIdAsync(Guid userId)
        {
            var response = await _supabase
                .From<ClassBooking>()
                .Where(cb => cb.UserId == userId)
                .Get();
            return response.Models;
        }

        public async Task<IEnumerable<ClassBooking>> GetByClassScheduleIdAsync(Guid classScheduleId)
        {
            var response = await _supabase
                .From<ClassBooking>()
                .Where(cb => cb.ClassScheduleId == classScheduleId)
                .Get();
            return response.Models;
        }

        public async Task<ClassBooking?> CreateAsync(ClassBooking classBooking)
        {
            var response = await _supabase
                .From<ClassBooking>()
                .Insert(classBooking);
            return response.Model;
        }

        public async Task<ClassBooking?> UpdateAsync(ClassBooking classBooking)
        {
            var response = await _supabase
                .From<ClassBooking>()
                .Where(cb => cb.Id == classBooking.Id)
                .Update(classBooking);
            return response.Model;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            await _supabase
                .From<ClassBooking>()
                .Where(cb => cb.Id == id)
                .Delete();
            return true;
        }

        public async Task<bool> CancelBookingAsync(Guid id)
        {
            var booking = await GetByIdAsync(id);
            if (booking == null)
            {
                return false;
            }

            booking.Status = "cancelled";
            booking.CancelledAt = DateTime.UtcNow;

            var response = await _supabase
                .From<ClassBooking>()
                .Where(cb => cb.Id == id)
                .Update(booking);
            
            return response.Model != null;
        }

        public async Task<bool> MarkAsAttendedAsync(Guid id)
        {
            var booking = await GetByIdAsync(id);
            if (booking == null)
            {
                return false;
            }

            booking.Status = "attended";
            booking.AttendedAt = DateTime.UtcNow;

            var response = await _supabase
                .From<ClassBooking>()
                .Where(cb => cb.Id == id)
                .Update(booking);
            
            return response.Model != null;
        }
    }
} 