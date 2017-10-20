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

        public string FriendlyName
        {
            get
            {
                if (!this.isConnected)
                {
                    throw new InvalidOperationException("Not connected to device.");
                }

                IPortableDeviceContent content;
                IPortableDeviceProperties properties;
                this.device.Content(out content);
                content.Properties(out properties);

                IPortableDeviceValues propertyValues;
                properties.GetValues("DEVICE", null, out propertyValues);

                var property = new _tagpropertykey();
                property.fmtid = new Guid(0x26D4979A, 0xE643, 0x4626, 0x9E, 0x2B,
                                          0x73, 0x6D, 0xC0, 0xC9, 0x2F, 0xDC);
                property.pid = 12;

                string propertyValue;
                propertyValues.GetStringValue(ref property, out propertyValue);

                return propertyValue;
            }
        }
        public void Connect()
        {
            if (this.isConnected) { return; }

            var clientInfo = (IPortableDeviceValues)new PortableDeviceValues();
            this.device.Open(this.DeviceId, clientInfo);
            this.isConnected = true;
        }

        public void Disconnect()
        {
            if (!this.isConnected) { return; }
            try
            {
                this.device.Close();
            }
            catch (Exception)
            {}
            this.isConnected = false;
        }
    }
}