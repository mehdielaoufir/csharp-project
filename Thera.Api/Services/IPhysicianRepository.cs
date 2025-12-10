using Thera.Api.Models;

namespace Thera.Api.Services
{
    public interface IPhysicianRepository
    {
        IEnumerable<Physician> GetAll();
        Physician? GetById(int id);
        IEnumerable<Physician> Search(string query);
        Physician Create(Physician physician);
        bool Update(int id, Physician updated);
        bool Delete(int id);
    }
}
