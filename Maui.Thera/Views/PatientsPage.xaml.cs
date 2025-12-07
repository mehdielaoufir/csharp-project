using Maui.Thera.Models;
using Maui.Thera.Services;

namespace Maui.Thera.Views;

public partial class PatientsPage : ContentPage
{
    private readonly IPatientService _svc;

    public PatientsPage(IPatientService svc)
    {
        InitializeComponent();
        _svc = svc;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadAsync();
    }

    private async Task LoadAsync()
    {
        var items = await _svc.GetAllAsync();
        PatientsList.ItemsSource = items;
    }

    private async void OnAddClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(PatientFormPage));
    }

    private async void OnPatientSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count == 0)
            return;

        var selected = e.CurrentSelection[0] as Patient;
        if (selected == null)
            return;

        var navParams = new Dictionary<string, object>
    {
        { "patient", selected }
    };

        await Shell.Current.GoToAsync(nameof(PatientFormPage), navParams);

        PatientsList.SelectedItem = null;
    }

    private async void OnDeletePatient(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is Patient patient)
        {
            bool confirm = await DisplayAlert(
                "Delete Patient",
                $"Are you sure you want to delete {patient.Name}?",
                "Yes",
                "No");

            if (!confirm)
                return;

            await _svc.DeleteAsync(patient.Id);
            await LoadAsync();
        }
    }


}
