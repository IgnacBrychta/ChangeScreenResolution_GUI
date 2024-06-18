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
		bool recentlyUpdated = false;
#if RELEASE
		VelopackApp.Build()
			.WithRestarted((arg) => { recentlyUpdated = true; })
			.Run();
		try
		{
			if (!recentlyUpdated)
			{
				Task update = AppUpdater.CheckForUpdateAndApply();
				update.Wait();
			}
		}
		catch (Exception)
		{
			_ = MessageBox.Show(
				"Unable to check for updates, the program should always be up-to-date.",
				"Update check failed.",
				MessageBoxButtons.OK,
				MessageBoxIcon.Error
				);
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