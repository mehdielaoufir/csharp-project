using Maui.Thera.Models;

namespace Maui.Thera.Services
{
    public interface IPatientService
    {
        Task<List<Patient>> GetAllAsync();
        Task<Patient?> GetAsync(int id);
        Task<int> AddOrUpdateAsync(Patient patient);
        Task DeleteAsync(int id);
    }
}
