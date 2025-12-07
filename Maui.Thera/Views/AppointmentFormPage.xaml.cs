using Maui.Thera.Models;
using Maui.Thera.Services;

namespace Maui.Thera.Views;

[QueryProperty(nameof(EditingAppointment), "appointment")]
public partial class AppointmentFormPage : ContentPage
{
    private readonly IAppointmentService _apptSvc;
    private readonly IPatientService _patientSvc;
    private readonly IPhysicianService _physicianSvc;

    private Appointment _editingAppointment = new();

    public Appointment EditingAppointment
    {
        get => _editingAppointment;
        set
        {
            _editingAppointment = value;
            LoadForm();
        }
    }

    public AppointmentFormPage(
        IAppointmentService apptSvc,
        IPatientService patientSvc,
        IPhysicianService physicianSvc)
    {
        InitializeComponent();
        _apptSvc = apptSvc;
        _patientSvc = patientSvc;
        _physicianSvc = physicianSvc;

        LoadPickers();
    }

    private async void LoadPickers()
    {
        PatientPicker.ItemsSource = await _patientSvc.GetAllAsync();
        PhysicianPicker.ItemsSource = await _physicianSvc.GetAllAsync();
    }

    private void LoadForm()
    {
        if (_editingAppointment.Id != 0)
        {
            // Set patient
            var patients = PatientPicker.ItemsSource as List<Patient>;
            PatientPicker.SelectedItem = patients?.FirstOrDefault(p => p.Id == _editingAppointment.PatientId);

            // Set physician
            var physicians = PhysicianPicker.ItemsSource as List<Physician>;
            PhysicianPicker.SelectedItem = physicians?.FirstOrDefault(d => d.Id == _editingAppointment.PhysicianId);

            // Date/time
            DatePicker.Date = _editingAppointment.Date.Date;
            TimePicker.Time = _editingAppointment.Date.TimeOfDay;
        }
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (PatientPicker.SelectedItem is not Patient selectedPatient ||
            PhysicianPicker.SelectedItem is not Physician selectedPhysician)
        {
            await DisplayAlert("Error", "Please select a patient and physician.", "OK");
            return;
        }

        // Build the exact appointment DateTime
        DateTime date = DatePicker.Date;
        TimeSpan time = TimePicker.Time;
        DateTime apptDateTime = date.Date + time;

        // Rule: Must be Monday–Friday
        if (apptDateTime.DayOfWeek == DayOfWeek.Saturday ||
            apptDateTime.DayOfWeek == DayOfWeek.Sunday)
        {
            await DisplayAlert("Invalid Time", "Appointments can only be Monday–Friday.", "OK");
            return;
        }

        // Rule: Must be between 8am and 5pm
        if (apptDateTime.Hour < 8 || apptDateTime.Hour >= 17)
        {
            await DisplayAlert("Invalid Time", "Appointments must be between 8am and 5pm.", "OK");
            return;
        }

        // Rule: No double booking for physician
        var allAppts = await _apptSvc.GetAllAsync();
        bool doubleBooked = allAppts.Any(a =>
            a.PhysicianId == selectedPhysician.Id &&
            a.Date == apptDateTime &&
            a.Id != _editingAppointment.Id);

        if (doubleBooked)
        {
            await DisplayAlert("Conflict",
                "This physician already has an appointment at that time.",
                "OK");
            return;
        }

        // Save the appointment
        _editingAppointment.PatientId = selectedPatient.Id;
        _editingAppointment.PhysicianId = selectedPhysician.Id;
        _editingAppointment.Date = apptDateTime;

        await _apptSvc.AddOrUpdateAsync(_editingAppointment);

        await Shell.Current.GoToAsync("..");
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}
