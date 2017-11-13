using System;
using System.Windows.Forms;

namespace IIPU_lab4_GUI
{
	public partial class Form1 : Form
	{
		Controller cntrl;

		public Form1()
		{
			InitializeComponent();
			InitializeComponent1();
			cntrl = new Controller(this);
			cb_List.TextUpdate+=comboBox1_SelectedIndexChanged;
		}

		public void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
		
			bool textUpdated = false;
			if (cntrl != null)
			{
				foreach(Device device in cntrl.deviceList)
				{
					if (String.Compare(device.Name, cb_List.Text) == 0)
					{
					  
						lb_info.Text = "GUID:" + Environment.NewLine +
						               device.ClassGuid + Environment.NewLine +
													 "Device ID:" + Environment.NewLine +
						               device.DeviceID + Environment.NewLine +
													 "Manufacturer:" + Environment.NewLine +
													 device.Manufacturer + Environment.NewLine +
													 "HardwareIDs:" + Environment.NewLine;

						for(var i=0; i<device.HardwareID.Length; i+=1)
						{
							lb_info.Text += device.HardwareID[i] + Environment.NewLine;
						}

						lb_info.Text += "System files:" + Environment.NewLine;

						for(var i=0; i<device.GetSysFileCount(); i+=1)
						{
							lb_info.Text += device.GetSysFileDesc(i) + Environment.NewLine +
							                device.GetSysFilePath(i) + Environment.NewLine;
						}

						if (device.Enabled)
						{
							lb_info.Text += "ENABLED";
						}
						else
						{
							lb_info.Text += "DISABLED";
						}

						textUpdated = true;
						break;
					}
				}
			}
			if (!textUpdated)
			{lb_info.Text = "no info";}
		 
		}

		private void InitializeComponent1()
		{
			this.SuspendLayout();
			this.ClientSize = new System.Drawing.Size(284, 300);
			this.Name = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load_1);
			this.ResumeLayout(false);

		}

		private void Form1_Load_1(object sender, EventArgs e)
		{}
	}
}
