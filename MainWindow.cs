using System.Collections.Immutable;
using System.Diagnostics;
using System.Reflection;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.Text.RegularExpressions;

namespace ChangeScreenResolution_GUI;

public partial class MainWindow : Form
{
	string nircmdExeFullPath = "";
	string iconPath = "";
	string csrExeFullPath = "";
	const int bpc = 32;
	ScreenResolution defaultResolution;
	int defaultRefreshRate = 60;
	internal List<ScreenResolution> availableResolutions;
	const string tempFolderName = "ChangeScreenResolution_GUI_Temp";
	const int noItemSelectedIndex = -1;
	public MainWindow()
	{
		InitializeComponent();

		ScreenResolution? res = GetDefaultScreenResolution() ?? throw new Exception("Primary screen not detected");
		defaultResolution = res;

		string tempPath = Path.GetTempPath();
		nircmdExeFullPath = Path.Combine(tempPath, "nircmd.exe");
		iconPath = Path.Combine(tempPath, "app_icon.ico");
		csrExeFullPath = Path.Combine(tempPath, "taukenkorb_ChangeScreenResolution.exe");

		// Extract the embedded resources
		ExtractResource("ChangeScreenResolution_GUI.nircmd.nircmd.exe", nircmdExeFullPath);
		ExtractResource("ChangeScreenResolution_GUI.nircmd.taukenkorb_ChangeScreenResolution.exe", csrExeFullPath);
		ExtractResource("ChangeScreenResolution_GUI.res.cmis logo.ico", iconPath);
		
		button_apply.Click += Button_apply_Click;
		comboBox_resolution.SelectedIndexChanged += ComboBox_resolution_SelectedIndexChanged;
		Icon = new Icon(iconPath);

		availableResolutions = GetAllAvailableScreenResolutions();

		FillInComboBoxes();
		ResetChoicesToDefaultValues();
	}

	private void ComboBox_resolution_SelectedIndexChanged(object? sender, EventArgs e)
	{
		if (comboBox_resolution.SelectedIndex == noItemSelectedIndex) return;

		ScreenResolution screenResolution = availableResolutions[comboBox_resolution.SelectedIndex];

		comboBox_refreshRate.Items.Clear();
		foreach(int refreshRate in screenResolution.RefreshRates)
		{
			comboBox_refreshRate.Items.Add($"{refreshRate} Hz");
		}
	}

	private List<ScreenResolution> GetAllAvailableScreenResolutions()
	{
		List<ScreenResolution> result = new List<ScreenResolution>();
		string displays =
#if DEBUG
@"
Connected display devices:
  [0] \\.\DISPLAY1                  Microsoft Hyper-V Video
      \\.\DISPLAY1\Monitor0           Generic PnP Monitor
          Settings: 0x0 0bit @0Hz default

  [1] \\.\DISPLAY2                  NVIDIA GeForce RTX 3060 Ti
  [2] \\.\DISPLAY3                  NVIDIA GeForce RTX 3060 Ti
  [3] \\.\DISPLAY4                  NVIDIA GeForce RTX 3060 Ti
  [4] \\.\DISPLAY5                  NVIDIA GeForce RTX 3060 Ti
  [5] \\.\DISPLAY6                  Parsec Virtual Display Adapter
      \\.\DISPLAY6\Monitor0           Generic PnP Monitor
          Settings: 1920x1080 32bit @60Hz default

  [6] \\.\DISPLAY7                  Parsec Virtual Display Adapter
  [7] \\.\DISPLAY8                  Parsec Virtual Display Adapter
  [8] \\.\DISPLAY9                  Parsec Virtual Display Adapter
  [9] \\.\DISPLAY10                 Parsec Virtual Display Adapter
  [10] \\.\DISPLAY11                 Parsec Virtual Display Adapter
  [11] \\.\DISPLAY12                 Parsec Virtual Display Adapter
  [12] \\.\DISPLAY13                 Parsec Virtual Display Adapter
  [13] \\.\DISPLAY14                 Parsec Virtual Display Adapter
  [14] \\.\DISPLAY15                 Parsec Virtual Display Adapter
  [15] \\.\DISPLAY16                 Parsec Virtual Display Adapter
  [16] \\.\DISPLAY17                 Parsec Virtual Display Adapter
  [17] \\.\DISPLAY18                 Parsec Virtual Display Adapter
  [18] \\.\DISPLAY19                 Parsec Virtual Display Adapter
  [19] \\.\DISPLAY20                 Parsec Virtual Display Adapter
  [20] \\.\DISPLAY21                 Parsec Virtual Display Adapter
";
#else

			LaunchScreenResolutionUtil("/l");
#endif
		string patternDisplays =
#if DEBUG
			@"\\\\.\\DISPLAY\d{1,2}\s+NVIDIA GeForce RTX 3060 Laptop GPU";
#else
			@"\\\\.\\DISPLAY\d{1,2}\s+Parsec Virtual Display Adapter";
#endif
		Match match = Regex.Match(displays, patternDisplays);
		string display = match.Value.Split(" ")[0];
		string availableDisplayModes =
#if DEBUG
			@"
Display modes for \\.\DISPLAY6:
  1920x1080 32bit @60Hz default
  1920x1080 32bit @240Hz default
  1920x1080 32bit @144Hz default
  1920x1080 32bit @30Hz default
  1920x1080 32bit @24Hz default
  3840x2160 32bit @240Hz default
  3840x2160 32bit @144Hz default
  3840x2160 32bit @60Hz default
  3840x2160 32bit @30Hz default
  3840x2160 32bit @24Hz default
  3200x1800 32bit @240Hz default
  3200x1800 32bit @144Hz default
  3200x1800 32bit @60Hz default
  3200x1800 32bit @30Hz default
  3200x1800 32bit @24Hz default
  2880x1620 32bit @240Hz default
  2880x1620 32bit @144Hz default
  2880x1620 32bit @60Hz default
  2880x1620 32bit @30Hz default
  2880x1620 32bit @24Hz default
  2560x1600 32bit @240Hz default
  2560x1600 32bit @144Hz default
  2560x1600 32bit @60Hz default
  2560x1600 32bit @30Hz default
  2560x1600 32bit @24Hz default
  2560x1440 32bit @240Hz default
  2560x1440 32bit @144Hz default
  2560x1440 32bit @60Hz default
  2560x1440 32bit @30Hz default
  2560x1440 32bit @24Hz default
  2048x1152 32bit @240Hz default
  2048x1152 32bit @144Hz default
  2048x1152 32bit @60Hz default
  1920x1200 32bit @240Hz default
  1920x1200 32bit @144Hz default
  1920x1200 32bit @60Hz default
  1680x1050 32bit @240Hz default
  1680x1050 32bit @144Hz default
  1680x1050 32bit @60Hz default
  1600x900 32bit @240Hz default
  1600x900 32bit @144Hz default
  1600x900 32bit @60Hz default
  1440x900 32bit @240Hz default
  1440x900 32bit @144Hz default
  1440x900 32bit @60Hz default
  1366x768 32bit @240Hz default
  1366x768 32bit @144Hz default
  1366x768 32bit @60Hz default
  1280x800 32bit @240Hz default
  1280x800 32bit @144Hz default
  1280x800 32bit @60Hz default
  1280x720 32bit @240Hz default
  1280x720 32bit @144Hz default
  1280x720 32bit @60Hz default
  3840x1600 32bit @240Hz default
  3840x1600 32bit @144Hz default
  3840x1600 32bit @60Hz default
  3840x1600 32bit @30Hz default
  3840x1600 32bit @24Hz default
  3840x1080 32bit @240Hz default
  3840x1080 32bit @144Hz default
  3840x1080 32bit @60Hz default
  3840x1080 32bit @30Hz default
  3840x1080 32bit @24Hz default
  3440x1440 32bit @240Hz default
  3440x1440 32bit @144Hz default
  3440x1440 32bit @60Hz default
  3440x1440 32bit @30Hz default
  3440x1440 32bit @24Hz default
  2560x1080 32bit @240Hz default
  2560x1080 32bit @144Hz default
  2560x1080 32bit @60Hz default
  2560x1080 32bit @30Hz default
  2560x1080 32bit @24Hz default
  4096x2160 32bit @240Hz default
  4096x2160 32bit @144Hz default
  4096x2160 32bit @60Hz default
  4096x2160 32bit @30Hz default
  4096x2160 32bit @24Hz default
  1600x1200 32bit @240Hz default
  1600x1200 32bit @144Hz default
  1600x1200 32bit @60Hz default
  1600x1200 32bit @30Hz default
  1600x1200 32bit @24Hz default
  2880x1800 32bit @60Hz default
  3000x2000 32bit @60Hz default
  2736x1824 32bit @60Hz default
  2256x1504 32bit @60Hz default
  3240x2160 32bit @60Hz default
  2496x1664 32bit @60Hz default
  1800x1200 32bit @60Hz default
";
#else
		LaunchScreenResolutionUtil($"/d={display} /m");
#endif

		string patternDisplayModes = @"(?<resolution>\d+x\d+)\s+32bit @(?<refresh_rate>\d+Hz) default";
		MatchCollection matches = Regex.Matches(availableDisplayModes, patternDisplayModes);
		foreach (Match mode in matches) 
		{
			int width, height, refreshRate;
			try
			{
				string[] widthAndHeight = mode.Groups["resolution"].Value.Split("x");
				string refreshRateText = mode.Groups["refresh_rate"].Value;
				width = int.Parse(widthAndHeight[0]);
				height = int.Parse(widthAndHeight[1]);
				refreshRate = int.Parse(refreshRateText.Substring(0, refreshRateText.Length - 2));

			}
			catch (Exception) { continue; }


			// resolution already exists
			ScreenResolution? resolution = result.Find(el => el.Width == width && el.Height == height);
			if (resolution is not null)
			{
				resolution.RefreshRates.Add(refreshRate);
			}
			else
			{
				result.Add(new ScreenResolution()
				{
					Width = width,
					Height = height,
					RefreshRates = new List<int> { refreshRate }
				});
			}
		}

		SortRefreshRates(result);
		AddTextModifiers(result);

		return result;
	}

	private static void AddTextModifiers(List<ScreenResolution> screenResolutions)
	{
		Dictionary<(int, int), string> modifiers = new Dictionary<(int, int), string>()
		{
			{(1280, 720), "HD" },
			{(1920, 1080), "Full HD" },
			{(2560, 1440), "Quad HD" },
			{(3840, 2160), "Ultra HD (4K)" },
		};
		foreach (ScreenResolution screenResolution in screenResolutions)
		{
			foreach(KeyValuePair<(int, int), string> modifier in modifiers)
			{
				if(screenResolution.Width == modifier.Key.Item1 && screenResolution.Height == modifier.Key.Item2)
				{
					screenResolution.Text = modifier.Value;
					continue;
				}
			}
		}
	}

	private static void SortRefreshRates(List<ScreenResolution> screenResolutions)
	{
		foreach(ScreenResolution screenResolution in screenResolutions)
		{
			screenResolution.RefreshRates.Sort();
			screenResolution.RefreshRates.Reverse();
		}
	}

	private string LaunchScreenResolutionUtil(string arguments)
	{
		ProcessStartInfo psi = new ProcessStartInfo()
		{
			UseShellExecute = false,
			RedirectStandardOutput = true,
			RedirectStandardError = false,
			RedirectStandardInput = false,
			Arguments = arguments,
			FileName = csrExeFullPath
		};
		Process process = Process.Start(psi)!;
		process.WaitForExit(new TimeSpan(0, 0, 1));
		return process.StandardOutput.ReadToEnd();
	}

	private static void ExtractResource(string resourceName, string outputPath)
	{
		using Stream? stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
		if (stream is null)
		{
			throw new FileNotFoundException("Resource not found: " + resourceName);
		}

		using FileStream fileStream = new FileStream(outputPath, FileMode.Create);
		stream.CopyTo(fileStream);
	}

	private ScreenResolution? GetDefaultScreenResolution()
	{
		if (Screen.PrimaryScreen is null) return null;
		Rectangle primaryScreenBounds = Screen.PrimaryScreen.Bounds;

		return new ScreenResolution() 
		{ 
			Width = primaryScreenBounds.Width, 
			Height = primaryScreenBounds.Height,
			Text = "Default"
		};
	}

	private void Button_apply_Click(object? sender, EventArgs e)
	{
		if(comboBox_resolution.SelectedIndex == noItemSelectedIndex || comboBox_refreshRate.SelectedIndex == noItemSelectedIndex)
		{
			return;
		}
		ScreenResolution screenResolution = availableResolutions[comboBox_resolution.SelectedIndex];
		int refreshRate = screenResolution.RefreshRates[comboBox_refreshRate.SelectedIndex];

		ApplyScreenSettings(screenResolution, refreshRate);
		DialogResult result = new DialogWindow(Icon!).ShowDialog();
		if(result != DialogResult.OK)
		{
			ApplyScreenSettings(defaultResolution, defaultRefreshRate);
			ResetChoicesToDefaultValues();
		}
	}

	private void ResetChoicesToDefaultValues()
	{
		comboBox_resolution.SelectedIndex = availableResolutions.ToImmutableArray().IndexOf(defaultResolution);
	}

	private void ApplyScreenSettings(ScreenResolution screenResolution, int refreshRate)
	{
		ProcessStartInfo psi = new ProcessStartInfo()
		{
			UseShellExecute = true,
			RedirectStandardOutput = false,
			RedirectStandardError = false,
			RedirectStandardInput = false,
			Arguments = $"setdisplay {screenResolution.Width} {screenResolution.Height} {bpc} {refreshRate}",
			FileName = nircmdExeFullPath
		};
		_ = Process.Start(psi);
	}

	private void FillInComboBoxes()
	{
		foreach(ScreenResolution resolution in availableResolutions.OrderByDescending(el => el.Width * el.Height))
		{
			comboBox_resolution.Items.Add(resolution);
		}
	}
}
