using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreMain.Interfaces;
using CoreMain.Models;
using Supabase;

namespace CoreMain.Repositories.Implementations
{
    public class InstructorGymRepository : IInstructorGymRepository
    {
        private readonly Client _supabase;

        public InstructorGymRepository(Client supabase)
        {
            _supabase = supabase;
        }

        public async Task<IEnumerable<InstructorGym>> GetAllAsync()
        {
            var response = await _supabase
                .From<InstructorGym>()
                .Get();
            return response.Models;
        }

        public async Task<IEnumerable<InstructorGym>> GetByInstructorIdAsync(Guid instructorId)
        {
            var response = await _supabase
                .From<InstructorGym>()
                .Where(ig => ig.InstructorId == instructorId)
                .Get();
            return response.Models;
        }

        public async Task<IEnumerable<InstructorGym>> GetByGymIdAsync(Guid gymId)
        {
            var response = await _supabase
                .From<InstructorGym>()
                .Where(ig => ig.GymId == gymId)
                .Get();
            return response.Models;
        }

        public async Task<InstructorGym?> CreateAsync(InstructorGym instructorGym)
        {
            var response = await _supabase
                .From<InstructorGym>()
                .Insert(instructorGym);
            return response.Model;
        }

        public async Task<bool> DeleteAsync(Guid instructorId, Guid gymId)
        {
            await _supabase
                .From<InstructorGym>()
                .Where(ig => ig.InstructorId == instructorId && ig.GymId == gymId)
                .Delete();
            return true;
        }
    }
} 