using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.Linq;
using System.Diagnostics;
using System.Reflection;
using CommonLib.DlkHandlers;

namespace AutomationAgent
{
    struct ResultsRecord
    {
        public int TotalCount { get; set; }
        public int PassCount { get; set; }
        public int FailCount { get; set; }
    }
    public static class Server
    {
        public static int iTestSuiteTimeOutSec = (60 * (60 * 8)); // wait 8 hrs --> (60s * (60m * n hrs))
        public static void Start()
        {
            // setup the environment
            //AutomationEngine.DlkEnvironment.InitializeEnvironment();
            //AutomationEngine.DlkEnvironment.KillProcessByName("EXCEL");

            // run the server
            UdpClient udpc = new UdpClient(2055);
            Console.WriteLine("AutomationAgent Server started. Servicing on port 2055");
            IPEndPoint ep = null;
            bool bIsData = false;

            // do something when cmd received
            String Response = "", Cmd = "";
            byte[] rdata;
            byte[] sdata = null;
            while (true)
            {
                // receive command
                rdata = udpc.Receive(ref ep);
                Cmd = Encoding.ASCII.GetString(rdata).ToLower();
                Console.WriteLine("Command received: " + Cmd);

                // Perform action based on command
                if (Cmd == "alive")
                {
                    Response = "true";
                    sdata = Encoding.ASCII.GetBytes(Response);
                }
                else if (Cmd == "running")
                {
                    Boolean bIsRunning = IsProcessRunning("testrunnerscheduler");
                    Response = bIsRunning.ToString().ToLower();
                    sdata = Encoding.ASCII.GetBytes(Response);
                }
                else if (Cmd == "start")
                {
                    Boolean bIsStarted = StartScheduler();
                    Response = bIsStarted.ToString().ToLower();
                    sdata = Encoding.ASCII.GetBytes(Response);
                }
                else if (Cmd == "start /r true /d monday")
                {
                    Boolean bIsStarted = StartScheduler("Monday");
                    Response = bIsStarted.ToString().ToLower();
                    sdata = Encoding.ASCII.GetBytes(Response);
                }
                else if (Cmd == "start /r true /d tuesday")
                {
                    Boolean bIsStarted = StartScheduler("Tuesday");
                    Response = bIsStarted.ToString().ToLower();
                    sdata = Encoding.ASCII.GetBytes(Response);
                }
                else if (Cmd == "start /r true /d wednesday")
                {
                    Boolean bIsStarted = StartScheduler("Wednesday");
                    Response = bIsStarted.ToString().ToLower();
                    sdata = Encoding.ASCII.GetBytes(Response);
                }
                else if (Cmd == "start /r true /d thursday")
                {
                    Boolean bIsStarted = StartScheduler("Thursday");
                    Response = bIsStarted.ToString().ToLower();
                    sdata = Encoding.ASCII.GetBytes(Response);
                }
                else if (Cmd == "start /r true /d friday")
                {
                    Boolean bIsStarted = StartScheduler("Friday");
                    Response = bIsStarted.ToString().ToLower();
                    sdata = Encoding.ASCII.GetBytes(Response);
                }
                else if (Cmd == "start /r true /d saturday")
                {
                    Boolean bIsStarted = StartScheduler("Saturday");
                    Response = bIsStarted.ToString().ToLower();
                    sdata = Encoding.ASCII.GetBytes(Response);
                }
                else if (Cmd == "start /r true /d sunday")
                {
                    Boolean bIsStarted = StartScheduler("Sunday");
                    Response = bIsStarted.ToString().ToLower();
                    sdata = Encoding.ASCII.GetBytes(Response);
                }
                else if (Cmd == "stop")
                {
                    Boolean bIsStopped = StopScheduler();
                    Response = bIsStopped.ToString().ToLower();
                    sdata = Encoding.ASCII.GetBytes(Response);
                }
                else if (Cmd == "getschedule")
                {
                    //FileStream fs = File.OpenRead(GetSchedule());

                    Response = GetSchedule();
                    sdata = File.ReadAllBytes(Response);
                }
                else if (Cmd == "setschedule")
                {
                    bIsData = true;
                    Response = "Waiting for schedule data";
                    sdata = Encoding.ASCII.GetBytes(Response);
                }
                else
                {
                    if (bIsData) // preceding command is setschedule
                    {
                        Response = GetSchedule();
                        File.WriteAllBytes(Response, rdata);
                        sdata = Encoding.ASCII.GetBytes(Response);
                        bIsData = false;
                    }
                    else
                    {
                        Response = "ERROR|Unsupported command: " + Cmd;
                        sdata = Encoding.ASCII.GetBytes(Response);
                    }
                }

                Console.WriteLine("Response: " + Response);

                // Send response data in increments of 1472 (Max MTU is 1500?)
                // Note that client need to know how to process response data sent in batces
                int skip = 0;
                while (skip < sdata.Length)
                {
                    byte[] sendnow = sdata.Skip(skip).Take(sdata.Length - skip > 1472 ? 1472 : sdata.Length - skip).ToArray();
                    skip += 1472;
                    udpc.Send(sendnow, sendnow.Length, ep);
                }
            }
        }

        private static string GetSchedule()
        {
            return Path.Combine(DlkConfigHandler.GetConfigRoot("Scheduler"), Environment.MachineName.ToUpper() + ".xml");
        }

        private static bool StartScheduler()
        {
            bool ret = false;
            int timeout = 10;
            RunProcess("TestRunnerScheduler.exe", "", Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), false, -1);
            while (!ret && timeout-- != 0)
            {
                ret = IsProcessRunning("testrunnerscheduler");
                Thread.Sleep(1000);
            }
            return ret;
        }

        //OVERLOAD FOR RUN NOW OF REMOTE MACHINES
        private static bool StartScheduler(string dayToRunNow)
        {
            //Similar process with running the Scheduler manually
            bool ret = false;
            int timeout = 10;
            //STOP SCHEDULER
            StopScheduler();
            RunProcess("TestRunnerScheduler.exe", "/r true /d " + dayToRunNow, Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), false, -1);
            while (!ret && timeout-- != 0)
            {
                ret = IsProcessRunning("testrunnerscheduler");
                Thread.Sleep(1000);
            }
            return ret;
        }


        private static bool StopScheduler()
        {
            bool ret = false;
            int timeout = 10;
            KillProcess("testrunnerscheduler");
            while (!ret && timeout-- != 0)
            {
                ret = !IsProcessRunning("testrunnerscheduler");
                Thread.Sleep(1000);
            }
            return ret;
        }

        public static bool IsProcessRunning(string ProcessName)
        {
            Boolean bIsRunning = false;
            Process[] processes = Process.GetProcesses();

            foreach (Process process in processes)
            {
                if (process.ProcessName.ToLower().Contains(ProcessName.ToLower()))
                {
                    bIsRunning = true;
                    break;
                }
            }
            return bIsRunning;
        }

        public static void KillProcess(string ProcessName)
        {
            Process[] processes = Process.GetProcesses();

            foreach (Process process in processes)
            {
                if (process.ProcessName.ToLower().Contains(ProcessName.ToLower()))
                {
                    process.Kill();
                }
            }
        }

        public static void RunProcess(String Filename, String Arguments, String WorkingDir, Boolean bHideWin, int iTimeoutSec)
        {
            Process p = new Process();
            p.StartInfo.FileName = Filename;
            p.StartInfo.Arguments = Arguments;
            p.StartInfo.WorkingDirectory = WorkingDir;
            p.StartInfo.UseShellExecute = true;
            if (bHideWin)
            {
                p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            }
            p.Start();
            if (iTimeoutSec > 0)
            {
                p.WaitForExit(iTimeoutSec * 1000);
                if (!p.HasExited)
                {
                    p.Kill();
                }
            }
        }
    }

}

