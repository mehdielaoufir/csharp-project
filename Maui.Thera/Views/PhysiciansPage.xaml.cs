using Maui.Thera.Models;
using Maui.Thera.Services;

namespace Maui.Thera.Views;

public partial class PhysiciansPage : ContentPage
{
    private readonly IPhysicianService _svc;

    public PhysiciansPage(IPhysicianService svc)
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
        PhysiciansList.ItemsSource = items;
    }

    private async void OnAddClicked(object sender, EventArgs e)
    {
        var parameters = new Dictionary<string, object>
        {
            { "physician", new Physician() }
        };

        await Shell.Current.GoToAsync(nameof(PhysicianFormPage), parameters);
    }

    private async void OnPhysicianSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count == 0)
            return;

        var selected = e.CurrentSelection[0] as Physician;
        if (selected == null)
            return;

        var parameters = new Dictionary<string, object>
        {
            { "physician", selected }
        };

        await Shell.Current.GoToAsync(nameof(PhysicianFormPage), parameters);

        PhysiciansList.SelectedItem = null;
    }

    private async void OnDeletePhysician(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.CommandParameter is Physician p)
        {
            bool confirm = await DisplayAlert(
                "Delete Physician",
                $"Are you sure you want to delete Dr. {p.Name}?",
                "Yes",
                "No");

            if (!confirm)
                return;

            await _svc.DeleteAsync(p.Id);
            await LoadAsync();
        }
    }
}
