using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using CoreMain.Interfaces;
using CoreMain.Models;
using Supabase;

namespace CoreMain.Repositories.Implementations
{
    public class GymRepository : IGymRepository
    {
        private readonly Client _supabase;

        public GymRepository(Client supabase)
        {
            _supabase = supabase;
        }

        public async Task<IEnumerable<Gym>> GetAllAsync()
        {
            var response = await _supabase
                .From<Gym>()
                .Get();
            return response.Models;
        }

        public async Task<Gym> GetByIdAsync(string id)
        {
            var response = await _supabase
                .From<Gym>()
                .Where(g => g.Id == id)
                .Get();
            return response.Models.FirstOrDefault();
        }

        public async Task<Gym> CreateAsync(Gym gym)
        {
            gym.CreatedAt = DateTime.UtcNow;
            
            var response = await _supabase
                .From<Gym>()
                .Insert(gym);
            return response.Models.FirstOrDefault();
        }

        public async Task<Gym> UpdateAsync(string id, Gym gym)
        {
            var response = await _supabase
                .From<Gym>()
                .Where(g => g.Id == id)
                .Update(gym);
            return response.Models.FirstOrDefault();
        }

        public async Task DeleteAsync(string id)
        {
            await _supabase
                .From<Gym>()
                .Where(g => g.Id == id)
                .Delete();
        }
    }
} 