using SimpleWifi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IIPU_lab6
{
	public partial class Form1 : Form
	{
		Controller Cntrl;
			
		public Form1()
		{
			InitializeComponent();

			lbox_wifiList.SelectedIndexChanged += infoUpdate;
			Cntrl = new Controller(this);			
		}

		private void label1_Click(object sender, EventArgs e)
		{

		}

		private void infoUpdate(object sender, EventArgs e)
		{
			
			lb_info.Text = Cntrl.accessPoints[lbox_wifiList.SelectedIndex].ToString(); // All the info.
			
			lb_info.Text += "Signal: " + Cntrl.accessPoints[lbox_wifiList.SelectedIndex].SignalStrength + "%"
			              + Environment.NewLine + "Mac: ";
			
			
			lb_info.Text += Cntrl.MacAddresses[lbox_wifiList.SelectedIndex];
		}

		private void b_connect_Click(object sender, EventArgs e)
		{
			if (!Cntrl.Connect(Cntrl.accessPoints[lbox_wifiList.SelectedIndex], tb_password.Text))
			{
				MessageBox.Show("Cannot connect to the wifi.", ":c");
			}
		}


	}
}
