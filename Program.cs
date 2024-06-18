using System.Reflection;
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
				"Unable to check for updates, the program must always be up-to-date; quitting",
				"Update check failed.",
				MessageBoxButtons.OK,
				MessageBoxIcon.Error
				);
			throw;
		}
#endif
		ApplicationConfiguration.Initialize();
		Application.Run(new MainWindow());
	}

	private static void ListAllEmbeddedResources()
	{
		var assembly = Assembly.GetExecutingAssembly();
		string[] resourceNames = assembly.GetManifestResourceNames();

		foreach (string resourceName in resourceNames)
		{
			MessageBox.Show($"embedded resource name: {resourceName}"); // Or log the resource names
		}
	}
}