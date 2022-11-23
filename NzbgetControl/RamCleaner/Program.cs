using System;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Microsoft.Win32;
using System.ComponentModel;
using System.Security.Principal;
using System.Linq;
using System.IO;
using static RamCleaner.Enums.Memory;
using static RamCleaner.Enums.Memory.Area;

namespace RamCleaner
{
    internal class Program
    {
        
        public static void Run()
        {
            foreach (Area x in (Area[])Enum.GetValues(typeof(Area)))
            {
                Cleaner.Clean(x);
            }
        }
        
    }
}
