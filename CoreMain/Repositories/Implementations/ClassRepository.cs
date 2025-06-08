using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreMain.Interfaces;
using CoreMain.Models;
using Supabase;

namespace CoreMain.Repositories.Implementations
{
    public class ClassRepository : IClassRepository
    {
        private readonly Client _supabase;

        public ClassRepository(Client supabase)
        {
            _supabase = supabase;
        }

        public async Task<IEnumerable<Class>> GetAllAsync()
        {
            var response = await _supabase
                .From<Class>()
                .Get();
            return response.Models;
        }

        public async Task<Class?> GetByIdAsync(Guid id)
        {
            var response = await _supabase
                .From<Class>()
                .Where(c => c.Id == id)
                .Single();
            return response;
        }

        public async Task<IEnumerable<Class>> GetByGymIdAsync(Guid gymId)
        {
            var response = await _supabase
                .From<Class>()
                .Where(c => c.GymId == gymId)
                .Get();
            return response.Models;
        }

        public async Task<IEnumerable<Class>> GetByInstructorIdAsync(Guid instructorId)
        {
            var response = await _supabase
                .From<Class>()
                .Where(c => c.InstructorId == instructorId)
                .Get();
            return response.Models;
        }

        public async Task<Class?> CreateAsync(Class classEntity)
        {
            var response = await _supabase
                .From<Class>()
                .Insert(classEntity);
            return response.Model;
        }

        public async Task<Class?> UpdateAsync(Class classEntity)
        {
            var response = await _supabase
                .From<Class>()
                .Where(c => c.Id == classEntity.Id)
                .Update(classEntity);
            return response.Model;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            await _supabase
                .From<Class>()
                .Where(c => c.Id == id)
                .Delete();
            return true;
        }
    }
} 