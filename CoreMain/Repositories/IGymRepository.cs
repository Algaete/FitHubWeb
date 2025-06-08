using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreMain.Models;

namespace CoreMain.Repositories
{
    public interface IGymRepository
    {
        Task<IEnumerable<Gym>> GetAllAsync();
        Task<Gym?> GetByIdAsync(Guid id);
        Task<Gym?> CreateAsync(Gym gym);
        Task<Gym?> UpdateAsync(Gym gym);
        Task<bool> DeleteAsync(Guid id);
    }
} 