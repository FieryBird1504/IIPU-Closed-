using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Management;
using System.Timers;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            aTimer = new System.Windows.Threading.DispatcherTimer();
            aTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            aTimer.Interval = new TimeSpan(0, 0, 1);
            aTimer.Start();
        }
        [DllImport("cppdll.dll", EntryPoint = "GetTimeout")]
        private static extern int GetTimeout();
        [DllImport("cppdll.dll", EntryPoint = "StatusCheck")]
        private static extern int StatusCheck();
        [DllImport("cppdll.dll", EntryPoint = "RemainTime")]
        private static extern int RemainTime();
        [DllImport("cppdll.dll", EntryPoint = "RemainPercent")]
        private static extern int RemainPercent();

        [DllImport("cppdll.dll", EntryPoint = "SetTimeout")]
        public static extern void SetTimeout(int timeInS);
    }
}
