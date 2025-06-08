using System.Collections.Generic;
using System.Threading.Tasks;
using CoreMain.Models;

namespace CoreMain.Interfaces
{
    public interface IGymRepository
    {
        Task<IEnumerable<Gym>> GetAllAsync();
        Task<Gym?> GetByIdAsync(string id);
        Task<Gym?> CreateAsync(Gym gym);
        Task<Gym?> UpdateAsync(string id, Gym gym);
        Task DeleteAsync(string id);
    }
} 