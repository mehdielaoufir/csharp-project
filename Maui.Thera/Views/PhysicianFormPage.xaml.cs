using Maui.Thera.Models;
using Maui.Thera.Services;

namespace Maui.Thera.Views;

[QueryProperty(nameof(EditingPhysician), "physician")]
public partial class PhysicianFormPage : ContentPage
{
    private readonly WebRequestHandler _api;

    public PhysicianFormPage(WebRequestHandler api)
    {
        InitializeComponent();
        _api = api;
    }

    private Physician _editingPhysician = new();
    public Physician EditingPhysician
    {
        get => _editingPhysician;
        set
        {
            _editingPhysician = value;
            LoadForm();
        }
    }

    private void LoadForm()
    {
        FirstNameEntry.Text = _editingPhysician.FirstName;
        LastNameEntry.Text = _editingPhysician.LastName;
        SpecialtyEntry.Text = _editingPhysician.Specialty;
        PhoneEntry.Text = _editingPhysician.Phone;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        _editingPhysician.FirstName = FirstNameEntry.Text;
        _editingPhysician.LastName = LastNameEntry.Text;
        _editingPhysician.Specialty = SpecialtyEntry.Text;
        _editingPhysician.Phone = PhoneEntry.Text;

        if (_editingPhysician.Id == 0)
        {
            await _api.PostAsync<Physician>("api/physicians", _editingPhysician);
        }
        else
        {
            await _api.PutAsync($"api/physicians/{_editingPhysician.Id}", _editingPhysician);
        }

        await Shell.Current.GoToAsync("..");
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}
