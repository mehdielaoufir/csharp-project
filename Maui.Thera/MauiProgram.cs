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

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
