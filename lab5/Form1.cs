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
			cntrl = new Controller(this);
			cb_List.TextUpdate+=comboBox1_SelectedIndexChanged;
		}

		public void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
		/*
			bool textUpdated = false;
			if (cntrl != null)
			{
				foreach(Device device in cntrl.deviceList)
				{
					if (device.Letter[0] == cb_List.Text[0])
					{
					  if (device.IsWPD)
						{
							lb_info.Text = "no info";
							b_eject.Enabled = false;
						}
						else
						{
							lb_info.Text=
								(device.Capacity / 1024.0 / 1024.0) + " MB"
								+Environment.NewLine
								+(device.FreeSpace / 1024.0 / 1024.0) + " MB";
							b_eject.Enabled = true;
						}

						textUpdated = true;
						break;
					}
				}
			}
			if (!textUpdated)
			{lb_info.Text = "no info";}
		 * */
		}

		private void button1_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Unable to eject USB. It may be busy.");
		}

		private void Form1_Load(object sender, EventArgs e)
		{

		}

		private void InitializeComponent()
		{
			this.SuspendLayout();
			// 
			// Form1
			// 
			this.ClientSize = new System.Drawing.Size(284, 261);
			this.Name = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load_1);
			this.ResumeLayout(false);

		}

		private void Form1_Load_1(object sender, EventArgs e)
		{

		}
	}
}
