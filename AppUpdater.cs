using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Velopack;

namespace ChangeScreenResolution_GUI;

internal static class AppUpdater
{
	internal static async Task UpdateMyApp()
	{
		var mgr = new UpdateManager("https://the.place/you-host/updates");

		if (!mgr.IsInstalled) return; 

		// check for new version
		var newVersion = await mgr.CheckForUpdatesAsync();
		if (newVersion == null)
			return; // no update available

		// download new version
		await mgr.DownloadUpdatesAsync(newVersion);

		// install new version and restart app
		mgr.ApplyUpdatesAndRestart(newVersion);
	}
}
