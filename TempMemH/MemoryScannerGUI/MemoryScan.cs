using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace MemoryScannerGUI
{
    
    class MemoryScan
    {
        const string DllLocation = @"..\..\..\Debug\MemoryDLL.dll";
        [DllImport(DllLocation)]
        public static extern void Run_scan();

        [DllImport(DllLocation)]
        public static extern bool ConditionalScan(UInt64 pid, int data_size, UInt32 start_val);

        [DllImport(DllLocation)]
        public static extern bool UnconditionalScan(UInt64 pid, int data_size);

        [DllImport(DllLocation)]
        public static extern void PrintMatches();

        [DllImport(DllLocation)]
        public static extern void Increased();

        [DllImport(DllLocation)]
        public static extern void Decreased();

        [DllImport(DllLocation)]
        public static extern void ConditionEquals(UInt32 val);

        [DllImport(DllLocation)]
        public static extern int MatchCount();

        [DllImport(DllLocation)]
        public static extern bool WriteAddress(UInt32 addr, UInt32 val);

        [DllImport(DllLocation)]
        public static extern UInt32 ReadAddress(UInt32 addr, UInt32 val);

        [DllImport(DllLocation)]
        public static extern void Free_Scan();

        [DllImport(DllLocation)]
        public static extern void IncreasedBy(UInt32 val);

        [DllImport(DllLocation)]
        public static extern void DecreasedBy(UInt32 val);

        [DllImport(DllLocation)]
        public static extern void GreaterThan(UInt32 val);

        [DllImport(DllLocation)]
        public static extern void LessThan(UInt32 val);

        [DllImport(DllLocation)]
        public static extern void Changed();

        [DllImport(DllLocation)]
        public static extern void Remained();

        [DllImport(DllLocation)]
        public static extern IntPtr MatchResult();

        [DllImport(DllLocation)]
        public static extern void Free_Returned_Matches();
    }
}
