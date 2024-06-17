using Velopack;
using Velopack.Sources;

namespace ChangeScreenResolution_GUI;

internal static class AppUpdater
{
	internal static async Task CheckForUpdateAndApply()
	{
		InfoWindow infoWindow = new InfoWindow(null, "Automatic Update", "Checking for updates, please wait...");
		infoWindow.Show();

		var mgr = new UpdateManager(new GithubSource("https://github.com/IgnacBrychta/ChangeScreenResolution_GUI", null, false));
		var newVersion = await mgr.CheckForUpdatesAsync();
		// check for new version
		if (newVersion == null)
			return; // no update available

		infoWindow.Close();
		DialogResult result = MessageBox.Show(
			"New release is available. Click OK to download and install it, click Cancel to close the program.",
			"New release available",
			MessageBoxButtons.OKCancel,
			MessageBoxIcon.Question
			);
		if (result != DialogResult.OK)
		{
			Environment.Exit(1);
		}

		infoWindow = new InfoWindow(null, "Automatic Update", "Downloading and installing the update, please wait...");

		// download new version
		await mgr.DownloadUpdatesAsync(newVersion);

		// install new version and restart app
		mgr.ApplyUpdatesAndRestart(newVersion);
	}
}
