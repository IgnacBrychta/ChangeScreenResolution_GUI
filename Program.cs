using Velopack;

namespace ChangeScreenResolution_GUI;

internal static class Program
{
	/// <summary>
	///  The main entry point for the application.
	/// </summary>
	[STAThread]
	static void Main()
	{
		// VelopackApp.Build().Run();
		// Task update = AppUpdater.UpdateMyApp();
		// update.Wait();
		// To customize application configuration such as set high DPI settings or default font,
		// see https://aka.ms/applicationconfiguration.
		ApplicationConfiguration.Initialize();
		Application.Run(new MainWindow());
	}
}