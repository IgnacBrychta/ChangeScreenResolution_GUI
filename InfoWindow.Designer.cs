namespace ChangeScreenResolution_GUI
{
	partial class InfoWindow
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			richTextBox_bodyText = new RichTextBox();
			groupBox1 = new GroupBox();
			button_ok = new Button();
			groupBox1.SuspendLayout();
			SuspendLayout();
			// 
			// richTextBox_bodyText
			// 
			richTextBox_bodyText.Location = new Point(6, 13);
			richTextBox_bodyText.Name = "richTextBox_bodyText";
			richTextBox_bodyText.Size = new Size(304, 181);
			richTextBox_bodyText.TabIndex = 0;
			richTextBox_bodyText.Text = "";
			// 
			// groupBox1
			// 
			groupBox1.Controls.Add(button_ok);
			groupBox1.Controls.Add(richTextBox_bodyText);
			groupBox1.Location = new Point(12, 12);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new Size(317, 265);
			groupBox1.TabIndex = 1;
			groupBox1.TabStop = false;
			// 
			// button_ok
			// 
			button_ok.Location = new Point(6, 200);
			button_ok.Name = "button_ok";
			button_ok.Size = new Size(304, 58);
			button_ok.TabIndex = 1;
			button_ok.Text = "OK";
			button_ok.UseVisualStyleBackColor = true;
			// 
			// InfoWindow
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(341, 285);
			Controls.Add(groupBox1);
			FormBorderStyle = FormBorderStyle.FixedSingle;
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "InfoWindow";
			Text = "InfoWindow";
			groupBox1.ResumeLayout(false);
			ResumeLayout(false);
		}

		#endregion

		private RichTextBox richTextBox_bodyText;
		private GroupBox groupBox1;
		private Button button_ok;
	}
}