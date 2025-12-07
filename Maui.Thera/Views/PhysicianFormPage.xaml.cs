using Maui.Thera.Models;
using Maui.Thera.Services;

namespace Maui.Thera.Views;

[QueryProperty(nameof(EditingPhysician), "physician")]
public partial class PhysicianFormPage : ContentPage
{
    private readonly IPhysicianService _svc;

    public PhysicianFormPage(IPhysicianService svc)
    {
        InitializeComponent();
        _svc = svc;
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
        NameEntry.Text = _editingPhysician.Name;
        LicenseEntry.Text = _editingPhysician.LicenseNumber;
        GradDatePicker.Date = _editingPhysician.GraduationDate == default
            ? DateTime.Today.AddYears(-5)
            : _editingPhysician.GraduationDate;
        SpecializationEntry.Text = _editingPhysician.Specialization;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        _editingPhysician.Name = NameEntry.Text;
        _editingPhysician.LicenseNumber = LicenseEntry.Text;
        _editingPhysician.GraduationDate = GradDatePicker.Date;
        _editingPhysician.Specialization = SpecializationEntry.Text;

        await _svc.AddOrUpdateAsync(_editingPhysician);
        await Shell.Current.GoToAsync("..");
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}
