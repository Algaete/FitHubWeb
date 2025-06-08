using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreMain.Interfaces;
using CoreMain.Models;
using Supabase;

namespace CoreMain.Repositories.Implementations
{
    public class InstructorRepository : IInstructorRepository
    {
        private readonly Client _supabase;

        public InstructorRepository(Client supabase)
        {
            _supabase = supabase;
        }

        public async Task<IEnumerable<Instructor>> GetAllAsync()
        {
            var response = await _supabase
                .From<Instructor>()
                .Get();
            return response.Models;
        }

        public async Task<Instructor?> GetByIdAsync(Guid id)
        {
            var response = await _supabase
                .From<Instructor>()
                .Where(i => i.Id == id)
                .Single();
            return response;
        }

        public async Task<Instructor?> GetByUserIdAsync(Guid userId)
        {
            var response = await _supabase
                .From<Instructor>()
                .Where(i => i.UserId == userId)
                .Single();
            return response;
        }

        public async Task<IEnumerable<Instructor>> GetByGymIdAsync(Guid gymId)
        {
            var response = await _supabase
                .From<InstructorGym>()
                .Where(ig => ig.GymId == gymId)
                .Get();
            
            var instructorIds = new List<Guid>();
            foreach (var instructorGym in response.Models)
            {
                instructorIds.Add(instructorGym.InstructorId);
            }

            var instructors = new List<Instructor>();
            foreach (var instructorId in instructorIds)
            {
                var instructor = await GetByIdAsync(instructorId);
                if (instructor != null)
                {
                    instructors.Add(instructor);
                }
            }

            return instructors;
        }

        public async Task<Instructor?> CreateAsync(Instructor instructor)
        {
            var response = await _supabase
                .From<Instructor>()
                .Insert(instructor);
            return response.Model;
        }

        public async Task<Instructor?> UpdateAsync(Instructor instructor)
        {
            var response = await _supabase
                .From<Instructor>()
                .Where(i => i.Id == instructor.Id)
                .Update(instructor);
            return response.Model;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            await _supabase
                .From<Instructor>()
                .Where(i => i.Id == id)
                .Delete();
            return true;
        }
    }
} 