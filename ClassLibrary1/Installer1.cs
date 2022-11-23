using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows.Forms;
using NzbgetControl;
using OptionConfigure;



namespace ClassLibrary1
{
    [RunInstaller(true)]
    public partial class Installer1 : System.Configuration.Install.Installer
    {
        public Installer1()
        {
            InitializeComponent();
        }



        public override void Install(System.Collections.IDictionary stateSaver)
        {
            List<string> fakeArg = new List<string>();
            fakeArg.Add("--register");
            var args = fakeArg.ToArray();
            Settings settings = new Settings(args);

            //Make config folder with write permissions
            string configFolder = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "config");
            Directory.CreateDirectory(configFolder);
            DirectoryInfo dInfo = new DirectoryInfo(configFolder);
            DirectorySecurity dSecurity = dInfo.GetAccessControl();
            dSecurity.AddAccessRule(new FileSystemAccessRule(
                new SecurityIdentifier(WellKnownSidType.WorldSid, null),
                FileSystemRights.FullControl,
                InheritanceFlags.ObjectInherit |
                   InheritanceFlags.ContainerInherit,
                PropagationFlags.NoPropagateInherit,
                AccessControlType.Allow));

            dInfo.SetAccessControl(dSecurity);




            base.Install(stateSaver);
        }

        public override void Uninstall(System.Collections.IDictionary stateSaver)
        {
            base.Uninstall(stateSaver);

            List<string> fakeArg = new List<string>();
            fakeArg.Add("--unregister");
            var args = fakeArg.ToArray();

            Settings settings = new Settings(args);

        }
    }
}
