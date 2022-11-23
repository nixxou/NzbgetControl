using Microsoft.VisualBasic.Devices;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using NzbgetControl.Misc;
using RamDisk;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Usenet.Nzb;

namespace NzbgetControl
{
    internal class Program
    {

        static void Main(string[] args)
        {

            //RamDisk.RamDrive.Mount(4000,FileSystem.NTFS);
            //return;


            //List<string> fakeArg = new List<string>();
            //fakeArg.Add(@"Z:\Code.Name.Emperor.2022.FRENCH.720p.HDLight.x264.AC3-EXTREME.nzb");
            //args = fakeArg.ToArray();
            //args = fakeArg.ToArray();


            var settings = new Settings(args);


            if (settings.CheckSettings())
            {
                using (var nzbGet = new NzbGet(settings, 6987, "nzbget", "backup"))
                {
                    Console.CancelKeyPress += delegate (object sender, ConsoleCancelEventArgs e)
                    {
                        Console.WriteLine("Force Exit !");
                        nzbGet.Dispose();
                        System.Environment.Exit(0);
 
                    };
                    nzbGet.Start();
                }
            }
            else
            {
                 
                if (!settings.CheckSettings())
                {
                    Console.WriteLine("Invalid Settings ! \n");
                    Console.WriteLine("HELP : \n");
                    Settings.PrintHelp();
                    Console.WriteLine("CURRENT CONFIG : \n");
                    Console.WriteLine(settings);
                    return;
                }
            }
           
        }

    }

}
