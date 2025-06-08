using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreMain.Interfaces;
using CoreMain.Models;
using Supabase;

namespace CoreMain.Repositories.Implementations
{
    public class ClassScheduleRepository : IClassScheduleRepository
    {
        private readonly Client _supabase;

        public ClassScheduleRepository(Client supabase)
        {
            _supabase = supabase;
        }

        public async Task<IEnumerable<ClassSchedule>> GetAllAsync()
        {
            var response = await _supabase
                .From<ClassSchedule>()
                .Get();
            return response.Models;
        }

        public async Task<ClassSchedule?> GetByIdAsync(Guid id)
        {
            var response = await _supabase
                .From<ClassSchedule>()
                .Where(s => s.Id == id)
                .Single();
            return response;
        }

        public async Task<IEnumerable<ClassSchedule>> GetByClassIdAsync(Guid classId)
        {
            var response = await _supabase
                .From<ClassSchedule>()
                .Where(s => s.ClassId == classId)
                .Get();
            return response.Models;
        }

        public async Task<IEnumerable<ClassSchedule>> GetByInstructorIdAsync(Guid instructorId)
        {
            var response = await _supabase
                .From<ClassSchedule>()
                .Where(s => s.InstructorId == instructorId)
                .Get();
            return response.Models;
        }

        public async Task<IEnumerable<ClassSchedule>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var response = await _supabase
                .From<ClassSchedule>()
                .Where(s => s.SessionStart >= startDate && s.SessionEnd <= endDate)
                .Get();
            return response.Models;
        }

        public async Task<ClassSchedule?> CreateAsync(ClassSchedule schedule)
        {
            var response = await _supabase
                .From<ClassSchedule>()
                .Insert(schedule);
            return response.Model;
        }

        public async Task<ClassSchedule?> UpdateAsync(ClassSchedule schedule)
        {
            var response = await _supabase
                .From<ClassSchedule>()
                .Where(s => s.Id == schedule.Id)
                .Update(schedule);
            return response.Model;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            await _supabase
                .From<ClassSchedule>()
                .Where(s => s.Id == id)
                .Delete();
            return true;
        }
    }
} 