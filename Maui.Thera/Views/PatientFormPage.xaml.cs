using System.Collections.Generic;
using Maui.Thera.Models;
using Maui.Thera.Services;

namespace Maui.Thera.Views;

[QueryProperty(nameof(EditingPatient), "patient")]
public partial class PatientFormPage : ContentPage
{
    private readonly IPatientService _svc;
    private Patient _editingPatient = new Patient();

    public Patient EditingPatient
    {
        get => _editingPatient;
        set
        {
            _editingPatient = value ?? new Patient();
            LoadPatientToForm();
        }
    }

    public PatientFormPage(IPatientService svc)
    {
        InitializeComponent();
        _svc = svc;
    }

    private void LoadPatientToForm()
    {
        NameEntry.Text = _editingPatient.Name;
        AddressEntry.Text = _editingPatient.Address;
        BirthdatePicker.Date = _editingPatient.Birthdate;
        RaceEntry.Text = _editingPatient.Race;
        GenderEntry.Text = _editingPatient.Gender;
        DiagnosesEditor.Text = _editingPatient.Diagnoses;
        PrescriptionsEditor.Text = _editingPatient.Prescriptions;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        _editingPatient.Name = NameEntry.Text;
        _editingPatient.Address = AddressEntry.Text;
        _editingPatient.Birthdate = BirthdatePicker.Date;
        _editingPatient.Race = RaceEntry.Text;
        _editingPatient.Gender = GenderEntry.Text;
        _editingPatient.Diagnoses = DiagnosesEditor.Text;
        _editingPatient.Prescriptions = PrescriptionsEditor.Text;

        await _svc.AddOrUpdateAsync(_editingPatient);

        await Shell.Current.GoToAsync("..");
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}
