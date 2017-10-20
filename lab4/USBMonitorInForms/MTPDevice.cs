using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortableDeviceApiLib;
using PortableDeviceTypesLib;
using _tagpropertykey = PortableDeviceApiLib._tagpropertykey;
using IPortableDeviceKeyCollection = PortableDeviceApiLib.IPortableDeviceKeyCollection;
using IPortableDeviceValues = PortableDeviceApiLib.IPortableDeviceValues;

namespace USBMonitorInForms
{
    public class MTPDevice
    {
        private bool isConnected;
        private readonly PortableDevice device;

        public MTPDevice(string deviceId)
        {
            device = new PortableDevice();

            this.DeviceId = deviceId;
            this.Connect();
        }
        ~MTPDevice()
        {
            this.Disconnect();
        }

        public string DeviceId { get; set; }
    }
}