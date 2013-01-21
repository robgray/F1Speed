using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using F1Speed.Core;
using Nancy;
using Nancy.Hosting.Self;
using System.Configuration;

namespace F1Speed
{
    class Program
    {
        private static NancyHost nancyHost;


        [STAThread]
        static void Main(string[] args)
        {
            Application.ApplicationExit += Application_ApplicationExit;

            var webPort = ConfigurationManager.AppSettings["webport"];
            nancyHost = new NancyHost(new Uri("http://localhost:" + (string.IsNullOrEmpty(webPort) ? "43920" : webPort)));
            nancyHost.Start();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(new TelemetryLapManagerFactory().GetManager()));
        }

        static void Application_ApplicationExit(object sender, EventArgs e)
        {
            nancyHost.Stop();
        }        
    }
}
