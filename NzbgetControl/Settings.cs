using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualBasic.Devices;
using Microsoft.Win32;
using Microsoft.Win32.TaskScheduler;
using NzbgetControl.Misc;
using RamCleaner;
using RamDisk;
using Usenet.Nzb;
using static RamCleaner.Constants.App;

namespace NzbgetControl
{
    public class Settings
    {


        public string Host { get; set; } = "";
        public int Port { get; set; } = 119;
        public string User { get; set; } = "";
        public string Password { get; set; } = "";
        public int Connections { get; set; } = 1;

        public bool Ssl { get; set; } = false;
        public bool Extract { get; set; } = true;
        public bool RootExtract { get; set; } = false;

        public bool Ramdisk { get; set; } = false;

        public bool Cleanram { get; set; } = false;
        public DirectoryInfo OutDir { get; set; } = null;

        public FileInfo NzbFile { get; set;  } = null;

        public string NzbGetCmd { get; } = "";

        public string UnrarCmd { get; } = "";
        public string SevenZipCmd { get; } = "";

        public string BasePathNzbGet { get; set; } = "";

        public bool saveini { get; private set; }

        public bool taskLoad { get; private set; }

        public static string ForceRegister = "";

        /*
        private NzbDocument NzbData;

        private int NzbSize = 0;

        private char RamDriveLetter;
        */

        public Settings(string[] args,string forceregister="")
        {
            if (forceregister != "") ForceRegister = forceregister;

            taskLoad = false;
            string pathNzbGet = Util.checkInstalled("NZBGet");
            if (String.IsNullOrEmpty(pathNzbGet)) throw new FileNotFoundException("NZBGet not installed");
            if(File.Exists(Path.Combine(pathNzbGet, "nzbget.exe")))
            {
                NzbGetCmd = Path.Combine(pathNzbGet, "nzbget.exe");
            }
            else throw new FileNotFoundException(Path.Combine(pathNzbGet, "nzbget.exe") + " missing");

            if (File.Exists(Path.Combine(pathNzbGet, "unrar.exe")))
            {
                UnrarCmd = Path.Combine(pathNzbGet, "unrar.exe");
            }
            else throw new FileNotFoundException(Path.Combine(pathNzbGet, "unrar.exe") + " missing");

            if (File.Exists(Path.Combine(pathNzbGet, "7za.exe")))
            {
                SevenZipCmd = Path.Combine(pathNzbGet, "7za.exe");
            }
            else throw new FileNotFoundException(Path.Combine(pathNzbGet, "7za.exe") + " missing");

            ParseArgs(args);
            
        }

        private static Dictionary<string, string> parametersWithArg = new Dictionary<string, string>
        {
            { "h", "host" },
            { "p", "port" },
            { "q", "user" },
            { "r", "password" },
            { "c", "connections" },
            { "x", "extract" },
            { "o", "out" },
        };

        private static Dictionary<string, string> parametersWithoutArg = new Dictionary<string, string>
        {
            { "s", "ssl" },
            { "ram", "ramdisk" },
            { "cleanram", "cleanram" },
            { "rootextract", "rootextract" },
            { "save","saveini" },
            { "register","register" },
            { "unregister","unregister" },
            { "taskload","taskload" }

        };

        public static void PrintHelp()
        {
            Console.WriteLine("nzbdownload <nzbfile> <options>");

            Console.WriteLine("Parameters :");
            foreach (var param in parametersWithArg)
            {
                Console.WriteLine(String.Format("-{0} <value> or  --{1} <value>", param.Key,param.Value) );
            }

            Console.WriteLine("No Arguments parameters :");
            foreach (var param in parametersWithoutArg)
            {
                Console.WriteLine(String.Format("--{0}", param.Value));
            }
        }

        public void ParseArgs(string[] args)
        {
            LoadIni();
            string activeParameter = "";
            foreach(string arg in args)
            {
                if (arg.Trim().ToLower() == "--filefromini")
                {
                    string inifile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "config",Assembly.GetExecutingAssembly().GetName().Name + ".ini");
                    var MyIni = new IniFile(inifile);

                    if (MyIni.KeyExists("FileFromIni"))
                    {
                        string fileFromIni = MyIni.Read("FileFromIni");
                        if (NzbFile == null)
                        {
                            if (File.Exists(fileFromIni.Trim()))
                            {

                                var fileInfo = new FileInfo(fileFromIni.Trim());
                                if (fileInfo.Extension.ToLower() == ".nzb")
                                {

                                    NzbFile = fileInfo;
                                    if (OutDir == null) OutDir = NzbFile.Directory;
                                    BasePathNzbGet = Path.Combine(NzbFile.Directory.FullName, "__WORK");
                                    Ramdisk = true;
                                }
                            }
                        }
                    } 


                }
                else if (arg.Trim().ToLower() == "--taskload")
                {
                    taskLoad = true;

                }
                else if (arg.Trim().ToLower() == "--register")
                {
                    bool isAdmin = new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
                    if (isAdmin)
                    {
                        using (TaskService ts = new TaskService())
                        {
                            TaskDefinition td = ts.NewTask();
                            td.RegistrationInfo.Description = "NzbgetControl, used for admin right for mounting ramdrive without prompting uac2";

                            td.Principal.RunLevel = TaskRunLevel.Highest;
                            td.Principal.LogonType = TaskLogonType.InteractiveToken;

                            // Create an action that will launch Notepad whenever the trigger fires
                            td.Actions.Add(GetAppName(), "--filefromini", null);
                            
                            // Register the task in the root folder
                            ts.RootFolder.RegisterTaskDefinition(@"NzbgetControl", td, TaskCreation.CreateOrUpdate, Environment.GetEnvironmentVariable("USERNAME"), null, TaskLogonType.InteractiveToken, null);

                            // Remove the task we just created
                            // ts.RootFolder.DeleteTask("Test");
                            Console.WriteLine("done");
                        }
                        Microsoft.Win32.RegistryKey key = Registry.ClassesRoot.OpenSubKey("SystemFileAssociations", true);
                        key.CreateSubKey(".nzb");
                        key = key.OpenSubKey(".nzb", true);
                        key.CreateSubKey("shell");
                        key = key.OpenSubKey("shell", true);
                        key.CreateSubKey("NzbGetControl");
                        key = key.OpenSubKey("NzbGetControl", true);
                        key.CreateSubKey("command");
                        key = key.OpenSubKey("command", true);
                        key.SetValue("", "\"" + GetAppName() + "\" \"%1\"" );

                        key = Registry.ClassesRoot.OpenSubKey("SystemFileAssociations", true);
                        key.CreateSubKey(".nzb");
                        key = key.OpenSubKey(".nzb", true);
                        key.CreateSubKey("shell");
                        key = key.OpenSubKey("shell", true);
                        key.CreateSubKey("NzbGetControl With RamDisk");
                        key = key.OpenSubKey("NzbGetControl With RamDisk", true);
                        key.CreateSubKey("command");
                        key = key.OpenSubKey("command", true);
                        key.SetValue("", "\"" + GetAppName() + "\" --ramdisk --taskload \"%1\"");

                    }
                    else
                    {

                        Console.WriteLine("You need admin right to register");
                    }
                }
                else if (arg.Trim().ToLower() == "--unregister")
                {
                    bool isAdmin = new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
                    if (isAdmin)
                    {
                        using (TaskService ts = new TaskService())
                        {
                            TaskDefinition td = ts.NewTask();
                            ts.RootFolder.DeleteTask("NzbgetControl");
                            Console.WriteLine("done");

                            Microsoft.Win32.RegistryKey key = Registry.ClassesRoot.OpenSubKey("SystemFileAssociations", true);
                            key.CreateSubKey(".nzb");
                            key = key.OpenSubKey(".nzb", true);
                            key.CreateSubKey("shell");
                            key = key.OpenSubKey("shell", true);
                            key.DeleteSubKeyTree("NzbGetControl");
                            key.DeleteSubKeyTree("NzbGetControl With RamDisk");

                        }
                    }
                    else
                    {
                        Console.WriteLine("You need admin right to unregister");
                    }
                }

                else if (arg.StartsWith("--") && parametersWithoutArg.ContainsValue(arg.Substring(2).ToLower()))
                {
                    activeParameter = "";
                    string paramKey = arg.Trim().Substring(2).ToLower();
                    SetPorpertyNoCase(paramKey, "true");
                    //if(SetPorpertyNoCase(paramKey, "true")) Console.WriteLine(paramKey + " = true");
                    continue;
                }
                else if (arg.StartsWith("-") && parametersWithoutArg.ContainsKey(arg.Substring(1).ToLower()))
                {
                    activeParameter = "";
                    string paramKey = parametersWithoutArg[arg.Trim().Substring(1).ToLower()];
                    SetPorpertyNoCase(paramKey, "true");
                    //if (SetPorpertyNoCase(paramKey, "true")) Console.WriteLine(paramKey + " = true");
                    continue;
                }
                else if (arg.StartsWith("--") && parametersWithArg.ContainsValue(arg.Trim().Substring(2).ToLower()))
                {
                    activeParameter = arg.Trim().Substring(2).ToLower();
                    continue;
                }
                else if (arg.StartsWith("-") && parametersWithArg.ContainsKey(arg.Trim().Substring(1).ToLower()))
                {
                    activeParameter = parametersWithArg[arg.Trim().Substring(1).ToLower()];
                    continue;
                }
                else
                {
                    if(activeParameter != "")
                    {
                        switch (activeParameter)
                        {
                            
                            case "out":
                                if (Directory.Exists(arg.Trim()))
                                {
                                    OutDir = new DirectoryInfo(arg.Trim());
                                   // Console.WriteLine("OutDir = " + OutDir.FullName);
                                }
                                else
                                {
                                    //Console.WriteLine("Invalid out directory");
                                    OutDir = null;
                                }
                                break;
                            

                            default:
                                string paramKey = activeParameter;
                                SetPorpertyNoCase(paramKey, arg);
                                //if (SetPorpertyNoCase(paramKey, arg)) Console.WriteLine(paramKey + " = " + arg);
                                break;


                        }
                        activeParameter = "";
                    }
                    else if(NzbFile == null)
                    {
                        if (File.Exists(arg.Trim( )))
                        {

                            var fileInfo = new FileInfo(arg.Trim());
                            if(fileInfo.Extension.ToLower() == ".nzb")
                            {

                                NzbFile = fileInfo;
                                if (OutDir == null) OutDir = NzbFile.Directory;
                                BasePathNzbGet = Path.Combine(NzbFile.Directory.FullName, "__WORK");
                            }
                        }
                    }
                }
            }
            if(taskLoad && NzbFile != null) 
            {
                string inifile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "config", Assembly.GetExecutingAssembly().GetName().Name + ".ini");
                var MyIni = new IniFile(inifile);
                MyIni.Write("FileFromIni", NzbFile.FullName);
            }

            if (saveini)
            {
                Console.WriteLine("Save config into ini file");
                SaveIni();
            }

        }

        public void SaveIni()
        {
            string inifile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "config", Assembly.GetExecutingAssembly().GetName().Name + ".ini");
            var MyIni = new IniFile(inifile);
            foreach (var property in GetType().GetProperties())
            {
                if(property.PropertyType.IsPublic && property.GetSetMethod() != null && property.Name.ToLower() != "ramdisk")
                {
                    if(parametersWithArg.ContainsValue(property.Name.ToLower()) || parametersWithoutArg.ContainsValue(property.Name.ToLower()))
                    {
                        MyIni.Write(property.Name, property.GetValue(this, null).ToString());
                    }
                }

            }
        }

        public void LoadIni()
        {
            string inifile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),"config",Assembly.GetExecutingAssembly().GetName().Name + ".ini");
            
            var MyIni = new IniFile(inifile);
            foreach (var property in GetType().GetProperties())
            {
                if (property.PropertyType.IsPublic && property.GetSetMethod() != null)
                {
                    if (parametersWithArg.ContainsValue(property.Name.ToLower()) || parametersWithoutArg.ContainsValue(property.Name.ToLower()) && property.Name.ToLower() != "ramdisk")
                    {
                        if(MyIni.KeyExists(property.Name)) SetPorpertyNoCase(property.Name, MyIni.Read(property.Name));
                    }
                }

            }
        }


        public bool CheckSettings()
        {
            bool isValidSettings = true;
            if (Host == "") isValidSettings = false;
            if (OutDir == null) isValidSettings = false;
            if (NzbFile == null) isValidSettings = false;
            if (NzbGetCmd == "") isValidSettings = false;
            if (UnrarCmd == "") isValidSettings = false;
            if (SevenZipCmd == "") isValidSettings = false;
            return isValidSettings;
        }

        private bool SetPorpertyNoCase(string key, string val)
        {
            key = key.Trim().ToLower();
            foreach (var property in GetType().GetProperties())
            {
                if (property.Name.ToLower().Contains(key))
                {
                    if(val is string && property.PropertyType == typeof(string))
                    {
                        property.SetValue(this,val, null);
                        return true;
                    }
                    else if (property.PropertyType == typeof(int))
                    {
                        int intVal;
                        if (int.TryParse(val, out intVal))
                        {
                            property.SetValue(this, intVal, null);
                            return true;
                        }
                        else return false;
                    }
                    else if (property.PropertyType == typeof(bool))
                    {
                        if (val == "1" || val == "true" || val == "True" || val == "y" || val == "yes")
                        {
                            property.SetValue(this, true, null);
                            return true;
                        }
                        else if (val == "0" || val == "false" || val == "False" || val == "n" || val == "no")
                        {
                            property.SetValue(this, false, null);
                            return true;
                        }
                        else return false;
                    }
                } 
            }
            return false;

        }

        public static string GetAppName()
        {
            string ext = Path.GetExtension(Assembly.GetExecutingAssembly().Location);

            return ext == ".dll" ? Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "NzbgetControl.exe") : Assembly.GetExecutingAssembly().Location;
        }

        public override string ToString()
        {
            string res = "Parameters : \n";
            foreach (var property in GetType().GetProperties())
            {
                //if (property.PropertyType.IsPublic && parametersWithArg.ContainsValue(property.Name.ToLower() ))
                if (property.PropertyType.IsPublic && property.GetSetMethod() != null && (parametersWithArg.ContainsValue(property.Name.ToLower()) || parametersWithoutArg.ContainsValue(property.Name.ToLower())))
                {
                    string value = "";

                    if (property.PropertyType == typeof(string) || property.PropertyType == typeof(bool) || property.PropertyType == typeof(int))
                    {
                        object obj = property.GetValue(this, null);
                        value = obj.ToString();
                    }
                    if (property.PropertyType == typeof(FileInfo))
                    {
                        FileInfo fileInfo = (FileInfo)property.GetValue(this, null);
                        if (fileInfo != null) value = fileInfo.FullName;
                    }
                    if (property.PropertyType == typeof(DirectoryInfo))
                    {
                        DirectoryInfo directoryInfo = (DirectoryInfo)property.GetValue(this, null);
                        if (directoryInfo != null) value = directoryInfo.FullName;
                    }
                    res += String.Format("{0} = {1} \n", property.Name, value);
                }
            }
            return res;
        }

    }
}
