using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IIPU_lab8
{
	public partial class Form1 : Form
	{

		Controller MyController;
		HookController Hooks;
		Logger MyLogger;

		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			nud_FileSize.Minimum = 0;
			nud_FileSize.Maximum = 10000;
			MyController = new Controller(this);

			//MyLogger = new Logger(MyController);

			Hooks = new HookController(MyController);
			
			Shown +=  FormShown;
		}

		private void b_Apply_Click(object sender, EventArgs e)
		{
			MyController.FormGetData();
			MyController.SaveData();
			if (MyController.Hidden)
			{this.Hide();}
		}

		void FormShown(object sender, EventArgs e)
		{
			if (MyController.Hidden)
			{this.Hide();}
		}
	}
}
