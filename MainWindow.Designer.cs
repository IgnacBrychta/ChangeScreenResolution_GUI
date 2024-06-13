namespace ChangeScreenResolution_GUI
{
	partial class MainWindow
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
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
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			groupBox1 = new GroupBox();
			comboBox_resolution = new ComboBox();
			groupBox2 = new GroupBox();
			comboBox_refreshRate = new ComboBox();
			groupBox3 = new GroupBox();
			button_apply = new Button();
			groupBox1.SuspendLayout();
			groupBox2.SuspendLayout();
			groupBox3.SuspendLayout();
			SuspendLayout();
			// 
			// groupBox1
			// 
			groupBox1.Controls.Add(comboBox_resolution);
			groupBox1.Location = new Point(3, 12);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new Size(271, 60);
			groupBox1.TabIndex = 0;
			groupBox1.TabStop = false;
			groupBox1.Text = "Resolution";
			// 
			// comboBox_resolution
			// 
			comboBox_resolution.FormattingEnabled = true;
			comboBox_resolution.Location = new Point(9, 22);
			comboBox_resolution.Name = "comboBox_resolution";
			comboBox_resolution.Size = new Size(250, 23);
			comboBox_resolution.TabIndex = 0;
			// 
			// groupBox2
			// 
			groupBox2.Controls.Add(comboBox_refreshRate);
			groupBox2.Location = new Point(280, 12);
			groupBox2.Name = "groupBox2";
			groupBox2.Size = new Size(130, 60);
			groupBox2.TabIndex = 1;
			groupBox2.TabStop = false;
			groupBox2.Text = "Refresh rate ";
			// 
			// comboBox_refreshRate
			// 
			comboBox_refreshRate.FormattingEnabled = true;
			comboBox_refreshRate.Location = new Point(9, 22);
			comboBox_refreshRate.Name = "comboBox_refreshRate";
			comboBox_refreshRate.Size = new Size(111, 23);
			comboBox_refreshRate.TabIndex = 0;
			// 
			// groupBox3
			// 
			groupBox3.Controls.Add(button_apply);
			groupBox3.Location = new Point(3, 78);
			groupBox3.Name = "groupBox3";
			groupBox3.Size = new Size(407, 100);
			groupBox3.TabIndex = 2;
			groupBox3.TabStop = false;
			// 
			// button_apply
			// 
			button_apply.Location = new Point(9, 22);
			button_apply.Name = "button_apply";
			button_apply.Size = new Size(388, 62);
			button_apply.TabIndex = 0;
			button_apply.Text = "Apply";
			button_apply.UseVisualStyleBackColor = true;
			// 
			// MainWindow
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(420, 190);
			Controls.Add(groupBox3);
			Controls.Add(groupBox2);
			Controls.Add(groupBox1);
			FormBorderStyle = FormBorderStyle.FixedSingle;
			MaximizeBox = false;
			Name = "MainWindow";
			Text = "Screen resolution change utility";
			groupBox1.ResumeLayout(false);
			groupBox2.ResumeLayout(false);
			groupBox3.ResumeLayout(false);
			ResumeLayout(false);
		}

		#endregion

		private GroupBox groupBox1;
		private ComboBox comboBox_resolution;
		private GroupBox groupBox2;
		private ComboBox comboBox_refreshRate;
		private GroupBox groupBox3;
		private Button button_apply;
	}
}
