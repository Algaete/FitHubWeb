using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreMain.Interfaces;
using CoreMain.Models;
using Supabase;

namespace CoreMain.Repositories.Implementations
{
    public class UserPlanRepository : IUserPlanRepository
    {
        private readonly Client _supabase;

        public UserPlanRepository(Client supabase)
        {
            _supabase = supabase;
        }

        public async Task<IEnumerable<UserPlan>> GetAllAsync()
        {
            var response = await _supabase
                .From<UserPlan>()
                .Get();
            return response.Models;
        }

        public async Task<UserPlan?> GetByIdAsync(Guid id)
        {
            var response = await _supabase
                .From<UserPlan>()
                .Where(up => up.Id == id)
                .Single();
            return response;
        }

        public async Task<IEnumerable<UserPlan>> GetByUserIdAsync(Guid userId)
        {
            var response = await _supabase
                .From<UserPlan>()
                .Where(up => up.UserId == userId)
                .Get();
            return response.Models;
        }

        public async Task<IEnumerable<UserPlan>> GetByPlanIdAsync(Guid planId)
        {
            var response = await _supabase
                .From<UserPlan>()
                .Where(up => up.PlanId == planId)
                .Get();
            return response.Models;
        }

        public async Task<UserPlan?> CreateAsync(UserPlan userPlan)
        {
            var response = await _supabase
                .From<UserPlan>()
                .Insert(userPlan);
            return response.Model;
        }

        public async Task<UserPlan?> UpdateAsync(UserPlan userPlan)
        {
            var response = await _supabase
                .From<UserPlan>()
                .Where(up => up.Id == userPlan.Id)
                .Update(userPlan);
            return response.Model;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            await _supabase
                .From<UserPlan>()
                .Where(up => up.Id == id)
                .Delete();
            return true;
        }
    }
} 