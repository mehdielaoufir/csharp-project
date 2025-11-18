using Maui.Thera.Views;

namespace Maui.Thera;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(PatientFormPage), typeof(PatientFormPage));
	}
}
