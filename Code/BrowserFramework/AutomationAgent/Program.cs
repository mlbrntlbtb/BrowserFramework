using System;
using System.Configuration;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Net;

namespace AutomationAgent
{
    class Program
    {
        public static void Main(String [] args)
        {
            Server.Start();
        }
    }
}
