using Maui.Thera.Models;

namespace Maui.Thera.Services
{
    public interface IPhysicianService
    {
        Task<List<Physician>> GetAllAsync();
        Task<Physician?> GetAsync(int id);
        Task<int> AddOrUpdateAsync(Physician physician);
        Task DeleteAsync(int id);
    }
}
