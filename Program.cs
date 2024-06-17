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
#if RELEASE
		VelopackApp.Build().Run();
		try
		{
			Task update = AppUpdater.CheckForUpdateAndApply();
			update.Wait();
		}
		catch (Exception)
		{
			_ = MessageBox.Show(
				"Unable to check for updates; quitting",
				"Update check failed.",
				MessageBoxButtons.OK,
				MessageBoxIcon.Error
				);
			throw;
		}
#endif
		// To customize application configuration such as set high DPI settings or default font,
		// see https://aka.ms/applicationconfiguration.
		ApplicationConfiguration.Initialize();
		Application.Run(new MainWindow());
	}
}