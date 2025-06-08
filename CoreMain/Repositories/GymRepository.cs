using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using CoreMain.Models;
using Supabase;
using CoreMain.Interfaces;

namespace CoreMain.Repositories
{
    public class GymRepository : IGymRepository
    {
        private readonly Supabase.Client _supabaseClient;

        public GymRepository(Supabase.Client supabaseClient)
        {
            _supabaseClient = supabaseClient;
        }

        public async Task<IEnumerable<Gym>> GetAllAsync()
        {
            var response = await _supabaseClient.From<Gym>().Get();
            return response.Models;
        }

        public async Task<Gym?> GetByIdAsync(Guid id)
        {
            var response = await _supabaseClient.From<Gym>().Where(x => x.Id == id).Get();
            return response.Models.FirstOrDefault();
        }

        public async Task<Gym?> CreateAsync(Gym gym)
        {
            var response = await _supabaseClient.From<Gym>().Insert(gym);
            return response.Models.FirstOrDefault();
        }

        public async Task<Gym?> UpdateAsync(Gym gym)
        {
            var response = await _supabaseClient.From<Gym>().Where(x => x.Id == gym.Id).Update(gym);
            return response.Models.FirstOrDefault();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var response = await _supabaseClient.From<Gym>().Where(x => x.Id == id).Delete();
            return response.Models.Any();
        }
    }
}