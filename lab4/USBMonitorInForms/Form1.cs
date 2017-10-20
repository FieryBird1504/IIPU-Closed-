using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using PortableDeviceApiLib;
using System.Threading;

namespace USBMonitorInForms
{
    public partial class Form1 : Form
    {
        private const int WM_DeviceChange = 0x219;          // something happened to usb
        private const int DBT_DEVICEARRIVAL = 0x8000;       // device connected
        private const int DBT_DEVICEREMOVECOMPLETE = 0x8004;// device disconnected

        public Form1()
        {
            InitializeComponent();
            LoadInfo();
        }
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_DeviceChange) // if smth happened with usb ports
            {
                if (m.WParam.ToInt32() == DBT_DEVICEARRIVAL)
                {
                    MessageBox.Show("New USB device connected!",
                            "USB Monitor", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    LoadInfo();
                }
                if (m.WParam.ToInt32() == DBT_DEVICEREMOVECOMPLETE)
                {
                    LoadInfo();
                    MessageBox.Show("USB is disconnected!",
                            "USB Monitor", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                }
            }
        }

        const string OutputFormat = "{0, -15} {1,-11} {2, -11} {3, -11}";

        List<string> ReadyDriveLetters;

        List<string> usbNames;
    }
}
