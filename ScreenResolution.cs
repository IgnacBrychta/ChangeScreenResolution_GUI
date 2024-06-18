namespace ChangeScreenResolution_GUI;

internal sealed class ScreenResolution
{
	internal int Width { get; init; }
	internal int Height { get; init; }
	internal List<int> RefreshRates { get; init; }
	internal string Text { get; set; } = "";

	public override string ToString()
	{
		return $"{Width} × {Height} {(string.IsNullOrEmpty(Text) ? "" : $"| {Text}")}";
	}
}
