using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChangeScreenResolution_GUI
{
	public partial class InfoWindow : Form
	{
		public InfoWindow(Icon? icon, string headerText, string bodyText)
		{
			InitializeComponent();
			Text = headerText;
			richTextBox_bodyText.Text = bodyText;
			Icon = icon;
		}
	}
}
