using System;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace IsWow64
{


    class Program
    {

        static void Main(string[] args)
        {


            Console.WriteLine("Enter Process ID:");
            int pid = int.Parse(Console.ReadLine());
            var proccess = Process.GetProcessById(pid);
            bool checkArch = is32Bit(proccess);

            if (checkArch == false)
            {
                Console.WriteLine("ProcessName: " + proccess.ProcessName);
                Console.WriteLine("PID: " + pid);
                Console.WriteLine("ArchType ?: 32bit");
                Console.ReadKey();
            }

            Console.WriteLine("ProcessName: " + proccess.ProcessName + "\n");
            Console.WriteLine("PID: " + pid + "\n");
            Console.WriteLine("ArchType ?: 64bit");
            Console.ReadKey();
        }
           


        /**
         * IsWow64Process
         * @param Handle = IntPtr in C#
         * @param PBOOL = out bool in C#
         * 
         */

        [DllImport("Kernel32.dll")]
        static extern bool IsWow64Process(IntPtr hProcess, out bool wow64);

        /**
         * 
         * WORD is 16bit eq in C#  = ushort
         * DWORD is 32bit eq in C# = uint
         * LPVOID is poinr to avoi eq in C# = IntPtr
         */
        struct SystemInfo
        {

            public ushort wProcessorArchitecture;
            public ushort wReserved;
            public uint dwPageSize;
            public IntPtr lpMinimumApplicationAddress;
            public IntPtr lpMaximumApplicationAddress;
            public IntPtr dwActiveProcessorMask;
            public uint dwNumberOfProcessors;
            public uint dwProcessorType;
            public uint dwAllocationGranularity;
            public ushort wProcessorLevel;
            public ushort wProcessorRevision;
        }
        [DllImport("Kernel32.dll")]
        static extern bool GetNativeSystemInfo(out SystemInfo SysInfo);

        
        static bool is32Bit(Process process){
            SystemInfo SysInfo;

            GetNativeSystemInfo(out SysInfo);
            bool IsWow64;
            IsWow64Process(process.Handle, out IsWow64);

            return SysInfo.wProcessorArchitecture == 0 || SysInfo.wProcessorArchitecture == 9 && IsWow64;


        }

    

    }
}
