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
    private bool _shouldLoadForm = false;

    public Appointment EditingAppointment
    {
        get => _editingAppointment;
        set
        {
            _editingAppointment = value;
            _shouldLoadForm = true;
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
        _ = LoadPickersAsync();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadPickersAsync();

        if (_shouldLoadForm)
        {
            LoadForm();
            _shouldLoadForm = false;
        }
    }

    private async Task LoadPickersAsync()
    {
        PatientPicker.ItemsSource = await _patientSvc.GetAllAsync();
        PhysicianPicker.ItemsSource = await _physicianSvc.GetAllAsync();
    }

    private void LoadForm()
    {
        if (_editingAppointment.Id != 0)
        {
            if (PatientPicker.ItemsSource is List<Patient> patients)
                PatientPicker.SelectedItem = patients.FirstOrDefault(p => p.Id == _editingAppointment.PatientId);

            if (PhysicianPicker.ItemsSource is List<Physician> physicians)
                PhysicianPicker.SelectedItem = physicians.FirstOrDefault(d => d.Id == _editingAppointment.PhysicianId);

            DatePicker.Date = _editingAppointment.Date.Date;
            TimePicker.Time = _editingAppointment.Date.TimeOfDay;
        }
        else
        {
            DatePicker.Date = DateTime.Today;
            TimePicker.Time = new TimeSpan(9, 0, 0);
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

        DateTime date = DatePicker.Date;
        TimeSpan time = TimePicker.Time;
        DateTime apptDateTime = date.Date + time;

        if (apptDateTime.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday)
        {
            await DisplayAlert("Invalid Time", "Appointments must be Monday–Friday.", "OK");
            return;
        }

        if (apptDateTime.Hour < 8 || apptDateTime.Hour >= 17)
        {
            await DisplayAlert("Invalid Time", "Appointments must be between 8am and 5pm.", "OK");
            return;
        }

        var allAppts = await _apptSvc.GetAllAsync();
        bool conflict = allAppts.Any(a =>
            a.PhysicianId == selectedPhysician.Id &&
            a.Date == apptDateTime &&
            a.Id != _editingAppointment.Id);

        if (conflict)
        {
            await DisplayAlert("Conflict",
                "This physician already has an appointment at that time.",
                "OK");
            return;
        }

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
