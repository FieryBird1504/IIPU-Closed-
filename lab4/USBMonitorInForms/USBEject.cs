using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace USBMonitorInForms
{
    class USBEject
    {
        const int OPEN_EXISTING = 3;
        const uint GENERIC_READ = 0x80000000;
        const uint GENERIC_WRITE = 0x40000000;
        const uint IOCTL_STORAGE_EJECT_MEDIA = 0x2D4808;

        [System.Runtime.InteropServices.DllImport("kernel32")]
        private static extern int CloseHandle(IntPtr handle);

        [System.Runtime.InteropServices.DllImport("kernel32")]
        private static extern int DeviceIoControl
            (IntPtr deviceHandle, uint ioControlCode,
              IntPtr inBuffer, int inBufferSize,
              IntPtr outBuffer, int outBufferSize,
              ref int bytesReturned, IntPtr overlapped);

        [System.Runtime.InteropServices.DllImport("kernel32")]
        private static extern IntPtr CreateFile
            (string filename, uint desiredAccess,
              uint shareMode, IntPtr securityAttributes,
              int creationDisposition, int flagsAndAttributes,
              IntPtr templateFile);

        [System.Runtime.InteropServices.DllImport("kernel32")]
        private static extern int GetLastError();

        public static void EjectDrive(string driveLetter)
        {
            string path = "//./" + driveLetter;

            IntPtr handle = CreateFile(path, GENERIC_READ | GENERIC_WRITE, 0,
                IntPtr.Zero, OPEN_EXISTING, 0, IntPtr.Zero);

            if ((long)handle == -1)
            {
                MessageBox.Show("USB device is not able to remove!",
                "USB Monitor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int dummy = 0;
            int returnValue = DeviceIoControl(handle, IOCTL_STORAGE_EJECT_MEDIA,
                     IntPtr.Zero, 0, IntPtr.Zero, 0, ref dummy, IntPtr.Zero);
            CloseHandle(handle);
            MessageBox.Show("USB device is able to remove",
            "USB Monitor", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }
}
