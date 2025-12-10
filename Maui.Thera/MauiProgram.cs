using Microsoft.Extensions.Logging;
using Maui.Thera.Services;

namespace Maui.Thera;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddSingleton<InMemoryClinicStore>();

		builder.Services.AddSingleton<IPatientService>(sp =>
			sp.GetRequiredService<InMemoryClinicStore>());
		builder.Services.AddSingleton<IPhysicianService>(sp =>
			sp.GetRequiredService<InMemoryClinicStore>());
		builder.Services.AddSingleton<IAppointmentService>(sp =>
			sp.GetRequiredService<InMemoryClinicStore>());

		builder.Services.AddTransient<Maui.Thera.Views.PatientsPage>();
		builder.Services.AddTransient<Maui.Thera.Views.PatientFormPage>();
		builder.Services.AddTransient<Maui.Thera.Views.PhysiciansPage>();
		builder.Services.AddTransient<Maui.Thera.Views.PhysicianFormPage>();
		builder.Services.AddTransient<Maui.Thera.Views.AppointmentsPage>();
		builder.Services.AddTransient<Maui.Thera.Views.AppointmentFormPage>();

		builder.Services.AddSingleton(new WebRequestHandler("http://localhost:5038/"));

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
