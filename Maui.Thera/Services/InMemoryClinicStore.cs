using Maui.Thera.Models;

namespace Maui.Thera.Services
{
    public class InMemoryClinicStore : IPatientService
    {
        private readonly List<Patient> _patients = new();
        private int _nextPatientId = 1;

        public Task<List<Patient>> GetAllAsync()
            => Task.FromResult(_patients.ToList());

        public Task<Patient?> GetAsync(int id)
            => Task.FromResult(_patients.FirstOrDefault(p => p.Id == id));

        public Task<int> AddOrUpdateAsync(Patient p)
        {
            if (p.Id == 0)
            {
                p.Id = _nextPatientId++;
                _patients.Add(p);
            }
            else
            {
                var idx = _patients.FindIndex(x => x.Id == p.Id);
                if (idx >= 0) _patients[idx] = p;
            }
            return Task.FromResult(p.Id);
        }

        public Task DeleteAsync(int id)
        {
            _patients.RemoveAll(p => p.Id == id);
            return Task.CompletedTask;
        }
    }
}

