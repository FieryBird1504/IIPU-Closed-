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

        private void UpdateAll()
        {
            usbNames = new List<string>();
            ReadyDriveLetters = new List<string>();
            bool foundAny = false;
            comboBox1.Items.Clear();
            textBox1.Clear();
            textBox1.AppendText("USB logical drives:\n");
            textBox1.AppendText(string.Format(OutputFormat, "Drive", "File system", "Free space", "Total space\n"));
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            foreach (DriveInfo d in allDrives)
            {
                try
                {
                    if (d.DriveType == DriveType.Removable)
                    {
                        foundAny = true;
                        if (d.IsReady == true)
                        {
                            textBox1.AppendText(string.Format(OutputFormat,
                                d.Name + " " + d.VolumeLabel, d.DriveFormat,
                                d.TotalFreeSpace / 1024 / 1024,
                                d.TotalSize / 1024 / 1024) + "\n");
                            ReadyDriveLetters.Add(d.Name.Replace("\\", ""));
                            comboBox1.Items.Add(d.Name);

                        }
                        else
                        {
                            textBox1.AppendText(string.Format("{0, -5} {1}", d.Name, "*** Devise is ready to remove***\n"));
                        }
                        usbNames.Add(d.Name);
                        if (d.VolumeLabel.Length != 0)
                            usbNames.Add(d.VolumeLabel);
                    }
                }
                catch (Exception)
                {

                }
            }
            if (!foundAny)
                this.textBox1.AppendText("No devices connected\n");
        }

        private void UpdateWindow()
        {
            textBox1.Clear();
            updateUSBDrives();
            updateMTPDrives();
        }

        void updateUSBDrives()
        {
            usbNames = new List<string>();
            textBox1.AppendText("USB logical drives:\n");
            textBox1.AppendText(string.Format(OutputFormat, "Drive", "File system", "Free space", "Total space") + "\n");
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            bool foundAny = false;
            foreach (DriveInfo d in allDrives)
            {
                try
                {
                    foundAny = true;
                    if (d.DriveType == DriveType.Removable)
                    {
                        Console.WriteLine("Drive {0}", d.Name);
                        if (d.IsReady == true)
                        {
                            textBox1.AppendText(string.Format(OutputFormat,
                                d.Name + " " + d.VolumeLabel, d.DriveFormat,
                                d.TotalFreeSpace / 1024 / 1024,
                                d.TotalSize / 1024 / 1024) + "\n");
                        }
                        else
                        {
                            textBox1.AppendText(string.Format("{0, -5} {1}", d.Name, "*** Devise is ready to remove***\n"));
                        }
                        usbNames.Add(d.Name);
                        if (d.VolumeLabel.Length != 0)
                            usbNames.Add(d.VolumeLabel);
                    }
                }
                catch (Exception)
                {
                    Thread.Sleep(100);
                }
            }
            if (!foundAny)
                this.textBox1.AppendText("No devices connected\n");
        }

        void updateMTPDrives()
        {
            textBox1.AppendText("MTP devices:\n");
            bool anyFound = false;
            try
            {
                var deviceManager = new PortableDeviceManager();
                deviceManager.RefreshDeviceList();
                var deviceIds = new string[1];
                uint count = 1;
                deviceManager.GetDevices(ref deviceIds[0], ref count);
                // Retrieve the device id for each connected device
                deviceIds = new string[count];
                deviceManager.GetDevices(ref deviceIds[0], ref count);
                foreach (var deviceId in deviceIds)
                {
                    var str = new MTPDevice(deviceId).FriendlyName;
                    bool found = false;
                    foreach (string name in usbNames)
                    {
                        if (name.CompareTo(str) == 0)
                        {
                            found = true;
                            usbNames.Remove(name);
                            break;
                        }
                    }
                    if (!found)
                    {
                        textBox1.AppendText(str + "\n");
                        anyFound = true;
                    }
                }
            }
            catch (Exception e)
            {
                Thread.Sleep(100);
            }
            if (!anyFound)
                textBox1.AppendText("No devices connected");
        }
    }
}
