using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SimpleWifi;
using System.Diagnostics;

namespace IIPU_lab6
{
	class Controller
	{
		Form1 MyForm;
		Timer UpdateTimer;

		Wifi wifi;

		public List<AccessPoint> accessPoints;

		public string[] MacAddresses;

		public Controller(Form1 formArg)
		{
			MyForm = formArg;
			wifi = new Wifi();
			//wifi.ConnectionStatusChanged += wifi_ConnectionStatusChanged;

			UpdateTimer = new Timer();
			UpdateTimer.Interval = 1000; //ms
			UpdateTimer.Enabled = true;
			UpdateTimer.Tick += UpdateWifiInfo;

			UpdateWifiInfo(null, null);
		}

		private void UpdateWifiInfo(object sender, EventArgs e)
		{
			MacAddresses = GetMacAddresses();
			
			accessPoints = wifi.GetAccessPoints();			
			
			
			var lbox_WifiList = MyForm.lbox_wifiList;
			
			string sel="";
			
			try
			{
				sel = lbox_WifiList.SelectedItem.ToString();
			}
			catch(Exception){}
			
			lbox_WifiList.Items.Clear();
			
			var i = 0;
			foreach(AccessPoint ap in accessPoints)
			{
				lbox_WifiList.Items.Add(ap.Name);
				if (string.Compare(ap.Name, sel) == 0)
				{
					lbox_WifiList.SelectedIndex = i;
				}
				i += 1;
			}
		}

		public bool Connect(AccessPoint ap, string password)
		{
		  try
			{
				var authRequest = new AuthRequest(ap);
				bool overwrite = true;
	
				// Requesting password.
				if (authRequest.IsPasswordRequired)
				{
					if (!ap.IsValidPassword(password)) // Invalid pass.
					{
						return false;
					}
					authRequest.Password = password;
				}
				else
				{
					authRequest.Password = "";
				}

				// Connecting.
				try
				{
					return ap.Connect(authRequest, true);
				}
				catch(Exception)
				{
					return false;
				}
				// Connecting.
			}
			catch(Exception){ }

			return true;
			
		} 

		public static string[] GetMacAddresses()
		{
			Process proc = new Process();
			proc.StartInfo.CreateNoWindow = true;
			proc.StartInfo.FileName = "cmd";
			proc.StartInfo.Arguments = @"/C ""netsh wlan show networks mode=bssid | findstr BSSID""";

			proc.StartInfo.RedirectStandardOutput = true;
			proc.StartInfo.UseShellExecute = false;
			proc.Start();
			string output = proc.StandardOutput.ReadToEnd();
			proc.WaitForExit();

			string[] arr = output.Split(
				new[] { Environment.NewLine },
				StringSplitOptions.None
			);

			var bssidLength = arr[0].Length - 17;
			//MessageBox.Show(arr[0].Length+"");
			for(var i = 0; i < arr.Length-1; i += 1)
			{
				arr[i] = arr[i].Substring(bssidLength);
			}

			return arr;
		}


	}
}
