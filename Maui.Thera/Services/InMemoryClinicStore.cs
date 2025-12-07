using System.Linq;
using Maui.Thera.Models;

namespace Maui.Thera.Services
{
    public class InMemoryClinicStore : IPatientService, IPhysicianService, IAppointmentService
    {
        private readonly List<Patient> _patients = new();
        private int _nextPatientId = 1;

        Task<List<Patient>> IPatientService.GetAllAsync()
            => Task.FromResult(_patients.ToList());

        Task<Patient?> IPatientService.GetAsync(int id)
            => Task.FromResult(_patients.FirstOrDefault(p => p.Id == id));

        Task<int> IPatientService.AddOrUpdateAsync(Patient p)
        {
            if (p.Id == 0)
            {
                p.Id = _nextPatientId++;
                _patients.Add(p);
            }
            else
            {
                var idx = _patients.FindIndex(x => x.Id == p.Id);
                if (idx >= 0)
                    _patients[idx] = p;
            }

            return Task.FromResult(p.Id);
        }

        Task IPatientService.DeleteAsync(int id)
        {
            _patients.RemoveAll(p => p.Id == id);
            return Task.CompletedTask;
        }

        private readonly List<Physician> _physicians = new();
        private int _nextPhysicianId = 1;

        Task<List<Physician>> IPhysicianService.GetAllAsync()
            => Task.FromResult(_physicians.ToList());

        Task<Physician?> IPhysicianService.GetAsync(int id)
            => Task.FromResult(_physicians.FirstOrDefault(d => d.Id == id));

        Task<int> IPhysicianService.AddOrUpdateAsync(Physician d)
        {
            if (d.Id == 0)
            {
                d.Id = _nextPhysicianId++;
                _physicians.Add(d);
            }
            else
            {
                var idx = _physicians.FindIndex(x => x.Id == d.Id);
                if (idx >= 0)
                    _physicians[idx] = d;
            }

            return Task.FromResult(d.Id);
        }

        Task IPhysicianService.DeleteAsync(int id)
        {
            _physicians.RemoveAll(d => d.Id == id);
            return Task.CompletedTask;
        }

        private readonly List<Appointment> _appointments = new();
        private int _nextAppointmentId = 1;

        public Task<List<Appointment>> GetAppointmentsAsync()
            => Task.FromResult(_appointments.ToList());

        public Task<Appointment?> GetAppointmentAsync(int id)
            => Task.FromResult(_appointments.FirstOrDefault(a => a.Id == id));

        public Task<int> AddOrUpdateAppointmentAsync(Appointment a)
        {
            if (a.Id == 0)
            {
                a.Id = _nextAppointmentId++;
                _appointments.Add(a);
            }
            else
            {
                var idx = _appointments.FindIndex(x => x.Id == a.Id);
                if (idx >= 0)
                    _appointments[idx] = a;
            }

            return Task.FromResult(a.Id);
        }

        public Task DeleteAppointmentAsync(int id)
        {
            _appointments.RemoveAll(a => a.Id == id);
            return Task.CompletedTask;
        }

        Task<List<Appointment>> IAppointmentService.GetAllAsync()
    => GetAppointmentsAsync();

        Task<Appointment?> IAppointmentService.GetAsync(int id)
            => GetAppointmentAsync(id);

        Task<int> IAppointmentService.AddOrUpdateAsync(Appointment a)
            => AddOrUpdateAppointmentAsync(a);

        Task IAppointmentService.DeleteAsync(int id)
            => DeleteAppointmentAsync(id);

    }
}
