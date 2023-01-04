using CommonLib.DlkSystem;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Diagnostics;
using Microsoft.VisualBasic.Devices;
using System.Threading;

namespace SchedulingAgent
{
    public class Machine
    {
        private BackgroundWorker bgWorkerMonitorMemoryUsage;
        public string PeakMemoryAndDate { get; set; }

        private double peakMemory = 0;
        private TimeSpan runTime = new TimeSpan(0);
        private string operatingSystem;
        private double totalMemory = 0;
        private double freeSpace = 0;

        public double PeakMemory
        {
            get { return peakMemory; }
        }

        public TimeSpan RunTime
        {
            get { return runTime; }
        }

        public string OperatingSystem
        {
            get { return operatingSystem; }
        }

        public double TotalMemory
        {
            get { return totalMemory; }
        }

        public double FreeSpace
        {
            get { return freeSpace; }
        }

        public enum SizeUnit
        {
            KiloBytes = 1,
            MegaBytes = 2,
            GigaBytes = 3
        }

        public Machine()
        {
            //initialize stats that are unlikely to change
            //OS
            operatingSystem = new ComputerInfo().OSFullName;
            //RAM
            double totalRAMNative = new ComputerInfo().TotalPhysicalMemory;
            totalMemory = Math.Round(totalRAMNative / Math.Pow(1024, (int)Machine.SizeUnit.MegaBytes), 2);
        }

        /// <summary>
        /// Returns the free disk space on the current directory
        /// </summary>
        /// <param name="sizeUnit"></param>
        /// <returns></returns>
        private static double GetFreeSpace(SizeUnit sizeUnit)
        {
            string dLetter = Path.GetPathRoot(Environment.CurrentDirectory);
            DriveInfo dInfo = new DriveInfo(dLetter);
            long freeSpaceNat = dInfo.AvailableFreeSpace;
            return Math.Round(freeSpaceNat / (Math.Pow(1024, (int)sizeUnit)), 2);
        }

        /// <summary>
        /// Starts monitoring the memory usage
        /// </summary>
        public void MonitorMemoryUsage()
        {
            bgWorkerMonitorMemoryUsage = new BackgroundWorker();
            bgWorkerMonitorMemoryUsage.DoWork += bgWorkerMachineInfo_DoWork;
            bgWorkerMonitorMemoryUsage.RunWorkerAsync();
        }

        /// <summary>
        /// Backgroundworker for checking memory used
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgWorkerMachineInfo_DoWork(object sender, DoWorkEventArgs e)
        {
            while(true)
            {
                //to avoid burning too much cpu and ram
                Thread.Sleep(2000);

                //Peak Ram
                ComputerInfo cmp = new ComputerInfo();
                double memoryUsage = cmp.TotalPhysicalMemory - cmp.AvailablePhysicalMemory;
                memoryUsage = Math.Round(memoryUsage / Math.Pow(1024, (int)SizeUnit.MegaBytes), 2);
                if (peakMemory < memoryUsage)
                {
                    peakMemory = memoryUsage;
                    PeakMemoryAndDate = peakMemory.ToString() + " MB [" + System.DateTime.Now.ToString() + "]";
                }

                //Run Time
                runTime = Process.GetCurrentProcess().StartTime.Subtract(System.DateTime.Now);

                //FreeSpace
                freeSpace = GetFreeSpace(Machine.SizeUnit.GigaBytes);
            }
        }
    }
}
