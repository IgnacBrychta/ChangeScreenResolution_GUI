using Velopack;
using Velopack.Sources;

namespace ChangeScreenResolution_GUI;

internal static class AppUpdater
{
	internal static async Task CheckForUpdateAndApply()
	{
		var cancellationTokenSource = new CancellationTokenSource();
		var cancellationToken = cancellationTokenSource.Token;

		Task updateInfo = Task.Run(() =>
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return;
			}

			MessageBox.Show(
				"Checking for updates, please wait...",
				"Automatic Update",
				MessageBoxButtons.OK,
				MessageBoxIcon.Information
			);
		}, cancellationToken);

		var mgr = new UpdateManager(new GithubSource("https://github.com/IgnacBrychta/ChangeScreenResolution_GUI", null, false));
		var newVersion = await mgr.CheckForUpdatesAsync();
		// check for new version
		if (newVersion == null)
			return; // no update available

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

		cancellationTokenSource.Cancel();

		_ = new Task(() =>
		{
			_ = MessageBox.Show(
				"Downloading and installing the update, please wait...",
				"Automatic Update",
				MessageBoxButtons.OK,
				MessageBoxIcon.Information
				);
		});

		// download new version
		await mgr.DownloadUpdatesAsync(newVersion);

		// install new version and restart app
		mgr.ApplyUpdatesAndRestart(newVersion);
	}
}
