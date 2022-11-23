using Microsoft.VisualBasic.Devices;
using NzbgetControl.Misc;
using RamDisk;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Usenet.Nzb;

namespace NzbgetControl
{
    public class RamDiskManager : IDisposable
    {
        public char RamDriveLetter { get; private set; }

        public RamDiskManager(int size)
        {
            Mount(size);
        }

        public static bool isDriverInstalled()
        {
            if (Util.checkInstalled("ImDisk Virtual Disk Driver") == null)
            {
                Console.WriteLine("ImDisk Virtual Disk Driver not installed, can't use ramdisk");
                return false;
            }
            return true;
        }

        public static bool isAdminRight()
        {
            return new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
        }

        public static int getFreeRamMb()
        {
            ulong freeMemory = new ComputerInfo().AvailablePhysicalMemory;
            int freeMemoryInMb = (int)(freeMemory / 1024 / 1024);
            return freeMemoryInMb;
        }



        public void Mount(int size)
        {
            var listFreeDriveLetters = Enumerable.Range('A', 'Z' - 'A' + 1).Select(i => (Char)i + ":").Except(DriveInfo.GetDrives().Select(s => s.Name.Replace("\\", ""))).ToList();
            RamDriveLetter = listFreeDriveLetters.Last()[0];
            RamDrive.Mount(size, FileSystem.NTFS, RamDriveLetter);
        }

        public void UnMount()
        {
            RamDrive.Unmount(RamDriveLetter);
        }


        protected virtual void Dispose(Boolean disposing)
        {
            if (disposing)
            {
                if (RamDriveLetter != '\0' && Directory.Exists(RamDriveLetter + ":\\") )
                {
                    RamDrive.Unmount(RamDriveLetter);
                    RamDriveLetter = '\0';
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
