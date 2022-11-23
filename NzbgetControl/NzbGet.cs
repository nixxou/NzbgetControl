using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RamCleaner;
using RamDisk;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Usenet.Nzb;

namespace NzbgetControl
{
    internal class NzbGet : IDisposable
    {

        public bool IsLaunched { get; private set; } = false;
        public int Port { get; }
        public string User { get;  }
        public string Password { get; }

        private Settings _settings;

        private RamDiskManager _ramDiskManager;

        private NzbDocument _nzbDocument;

        private ProgressBar _progressBar;
        private long _nzbsize = 0;
        private string _status = "";
        private bool _isDownloading = false;

        private int _neededSizeDrive = 0;

        public NzbGet(Settings settings,int port, string user, string password)
        {
            Port = port;
            User = user;
            Password = password;
            _settings = settings;

            if(_settings.taskLoad)
            {
                Console.WriteLine("Do Taskload !");
                return;
            }

            if (_settings.Ramdisk)
            {
                if (RamDiskManager.isDriverInstalled() && RamDiskManager.isAdminRight())
                {
                    _nzbDocument = NzbParser.Parse(File.ReadAllText(_settings.NzbFile.FullName));
                    int sizeNzbMb = (int)(_nzbDocument.Size / 1024 / 1024);
                    int neededSizeDrive = (int)(sizeNzbMb * 1.3) + 250;
                    if (neededSizeDrive < RamDiskManager.getFreeRamMb())
                    {
                        _neededSizeDrive = neededSizeDrive;
                        if (settings.Cleanram)
                        {
                            Console.WriteLine("Clean Ram !");
                            RamCleaner.Program.Run();
                            Thread.Sleep(1000);
                        }

                        //_ramDiskManager = new RamDiskManager(neededSizeDrive);
                        //_settings.BasePathNzbGet = _ramDiskManager.RamDriveLetter + ":\\";
                    }
                    else _settings.Ramdisk = false;
                }
                else _settings.Ramdisk = false;
            }
            //WaitUntilOffLine();
        }

        public void Start()
        {
            if (_settings.taskLoad)
            {
                Task<int> resultKillTask = Task.Run(() => RunProcessAsync2("SCHTASKS.EXE", "/RUN /TN \"NzbgetControl\""));
                resultKillTask.Wait();
                return;
            }
            if (_settings.Ramdisk)
            {
                _ramDiskManager = new RamDiskManager(_neededSizeDrive);
                _settings.BasePathNzbGet = _ramDiskManager.RamDriveLetter + ":\\";
            }
            IsLaunched = true;
            WaitUntilOffLine();

            var result = Task.Run(async () => await ExecuteNZB()).Result;
        }

        public async Task<int> ExecuteNZB()
        {
            var resultThing = Task.Run<int>(async () => await LaunchNzb());
            WaitUntilOnline();
            Console.WriteLine("Add NZB : " + _settings.NzbFile.Name);
            var nzbTaskSubmit = RunProcessAsync(_settings.NzbGetCmd
, "-o ControlUsername=\"" + User + "\"  -o ControlPassword=\"" + Password + "\" -o ControlPort=" + Port + "  -A \"" + _settings.NzbFile.FullName + "\"");
            using (var wc = new WebClientWithTimeout())
            {
                wc.Timeout = 2000;
                wc.Credentials = new NetworkCredential(User, Password);
                wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(DownloadListgroupsCallback);
                wc.DownloadStringAsync(new Uri("http://127.0.0.1:" + Port + "/jsonrpc/listgroups"));
            }
            await nzbTaskSubmit;
            Console.WriteLine("NZB Added");


            await resultThing;

            return 0;
        }



        public async Task<int> LaunchNzb()
        {
            WaitUntilOffLine();
            Console.WriteLine("Starting NZBGet");
            var parameters = String.Join("",
            "-n ",
            "-o TempDir=\"" + Path.Combine(_settings.BasePathNzbGet, "tmp") + "\" ",
            "-o DestDir=\"" + _settings.OutDir.FullName + "\" ",
            "-o InterDir=\"" + Path.Combine(_settings.BasePathNzbGet, "inter") + "\" ",
            "-o QueueDir=\"" + Path.Combine(_settings.BasePathNzbGet, "queue") + "\" ",
            "-o NzbDir=\"" + Path.Combine(_settings.BasePathNzbGet, "nzb") + "\" ",
            "-o LockFile=\"" + Path.Combine(_settings.BasePathNzbGet, "nzbget.lock") + "\" ",
            //"-o LogFile=\"" + Path.Combine(_settings.BasePathNzbGet, "nzbget.log") + "\" ",
            "-o LogFile=\"\" ",
            @"-o Server1.Active=yes ",
            @"-o Server1.Name=" + _settings.Host + " ",
            @"-o Server1.Level=0 ",
            @"-o Server1.Optional=no ",
            @"-o Server1.Group=0 ",
            @"-o Server1.Host=" + _settings.Host + " ",
            @"-o Server1.Port=" + _settings.Port + " ",
            @"-o Server1.Username=" + _settings.User + " ",
            @"-o Server1.Password=" + _settings.Password + " ",
            @"-o Server1.JoinGroup=no ",
            @"-o Server1.Encryption=" + (_settings.Ssl ? "yes" : "no") + " ",
            @"-o Server1.Connections=" + _settings.Connections + " ",
            @"-o Server1.Retention=0 ",
            @"-o Server1.IpVersion=auto ",
            @"-o ContinuePartial=""no"" ",
            @"-o PostStrategy=""sequential"" ",
            @"-o FileNaming=""auto"" ",
            @"-o ParBuffer=""250"" ",
            @"-o ParThreads=""0"" ",
            @"-o HealthCheck=""park"" ",
            @"-o DirectRename=""yes"" ",
            @"-o WriteBuffer=""1024"" ",
            @"-o NzbCleanupDisk=""yes"" ",
            "-o Unpack=\"" + (_settings.Extract ? "yes" : "no") + "\" ",
            @"-o DirectUnpack=""yes"" ",
            @"-o UnpackCleanupDisk=""yes"" ",
            "-o UnrarCmd=\"" + _settings.UnrarCmd + "\" ",
            "-o SevenZipCmd=\"" + _settings.SevenZipCmd + "\" ",
            @"-o ExtCleanupDisk="".par2, .sfv"" ",
            @"-o ParIgnoreExt="".sfv, .nzb, .nfo"" ",
            @"-o UnpackIgnoreExt="".cbr"" ",
            @"-o ArticleCache=""250"" ",
            @"-o ReorderFiles=""yes"" ",
            "-o ControlUsername=\"" + User + "\" ",
            "-o ControlPassword=\"" + Password + "\" ",
            "-o ControlPort=" + Port + " ",
            @"-o ControlIP=""127.0.0.1"" ",
            //@"-o OutputMode=""Log"" ",
            "-s");
            //Console.WriteLine(_settings.NzbGetCmd);
            //Console.WriteLine(parameters);
            var nzbTask = RunProcessAsync(_settings.NzbGetCmd, parameters);
            await nzbTask;
            return 0;

        }

        public void WaitUntilOffLine()
        {
            bool nzbGetOnline = true;
            while (nzbGetOnline)
            {
                using (var wc = new WebClientWithTimeout())
                {
                    wc.Timeout = 1000;
                    wc.Credentials = new NetworkCredential(User, Password);
                    try
                    {
                        string result = wc.DownloadString("http://127.0.0.1:" + Port + "/jsonrpc/listgroups");
                        if (!String.IsNullOrEmpty(result))
                        {
                            nzbGetOnline = true;
                        }
                    }
                    catch (System.Net.WebException e) when (e.Status.ToString() == "Timeout")
                    {
                        nzbGetOnline = false;
                    }
                }
                if (nzbGetOnline) Thread.Sleep(500);
            }
        }

        public void WaitUntilOnline()
        {
            bool nzbGetOnline = false;
            while (!nzbGetOnline)
            {
                using (var wc = new WebClientWithTimeout())
                {
                    wc.Timeout = 1000;
                    wc.Credentials = new NetworkCredential(User, Password);
                    try
                    {
                        string result = wc.DownloadString("http://127.0.0.1:" + Port + "/jsonrpc/listgroups");
                        if (!String.IsNullOrEmpty(result))
                        {
                            if (IsValidJson(result))
                            {
                                JObject o = JObject.Parse(result);
                                if (o.ContainsKey("version"))
                                {
                                    nzbGetOnline = true;
                                }
                            }
                        }
                    }
                    catch (System.Net.WebException e) when (e.Status.ToString() == "Timeout")
                    {
                        nzbGetOnline = false;
                    }
                }
                if (!nzbGetOnline) Thread.Sleep(500);
            }
        }


        private void DownloadListgroupsCallback(object sender, DownloadStringCompletedEventArgs e)
        {
            if (!e.Cancelled && e.Error == null)
            {
                string textString = (string)e.Result;
                if (IsValidJson(textString))
                {
                    JObject o = JObject.Parse(textString);

                    var tokenStatus = o.SelectToken("$.result[0].Status");
                    if (tokenStatus != null)
                    {
                        string status = o["result"][0]["Status"].ToString();
                        if (_status != status)
                        {
                            _status = status;
                            if (_isDownloading) _progressBar.Dispose();
                            Console.WriteLine("NEW STATUS : " + _status);
                            
                        }
                    }
                    else
                    {
                        if (_isDownloading)
                        {
                            Console.WriteLine("Fini !");
                            Thread.Sleep(1000);

                            using (var wc = new WebClientWithTimeout())
                            {
                                wc.Timeout = 2000;
                                wc.Credentials = new NetworkCredential(User, Password);
                                string result = wc.DownloadString("http://127.0.0.1:" + Port + "/jsonrpc/history");
                                if (!String.IsNullOrEmpty(result) && IsValidJson(result))
                                {
                                    JObject oHistory = JObject.Parse(result);
                                    var tokenHistory = oHistory.SelectToken("$.result[0].Status");
                                    if (tokenHistory != null)
                                    {
                                        Console.WriteLine("Resultat : " + oHistory["result"][0]["Status"].ToString());
                                    }
                                }
                            }

                            Task<int> resultKillTask = Task.Run(() => RunProcessAsync(_settings.NzbGetCmd, "-o ControlUsername=\"" + User + "\"  -o ControlPassword=\"" + Password + "\" -o ControlPort=" + Port + "  -Q"));
                            resultKillTask.Wait();


                            return;



                        }
                    }

                    var token = o.SelectToken("$.result[0].RemainingSizeMB");
                    if (token != null)
                    {

                        _isDownloading = true;
                        long restant = long.Parse(o["result"][0]["RemainingSizeLo"].ToString());
                        if (_nzbsize == 0)
                        {
                            _progressBar = new ProgressBar();
                            _nzbsize = restant;
                        }
                        
                        double pourcent = 1.0-((double)restant / (double)_nzbsize);

                        _progressBar.Report(pourcent);
                    }


                }

            }
            else Console.WriteLine("Echec");
            Thread.Sleep(500);
            if (IsLaunched)
            {
                using (var wc = new WebClientWithTimeout())
                {
                    wc.Timeout = 2000;
                    wc.Credentials = new NetworkCredential(User, Password);
                    wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(DownloadListgroupsCallback);
                    wc.DownloadStringAsync(new Uri("http://127.0.0.1:" + Port + "/jsonrpc/listgroups"));
                }

            }

        }

        private static bool IsValidJson(string strInput)
        {
            if (string.IsNullOrWhiteSpace(strInput)) { return false; }
            strInput = strInput.Trim();
            if ((strInput.StartsWith("{") && strInput.EndsWith("}")) || //For object
                (strInput.StartsWith("[") && strInput.EndsWith("]"))) //For array
            {
                try
                {
                    var obj = JToken.Parse(strInput);
                    return true;
                }
                catch (JsonReaderException jex)
                {
                    //Exception in parsing json
                    Console.WriteLine(jex.Message);
                    return false;
                }
                catch (Exception ex) //some other exception
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static async Task<int> RunProcessAsync(string fileName, string args)
        {
            using (var process = new Process
            {
                StartInfo =
        {
            FileName = fileName, Arguments = args,
            UseShellExecute = false, CreateNoWindow = true,
            RedirectStandardOutput = true, RedirectStandardError = true
        },
                EnableRaisingEvents = true
            })
            {
                return await RunProcessAsync(process).ConfigureAwait(false);
            }
        }
        private static Task<int> RunProcessAsync(Process process)
        {
            var tcs = new TaskCompletionSource<int>();

            process.Exited += (s, ea) => tcs.SetResult(process.ExitCode);
            //process.OutputDataReceived += (s, ea) => Console.WriteLine(ea.Data);
            //process.ErrorDataReceived += (s, ea) => Console.WriteLine("ERR: " + ea.Data);

            bool started = process.Start();
            if (!started)
            {
                throw new InvalidOperationException("Could not start process: " + process);
            }

            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            return tcs.Task;
        }

        public static async Task<int> RunProcessAsync2(string fileName, string args)
        {
            using (var process = new Process
            {
                StartInfo =
        {
            FileName = fileName, Arguments = args,
            UseShellExecute = false, CreateNoWindow = true,
            RedirectStandardOutput = true, RedirectStandardError = true
        },
                EnableRaisingEvents = true
            })
            {
                return await RunProcessAsync2(process).ConfigureAwait(false);
            }
        }
        private static Task<int> RunProcessAsync2(Process process)
        {
            var tcs = new TaskCompletionSource<int>();

            process.Exited += (s, ea) => tcs.SetResult(process.ExitCode);
            process.OutputDataReceived += (s, ea) => Console.WriteLine(ea.Data);
            process.ErrorDataReceived += (s, ea) => Console.WriteLine("ERR: " + ea.Data);

            bool started = process.Start();
            if (!started)
            {
                throw new InvalidOperationException("Could not start process: " + process);
            }

            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            return tcs.Task;
        }

        protected virtual void Dispose(Boolean disposing)
        {
            if (disposing)
            {
                if(!_settings.taskLoad)
                {
                    if (_progressBar != null) _progressBar.Dispose();
                    _progressBar = null;

                    IsLaunched = false;
                    using (var wc = new WebClientWithTimeout())
                    {
                        wc.Timeout = 1000;
                        wc.Credentials = new NetworkCredential(User, Password);
                        try
                        {
                            string result = wc.DownloadString("http://127.0.0.1:" + Port + "/jsonrpc/listgroups");
                            if (!String.IsNullOrEmpty(result))
                            {
                                Task<int> resultKillTask = Task.Run(() => RunProcessAsync2(_settings.NzbGetCmd, "-o ControlUsername=\"" + User + "\"  -o ControlPassword=\"" + Password + "\" -o ControlPort=" + Port + "  -Q"));
                                resultKillTask.Wait();
                            }
                        }
                        catch (System.Net.WebException e) when (e.Status.ToString() == "Timeout")
                        {

                        }
                    }
                    if (_settings.Ramdisk && _ramDiskManager != null) _ramDiskManager.Dispose();
                    else if (!_settings.Ramdisk && Directory.Exists(_settings.BasePathNzbGet))
                    {
                        Thread.Sleep(1000);
                        Directory.Delete(_settings.BasePathNzbGet, true);
                    }
                }

            }
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);

        }
    }
}
