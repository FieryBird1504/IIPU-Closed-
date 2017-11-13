using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Windows.Forms;

namespace IIPU_lab4_GUI
{
	public class Device
	{
        public string Name,
                                    ClassGuid,
                                    Manufacturer,
                                    DeviceID;
        public string[] HardwareID;
        private List<SysFile> SysFiles = new List<SysFile>();
        public bool Enabled;

        class SysFile
        {
            public string PathName;
            public string Description;
        }

        public void AddSysFile(string pname, string desc)
        {
            var sysFile = new SysFile();
            sysFile.PathName = pname;
            sysFile.Description = desc;
            SysFiles.Add(sysFile);
        }

        public int GetSysFileCount()
        { return SysFiles.Count(); }

        public string GetSysFileDesc(int index)
        { return SysFiles[index].Description; }

        public string GetSysFilePath(int index)
        { return SysFiles[index].PathName; }

        public void ToggleEnable()
        {
            try
            {
                var device = new ManagementObjectSearcher("SELECT * FROM Win32_PNPEntity").Get()
                    .OfType<ManagementObject>()
                    .FirstOrDefault(x => x.Properties["DeviceID"].Value.ToString().Equals(DeviceID));

                if (Enabled)
                {
                    device.InvokeMethod("Disable", new object[] { false });
                }
                else
                {
                    device.InvokeMethod("Enable", new object[] { false });
                }

                Enabled = !Enabled;
            }
            catch (Exception)
            { MessageBox.Show("Cannot disconnect the device!"); }
        }
    }
}
