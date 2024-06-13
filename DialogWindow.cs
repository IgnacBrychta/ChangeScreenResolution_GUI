namespace ChangeScreenResolution_GUI;

public partial class DialogWindow : Form
{
	private bool forceClose = false;
	private TimeSpan timeToConfirm = new TimeSpan(0, 0, 15);
	public DialogWindow(Icon icon)
	{
		InitializeComponent();

		Icon = icon;
		button_keep.Click += Button_keep_Click;
		button_revert.Click += Button_revert_Click;
		this.FormClosing += DialogWindow_FormClosing;
		DialogResult = DialogResult.Ignore;

		_ = StartRevertTimer();
	}

	private async Task StartRevertTimer()
	{
		await Task.Delay(timeToConfirm);

		forceClose = true;
		Invoke(new Action(() =>
		{
			Close();
		}));
	}

	private void DialogWindow_FormClosing(object? sender, FormClosingEventArgs e)
	{
		e.Cancel = !(DialogResult == DialogResult.OK || DialogResult == DialogResult.Abort) && !forceClose;
	}

	private void Button_revert_Click(object? sender, EventArgs e)
	{
		DialogResult = DialogResult.Abort;
		Close();
	}

	private void Button_keep_Click(object? sender, EventArgs e)
	{
		DialogResult = DialogResult.OK;
		Close();
	}
}
