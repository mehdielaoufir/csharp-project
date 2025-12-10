using Thera.Api.Models;

namespace Thera.Api.Services
{
    public class InMemoryPhysicianRepository : IPhysicianRepository
    {
        private readonly List<Physician> _physicians = new();
        private int _nextId = 1;

        public InMemoryPhysicianRepository()
        {
            _physicians.Add(new Physician
            {
                Id = _nextId++,
                FirstName = "John",
                LastName = "Doe",
                Specialty = "Cardiology",
                Phone = "555-1111"
            });

            _physicians.Add(new Physician
            {
                Id = _nextId++,
                FirstName = "Jane",
                LastName = "Smith",
                Specialty = "Dermatology",
                Phone = "555-2222"
            });
        }

        public IEnumerable<Physician> GetAll() => _physicians;

        public Physician? GetById(int id) =>
            _physicians.FirstOrDefault(p => p.Id == id);

        public IEnumerable<Physician> Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return _physicians;

            query = query.ToLower();

            return _physicians.Where(p =>
                p.FirstName.ToLower().Contains(query) ||
                p.LastName.ToLower().Contains(query) ||
                p.Specialty.ToLower().Contains(query));
        }

        public Physician Create(Physician physician)
        {
            physician.Id = _nextId++;
            _physicians.Add(physician);
            return physician;
        }

        public bool Update(int id, Physician updated)
        {
            var existing = GetById(id);
            if (existing == null)
                return false;

            existing.FirstName = updated.FirstName;
            existing.LastName = updated.LastName;
            existing.Specialty = updated.Specialty;
            existing.Phone = updated.Phone;

            return true;
        }

        public bool Delete(int id)
        {
            var existing = GetById(id);
            if (existing == null)
                return false;

            _physicians.Remove(existing);
            return true;
        }
    }
}
