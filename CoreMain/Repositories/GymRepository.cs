using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreMain.Models;
using Supabase;
using Supabase.Postgrest.Constants;

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
            var response = await _supabaseClient.From<Gym>().Filter("id", Operator.Equals, id).Get();
            return response.Models.Count > 0 ? response.Models[0] : null;
        }

        public async Task<Gym?> CreateAsync(Gym gym)
        {
            var response = await _supabaseClient.From<Gym>().Insert(gym);
            return response.Models.Count > 0 ? response.Models[0] : null;
        }

        public async Task<Gym?> UpdateAsync(Gym gym)
        {
            var response = await _supabaseClient.From<Gym>().Update(gym);
            return response.Models.Count > 0 ? response.Models[0] : null;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var response = await _supabaseClient.From<Gym>().Filter("id", Operator.Equals, id).Delete();
            return response.Models.Count > 0;
        }
    }
} 