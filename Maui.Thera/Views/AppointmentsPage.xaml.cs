using Maui.Thera.Models;
using Maui.Thera.Services;

namespace Maui.Thera.Views;

public partial class AppointmentsPage : ContentPage
{
    private readonly IAppointmentService _apptSvc;
    private readonly IPatientService _patientSvc;
    private readonly IPhysicianService _physicianSvc;

    public AppointmentsPage(
        IAppointmentService apptSvc,
        IPatientService patientSvc,
        IPhysicianService physicianSvc)
    {
        InitializeComponent();

        _apptSvc = apptSvc;
        _patientSvc = patientSvc;
        _physicianSvc = physicianSvc;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadAsync();
    }

    private async Task LoadAsync()
    {
        var appts = await _apptSvc.GetAllAsync();
        var patients = await _patientSvc.GetAllAsync();
        var physicians = await _physicianSvc.GetAllAsync();

        var displayList = appts.Select(a => new AppointmentDisplay
        {
            Id = a.Id,
            Date = a.Date,
            PatientName = patients.FirstOrDefault(p => p.Id == a.PatientId)?.Name ?? "(Unknown Patient)",
            PhysicianName = physicians.FirstOrDefault(d => d.Id == a.PhysicianId)?.Name ?? "(Unknown Physician)"
        }).ToList();

        AppointmentsList.ItemsSource = displayList;
    }

    private async void OnAddClicked(object sender, EventArgs e)
    {
        var navParams = new Dictionary<string, object>
        {
            { "appointment", new Appointment() }
        };

        await Shell.Current.GoToAsync(nameof(AppointmentFormPage), navParams);
    }

    private async void OnAppointmentSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count == 0)
            return;

        var selected = e.CurrentSelection[0] as AppointmentDisplay;
        if (selected == null)
            return;

        var appt = await _apptSvc.GetAsync(selected.Id);

        var navParams = new Dictionary<string, object>
        {
            { "appointment", appt }
        };

        await Shell.Current.GoToAsync(nameof(AppointmentFormPage), navParams);

        AppointmentsList.SelectedItem = null;
    }

    private async void OnDeleteAppointment(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.CommandParameter is AppointmentDisplay disp)
        {
            bool confirm = await DisplayAlert(
                "Delete Appointment",
                $"Are you sure you want to delete this appointment?",
                "Yes",
                "No");

            if (!confirm)
                return;

            await _apptSvc.DeleteAsync(disp.Id);
            await LoadAsync();
        }
    }

    private class AppointmentDisplay
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string PatientName { get; set; } = "";
        public string PhysicianName { get; set; } = "";
    }
}
