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
        private void addDirectory(string dirPath)
        {
            string[] filesPathes = Directory.GetFiles(dirPath);
            foreach (string filePath in filesPathes)
            {
                this.fileToBurn.Items.Add(filePath);
                busySpace += (double)(new FileInfo(filePath).Length);
            }
            string[] subDirs = Directory.GetDirectories(dirPath);
            foreach (string subdirPath in subDirs)
            {
                addDirectory(subdirPath);
            }
        }
        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {
                FileAttributes attr = File.GetAttributes(file);
                if (attr.HasFlag(FileAttributes.Directory))
                {
                    addDirectory(file);
                    mediaItems.Add(new DirectoryItem(file));
                }
                else
                {
                    this.fileToBurn.Items.Add(file);
                    busySpace += (double)(new FileInfo(file).Length);
                    mediaItems.Add(new FileItem(file));
                }
            }
            updateProgressBar();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //get available CDS
            MsftDiscMaster2 discMaster = null;
            try
            {
                discMaster = new MsftDiscMaster2();
                if (!discMaster.IsSupportedEnvironment)
                    return;
                foreach (string uniqueRecorderId in discMaster)
                {
                    var discRecorder = new MsftDiscRecorder2();
                    discRecorder.InitializeDiscRecorder(uniqueRecorderId);
                    if (discRecorder.VolumePathNames.Count() > 0)
                    {
                        AvailableCD.Items.Add(discRecorder.VolumePathNames.First().ToString());
                        discs[discRecorder.VolumePathNames.First().ToString()] = discRecorder;
                    }
                }
            }
            catch (COMException ex)
            {
                MessageBox.Show(ex.Message,
                    string.Format("Error:{0} - Please install IMAPI2", ex.ErrorCode),
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            finally
            {
                if (discMaster != null)
                {
                    Marshal.ReleaseComObject(discMaster);
                }
            }
        }
        private void AvailableCD_SelectedIndexChanged(object sender, EventArgs e)
        {
            MsftDiscRecorder2 discRecorder = discs[AvailableCD.Items[AvailableCD.SelectedIndex].ToString()];
            MsftFileSystemImage fileSystemImage = null;
            MsftDiscFormat2Data discFormatData = null;
            try
            {
                discFormatData = new MsftDiscFormat2Data();
                if (!discFormatData.IsCurrentMediaSupported(discRecorder))
                {
                    MessageBox.Show("Media not supported");
                    return;
                }
                else
                {
                    discFormatData.Recorder = discRecorder;
                    fileSystemImage = new MsftFileSystemImage();
                    try
                    {
                        fileSystemImage.ChooseImageDefaultsForMediaType(discFormatData.CurrentPhysicalMediaType);
                    }
                    catch
                    {
                        MessageBox.Show(AvailableCD.SelectedItem.ToString() + " read only");
                        return;
                    }
                    if (!discFormatData.MediaHeuristicallyBlank)
                    {
                        fileSystemImage.MultisessionInterfaces = discFormatData.MultisessionInterfaces;
                        fileSystemImage.ImportFileSystem();
                    }
                    Int64 totalBlocks = fileSystemImage.UsedBlocks + fileSystemImage.FreeMediaBlocks;
                    this.totalSpace = getGbSize(totalBlocks);
                    this.freeSpace = getGbSize(fileSystemImage.FreeMediaBlocks);
                    this.busySpace = getGbSize(fileSystemImage.UsedBlocks);
                    this.updateProgressBar();
                }
            }
            catch (COMException exception)
            {
                MessageBox.Show(this, exception.Message, "Detect Media Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (discFormatData != null)
                {
                    Marshal.ReleaseComObject(discFormatData);
                }

                if (fileSystemImage != null)
                {
                    Marshal.ReleaseComObject(fileSystemImage);
                }
            }
        }
        private void btnBurn_Click(object sender, EventArgs e)
        {
            if (this.fileToBurn.Items.Count == 0)
            {
                MessageBox.Show("Add files to burn", "Info", MessageBoxButtons.OK);
                return;
            }
            else
            {
                if (busySpace / 1024 / 1024 / 1024 > totalSpace)
                {
                    MessageBox.Show("Too many data, delete some files");
                    return;
                }
                ejectMedia = CheckBoxEject.Checked;
                _burnData.uniqueRecorderId = discs[AvailableCD.SelectedItem.ToString()].ActiveDiscRecorder;
                _burnData.volumeName = discs[AvailableCD.SelectedItem.ToString()].VolumeName;
                backgroundWorker1.RunWorkerAsync(_burnData);
            }
        }
    }
}