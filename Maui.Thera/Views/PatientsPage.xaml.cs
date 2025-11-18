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
}
