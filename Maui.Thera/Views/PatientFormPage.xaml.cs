using Maui.Thera.Models;
using Maui.Thera.Services;

namespace Maui.Thera.Views;

public partial class PatientFormPage : ContentPage
{
    private readonly IPatientService _svc;
    private Patient _editingPatient;

    public PatientFormPage(IPatientService svc, Patient? patient = null)
    {
        InitializeComponent();
        _svc = svc;
        _editingPatient = patient ?? new Patient();


        if (patient != null)
        {
            NameEntry.Text = patient.Name;
            AddressEntry.Text = patient.Address;
            BirthdatePicker.Date = patient.Birthdate;
            RaceEntry.Text = patient.Race;
            GenderEntry.Text = patient.Gender;
            DiagnosesEditor.Text = patient.Diagnoses;
            PrescriptionsEditor.Text = patient.Prescriptions;
        }
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
