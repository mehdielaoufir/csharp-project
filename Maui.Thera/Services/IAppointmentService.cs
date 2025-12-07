using Maui.Thera.Models;

namespace Maui.Thera.Services
{
    public interface IAppointmentService
    {
        Task<List<Appointment>> GetAllAsync();
        Task<Appointment?> GetAsync(int id);
        Task<int> AddOrUpdateAsync(Appointment a);
        Task DeleteAsync(int id);
    }
}
