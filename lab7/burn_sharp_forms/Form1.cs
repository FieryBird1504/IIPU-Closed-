using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using IMAPI2.Interop;
using burn_sharp_forms.MediaItem;

namespace burn_sharp_forms
{
    public partial class Form1 : Form
    {
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetDiskFreeSpaceEx(string lpDirectoryName,
        out ulong lpFreeBytesAvailable,
        out ulong lpTotalNumberOfBytes,
        out ulong lpTotalNumberOfFreeBytes);

        #region variables
        string clientName = "BurnMedia";
        private IMAPI_BURN_VERIFICATION_LEVEL verificationLevel = IMAPI_BURN_VERIFICATION_LEVEL.IMAPI_BURN_VERIFICATION_NONE;
        double totalSpace = 0;
        double busySpace = 0;
        double freeSpace = 0;
        bool stopWriteInFuture = false;
        bool ejectMedia = false;
        ProgressBarWithCaption progressBarSpaceOnDisc = new ProgressBarWithCaption();
        BurnData _burnData = new BurnData();
        List<IMediaItem> mediaItems = new List<IMediaItem>();
        Dictionary<string, MsftDiscRecorder2> discs = new Dictionary<string, MsftDiscRecorder2>();
        #endregion

        #region Form and conrols
        public Form1()
        {
            this.Controls.Add(progressBarSpaceOnDisc);
            InitializeComponent();
            progressBarSpaceOnDisc.Bounds = this.barFreeSpace.Bounds;
            this.barFreeSpace.Visible = false;
            progressBarSpaceOnDisc.Visible = true;
            progressBarSpaceOnDisc.DisplayStyle = ProgressBarDisplayText.CustomText;
            progressBarSpaceOnDisc.Minimum = 0;
            progressBarSpaceOnDisc.Maximum = 100;
            progressBarSpaceOnDisc.CustomText = String.Format("Select a disc");
            this.AllowDrop = true;
            this.DragEnter += new DragEventHandler(Form1_DragEnter);
            this.DragDrop += new DragEventHandler(Form1_DragDrop);
        }
        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }
    }
}