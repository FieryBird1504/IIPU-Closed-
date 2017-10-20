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
    }
}
