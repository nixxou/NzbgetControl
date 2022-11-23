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
    public class ComputerHelper
    {

        #region Methods

        internal static bool SetIncreasePrivilege(string privilegeName)
        {
            using (WindowsIdentity current = WindowsIdentity.GetCurrent(TokenAccessLevels.Query | TokenAccessLevels.AdjustPrivileges))
            {
                Structs.Windows.TokenPrivileges newState;
                newState.Count = 1;
                newState.Luid = 0L;
                newState.Attr = Constants.Windows.PrivilegeEnabled;

                if (import.LookupPrivilegeValue(null, privilegeName, ref newState.Luid))
                {
                    int result = import.AdjustTokenPrivileges(current.Token, false, ref newState, 0, IntPtr.Zero, IntPtr.Zero) ? 1 : 0;

                    return result != 0;
                }
            }

            return false;
        }

        #endregion

        #region Properties

        internal static bool Is64Bit => Environment.Is64BitOperatingSystem;

        internal static bool IsWindows8OrAbove => Environment.OSVersion.Platform == PlatformID.Win32NT && Environment.OSVersion.Version.Major >= 6.2;

        internal static bool IsWindowsVistaOrAbove => Environment.OSVersion.Platform == PlatformID.Win32NT && Environment.OSVersion.Version.Major >= 6;

        #endregion

    }
}
