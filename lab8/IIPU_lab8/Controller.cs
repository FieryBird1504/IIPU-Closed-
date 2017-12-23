using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace IIPU_lab8
{
	public class Controller
	{
		public bool Hidden = true;
		public bool Logging = true;
		public bool HooksEnabled = true;
		public int LogFileSize = 0;
		public string Email = "";

		Form1 MyForm;

		string FileLocation = "./save.txt";

		private byte key = 54;

		public Controller(Form1 formArg)
		{
			MyForm = formArg;

			LoadData();
			FormSetData();

			
		}

		public void FormSetData()
		{
			MyForm.tb_Email.Text = Email;
			MyForm.chbox_Hidden.Checked = Hidden;
			MyForm.chbox_Logging.Checked = Logging;
			MyForm.chbox_EnableHooks.Checked = HooksEnabled;
			try
			{
				MyForm.nud_FileSize.Value = LogFileSize;
			}
			catch(Exception) { };

			if (Hidden)
			{MyForm.Hide();}
			else
			{MyForm.Show();}
		}

		public void FormGetData()
		{
			Email = MyForm.tb_Email.Text;
			Hidden = MyForm.chbox_Hidden.Checked;
			Logging = MyForm.chbox_Logging.Checked;
			HooksEnabled = MyForm.chbox_EnableHooks.Checked;
			LogFileSize = (int)(MyForm.nud_FileSize.Value);
		}

		public void SaveData()
		{
			string[] lines = new string[5];
			lines[0] = Email;
			lines[1] = Hidden.ToString();
			lines[2] = Logging.ToString();
			lines[3] = HooksEnabled.ToString();
			lines[4] = LogFileSize.ToString();

			
			File.WriteAllLines(FileLocation, lines);
			
			DecryptFile();

		}

		void LoadData()
		{
			try
			{	
				DecryptFile();
				string[] lines = File.ReadAllLines(FileLocation);
				Email = lines[0];
				Hidden = StrToBool(lines[1]);
				Logging = StrToBool(lines[2]);
				HooksEnabled = StrToBool(lines[3]);
				LogFileSize = Int32.Parse(lines[4]);	
				DecryptFile();
			}
			catch(Exception)
			{
				Email = "fakeyfoxe@gmail.com";
				Hidden = false;
				Logging = true;
				HooksEnabled = true;
				LogFileSize = 1000;
			}
			
		}

		bool StrToBool(string str)
		{
			return (str.ToLower().CompareTo("true") == 0);
		}

		void DecryptFile()
		{
			byte[] arr = File.ReadAllBytes(FileLocation);
			for(var i = 0; i < arr.Length; i += 1)
			{arr[i] = (byte)(arr[i] ^ key);}

			File.WriteAllBytes(FileLocation, arr);
		}

		void EncryptFile()
		{
			
		}


	}
}
