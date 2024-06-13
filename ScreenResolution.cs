namespace ChangeScreenResolution_GUI;

internal sealed class ScreenResolution
{
	internal int Width { get; init; }
	internal int Height { get; init; }
	internal string Text { get; init; } = "";

	public override string ToString()
	{
		return $"{Width} × {Height} {(string.IsNullOrEmpty(Text) ? "" : $"| {Text}")}";
	}
}
