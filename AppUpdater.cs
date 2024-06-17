using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Velopack;

namespace ChangeScreenResolution_GUI;

internal static class AppUpdater
{
	internal static async Task CheckForUpdateAndApply()
	{
		var mgr = new UpdateManager("https://github.com/IgnacBrychta/ChangeScreenResolution_GUI");
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

		// download new version
		await mgr.DownloadUpdatesAsync(newVersion);

		// install new version and restart app
		mgr.ApplyUpdatesAndRestart(newVersion);
	}
}
