using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NzbgetControl.Misc;
using OptionConfigure.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Usenet.Nntp;
using Usenet.Nntp.Responses;

namespace OptionConfigure
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void UpdateInstalled()
        {
            if (NzbgetControl.RamDiskManager.isDriverInstalled())
            {
                label_Imdisk_false.Visible = false;
                label_Imdisk_true.Visible = true;
                buttonInstallImDisk.Enabled = false;
            }
            else
            {
                label_Imdisk_false.Visible = true;
                label_Imdisk_true.Visible = false;
                buttonInstallImDisk.Enabled = true;
            }

            string pathNzbGet = Util.checkInstalled("NZBGet");
            if (!String.IsNullOrEmpty(pathNzbGet))
            {
                label_Nzbget_true.Visible=true;
                label_Nzbget_false.Visible = false;
                buttonInstallNzbGet.Enabled = false;
            }
            else
            {
                label_Nzbget_true.Visible = false;
                label_Nzbget_false.Visible = true;
                buttonInstallNzbGet.Enabled = true;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            List<string> fakeArg = new List<string>();
            var args = fakeArg.ToArray();
            NzbgetControl.Settings settings = new NzbgetControl.Settings(args);
            settings.LoadIni();
            textBox_host.Text = settings.Host;
            numericUpDown_port.Value = settings.Port;
            textBox_user.Text = settings.User;
            textBox_password.Text = settings.Password;
            numericUpDown_connections.Value = settings.Connections;
 
            radio_ssl_true.Checked = settings.Ssl;
            radio_ssl_false.Checked = !settings.Ssl;

            radio_extract_true.Checked = settings.Extract;
            radio_extract_false.Checked = !settings.Extract;

            radio_rootextract_true.Checked = settings.RootExtract;
            radio_rootextract_false.Checked = !settings.RootExtract;

            radio_cleanram_true.Checked = settings.Cleanram;
            radio_cleanram_false.Checked = !settings.Cleanram;

            UpdateInstalled();



        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var linkLabel = (LinkLabel)sender;
            System.Diagnostics.Process.Start(linkLabel.Text);
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var linkLabel = (LinkLabel)sender;
            System.Diagnostics.Process.Start(linkLabel.Text);
        }

        private void buttonInstallImDisk_Click(object sender, EventArgs e)
        {

            string tempExeName = Path.Combine(Path.GetTempPath(), "imdiskinst.exe");
            if (File.Exists(tempExeName)) File.Delete(tempExeName);

            //MessageBox.Show(tempExeName);
            using (FileStream fsDst = new FileStream(tempExeName, FileMode.CreateNew, FileAccess.Write))
            {
                byte[] bytes = Resources.Getimdiskinst();

                fsDst.Write(bytes, 0, bytes.Length);
            }
            RunFile(tempExeName);
            File.Delete(tempExeName);

        }

        private int RunFile(string exefile)
        {
            ProcessStartInfo psi = new ProcessStartInfo(exefile);
            //psi.WindowStyle = ProcessWindowStyle.Hidden;
            //psi.CreateNoWindow = true;

            using (Process process = Process.Start(psi))
            {
                process.WaitForExit();
                if (process.HasExited)
                    return process.ExitCode;
            }
            return 0;
        }

        private void buttonInstallNzbGet_Click(object sender, EventArgs e)
        {
            using (var wc = new WebClient())
            {
                wc.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; " +
                                  "Windows NT 5.2; .NET CLR 1.0.3705;)");
                wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(DownloadGitHubLastRlzForNzbget);
                wc.DownloadStringAsync(new Uri("https://api.github.com/repos/nzbget/nzbget/releases/latest"));
            }
        }

        private void DownloadGitHubLastRlzForNzbget(object sender, DownloadStringCompletedEventArgs e)
        {
            if (!e.Cancelled && e.Error == null)
            {
                string textString = (string)e.Result;
                if (IsValidJson(textString))
                {
                    JObject o = JObject.Parse(textString);
                    var windowsRlz = o["assets"]
                                        .Where(b => b.Value<string>("name").Contains("bin-windows-setup.exe"))
                                        //.Where(b => b.Value<string>("name").Contains("bin"))
                                        .OrderByDescending(b => b.Value<int>("id"))
                                        .Select(b => b.Value<string>("browser_download_url"));


                    var windowsRlzArr = windowsRlz.ToArray();
                    if (windowsRlzArr.Length > 0)
                    {
                        string urlRlz = windowsRlzArr[0];
                        //MessageBox.Show(urlRlz);

                        string tempExeName = Path.Combine(Path.GetTempPath(), "nzbget.exe");
                        if (File.Exists(tempExeName)) File.Delete(tempExeName);

                        //MessageBox.Show(tempExeName);
                        using (FileStream fsDst = new FileStream(tempExeName, FileMode.CreateNew, FileAccess.Write))
                        {
                            using (var client = new WebClient())
                            {
                                byte[] bytes = client.DownloadData(urlRlz);
                                fsDst.Write(bytes, 0, bytes.Length);
                            }
                        }
                        RunFile(tempExeName);
                        File.Delete(tempExeName);

                    }



                }
                //MessageBox.Show(textString);
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

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            List<string> fakeArg = new List<string>();
            var args = fakeArg.ToArray();
            NzbgetControl.Settings settings = new NzbgetControl.Settings(args);

            settings.Host = textBox_host.Text;
            settings.Port = (int)numericUpDown_port.Value;
            settings.User = textBox_user.Text;
            settings.Password = textBox_password.Text;
            settings.Connections = (int)numericUpDown_connections.Value;
            

            settings.Ssl = radio_cleanram_true.Checked;
            settings.Extract = radio_extract_true.Checked;
            settings.RootExtract = radio_rootextract_true.Checked;  
            settings.Cleanram = radio_cleanram_true.Checked;

            settings.SaveIni();
            System.Windows.Forms.Application.Exit();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateInstalled();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private async void button1_Click_2(object sender, EventArgs e)
        {
            try
            {
                var client = new NntpClient(new NntpConnection());
                await client.ConnectAsync(textBox_host.Text, (int)numericUpDown_port.Value, radio_ssl_true.Checked);
                client.Authenticate(textBox_user.Text, textBox_password.Text);
                NntpArticleResponse response = client.Article("x");
                if (response.Code == 430)
                {
                    MessageBox.Show("Ok !");
                    return;
                }
            }
            catch(System.Net.Sockets.SocketException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            MessageBox.Show("Erreur");
            return;
            





        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }
    }
}
