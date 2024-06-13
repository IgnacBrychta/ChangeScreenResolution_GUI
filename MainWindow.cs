using System.Collections.Immutable;
using System.Diagnostics;

namespace ChangeScreenResolution_GUI;

public partial class MainWindow : Form
{
	const string nircmdExePath = "../../../nircmd/nircmd.exe";
	const string iconPath = "../../../res/5ee37bf62e6ec804b72e580a_cmis logo 256.ico";
	string nircmdExeFullPath = "";
	const int bpc = 32;
	ScreenResolution defaultResolution;
	int defaultRefreshRate = 60;
	internal ScreenResolution[] availableResolutions;
	internal int[] availableRefreshRates = new int[]
	{
		24,
		30,
		60,
		144,
		240
	};
	public MainWindow()
	{
		InitializeComponent();

		ScreenResolution? res = GetDefaultScreenResolution() ?? throw new Exception("Primary screen not detected");
		defaultResolution = res;

		Icon = new Icon(iconPath);
		button_apply.Click += Button_apply_Click;
		nircmdExeFullPath = Path.GetFullPath(nircmdExePath);

		availableResolutions = new[]
		{
			defaultResolution,
			new ScreenResolution() { Width = 1280, Height = 720, Text = "HD" },
			new ScreenResolution() { Width = 1920, Height = 1080, Text = "Full HD" },
			new ScreenResolution() { Width = 1920, Height = 1200 },
			new ScreenResolution() { Width = 2560, Height = 1080 },
			new ScreenResolution() { Width = 2560, Height = 1440, Text = "Quad HD" },
			new ScreenResolution() { Width = 2560, Height = 1600 },
			new ScreenResolution() { Width = 3440, Height = 1440 },
			new ScreenResolution() { Width = 3840, Height = 1600 },
			new ScreenResolution() { Width = 3840, Height = 2160, Text = "Ultra HD (4K)" },
		}.ToImmutableArray()
		.DistinctBy(el => el.Width * el.Height)
		.OrderByDescending(el => el.Width * el.Height)
		.Reverse()
		.ToArray();

		FillInComboBoxes();
		ResetChoicesToDefaultValues();
	}

	private ScreenResolution? GetDefaultScreenResolution()
	{
		if (Screen.PrimaryScreen is null) return null;
		Rectangle primaryScreenBounds = Screen.PrimaryScreen.Bounds;

		return new ScreenResolution() 
		{ 
			Width = primaryScreenBounds.Width, 
			Height = primaryScreenBounds.Height ,
			Text = "Default"
		};
	}

	private void Button_apply_Click(object? sender, EventArgs e)
	{
		int noItemSelectedIndex = -1;
		if(comboBox_resolution.SelectedIndex == noItemSelectedIndex || comboBox_refreshRate.SelectedIndex == noItemSelectedIndex)
		{
			return;
		}
		ScreenResolution screenResolution = availableResolutions[comboBox_resolution.SelectedIndex];
		int refreshRate = availableRefreshRates[comboBox_refreshRate.SelectedIndex];

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
		comboBox_refreshRate.SelectedIndex = availableRefreshRates.ToImmutableArray().IndexOf(defaultRefreshRate);
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
		foreach(ScreenResolution resolution in availableResolutions)
		{
			comboBox_resolution.Items.Add(resolution);
		}

		foreach(int availableRefreshRate in availableRefreshRates)
		{
			comboBox_refreshRate.Items.Add($"{availableRefreshRate} Hz");
		}
	}
}
