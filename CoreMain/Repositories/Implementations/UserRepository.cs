using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreMain.Interfaces;
using CoreMain.Models;
using Supabase;

namespace CoreMain.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly Client _supabase;

        public UserRepository(Client supabase)
        {
            _supabase = supabase;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var response = await _supabase
                .From<User>()
                .Get();
            return response.Models;
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            var response = await _supabase
                .From<User>()
                .Where(u => u.Id == id)
                .Single();
            return response;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            var response = await _supabase
                .From<User>()
                .Where(u => u.Email == email)
                .Single();
            return response;
        }

        public async Task<User?> CreateAsync(User user)
        {
            var response = await _supabase
                .From<User>()
                .Insert(user);
            return response.Model;
        }

        public async Task<User?> UpdateAsync(User user)
        {
            var response = await _supabase
                .From<User>()
                .Where(u => u.Id == user.Id)
                .Update(user);
            return response.Model;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            await _supabase
                .From<User>()
                .Where(u => u.Id == id)
                .Delete();
            return true;
        }
    }
} 