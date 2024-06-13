namespace ChangeScreenResolution_GUI
{
	partial class DialogWindow
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
			groupBox1 = new GroupBox();
			button_revert = new Button();
			button_keep = new Button();
			groupBox1.SuspendLayout();
			SuspendLayout();
			// 
			// groupBox1
			// 
			groupBox1.Controls.Add(button_revert);
			groupBox1.Controls.Add(button_keep);
			groupBox1.Location = new Point(12, 12);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new Size(366, 82);
			groupBox1.TabIndex = 0;
			groupBox1.TabStop = false;
			// 
			// button_revert
			// 
			button_revert.Location = new Point(190, 22);
			button_revert.Name = "button_revert";
			button_revert.Size = new Size(165, 49);
			button_revert.TabIndex = 1;
			button_revert.Text = "Revert to default";
			button_revert.UseVisualStyleBackColor = true;
			// 
			// button_keep
			// 
			button_keep.Location = new Point(6, 22);
			button_keep.Name = "button_keep";
			button_keep.Size = new Size(165, 49);
			button_keep.TabIndex = 0;
			button_keep.Text = "Keep";
			button_keep.UseVisualStyleBackColor = true;
			// 
			// DialogWindow
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(396, 109);
			Controls.Add(groupBox1);
			FormBorderStyle = FormBorderStyle.FixedSingle;
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "DialogWindow";
			Text = "Keep changes?";
			groupBox1.ResumeLayout(false);
			ResumeLayout(false);
		}

		#endregion

		private GroupBox groupBox1;
		private Button button_revert;
		private Button button_keep;
	}
}