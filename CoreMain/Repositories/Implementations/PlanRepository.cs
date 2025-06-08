using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreMain.Interfaces;
using CoreMain.Models;
using Supabase;

namespace CoreMain.Repositories.Implementations
{
    public class PlanRepository : IPlanRepository
    {
        private readonly Client _supabase;

        public PlanRepository(Client supabase)
        {
            _supabase = supabase;
        }

        public async Task<IEnumerable<Plan>> GetAllAsync()
        {
            var response = await _supabase
                .From<Plan>()
                .Get();
            return response.Models;
        }

        public async Task<Plan?> GetByIdAsync(Guid id)
        {
            var response = await _supabase
                .From<Plan>()
                .Where(p => p.Id == id)
                .Single();
            return response;
        }

        public async Task<IEnumerable<Plan>> GetByGymIdAsync(Guid gymId)
        {
            var response = await _supabase
                .From<Plan>()
                .Where(p => p.GymId == gymId)
                .Get();
            return response.Models;
        }

        public async Task<Plan?> CreateAsync(Plan plan)
        {
            var response = await _supabase
                .From<Plan>()
                .Insert(plan);
            return response.Model;
        }

        public async Task<Plan?> UpdateAsync(Plan plan)
        {
            var response = await _supabase
                .From<Plan>()
                .Where(p => p.Id == plan.Id)
                .Update(plan);
            return response.Model;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            await _supabase
                .From<Plan>()
                .Where(p => p.Id == id)
                .Delete();
            return true;
        }
    }
} 