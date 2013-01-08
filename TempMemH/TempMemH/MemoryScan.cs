using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace MemoryScannerDLL
{
    class MemoryScan
    {
        [DllImport(@"C:\Users\Grant\Documents\Visual Studio 2012\Projects\TempMemH\Debug\MemCDll.dll")]
        public static extern void Run_scan();
        
        [DllImport(@"C:\Users\Grant\Documents\Visual Studio 2012\Projects\TempMemH\Debug\MemCDll.dll")]
        public static extern bool ConditionalScan(UInt64 pid, int data_size, UInt32 start_val);
        
        [DllImport(@"C:\Users\Grant\Documents\Visual Studio 2012\Projects\TempMemH\Debug\MemCDll.dll")]
        public static extern bool UnconditionalScan(UInt64 pid, int data_size, UInt32 start_val);
        
        [DllImport(@"C:\Users\Grant\Documents\Visual Studio 2012\Projects\TempMemH\Debug\MemCDll.dll")]
        public static extern void PrintMatches();
        
        [DllImport(@"C:\Users\Grant\Documents\Visual Studio 2012\Projects\TempMemH\Debug\MemCDll.dll")]
        public static extern void Increased();
        
        [DllImport(@"C:\Users\Grant\Documents\Visual Studio 2012\Projects\TempMemH\Debug\MemCDll.dll")]
        public static extern void Decreased();
        
        [DllImport(@"C:\Users\Grant\Documents\Visual Studio 2012\Projects\TempMemH\Debug\MemCDll.dll")]
        public static extern void ConditionEquals(UInt32 val);
        
        [DllImport(@"C:\Users\Grant\Documents\Visual Studio 2012\Projects\TempMemH\Debug\MemCDll.dll")]
        public static extern int MatchCount();
        
        [DllImport(@"C:\Users\Grant\Documents\Visual Studio 2012\Projects\TempMemH\Debug\MemCDll.dll")]
        public static extern bool WriteAddress(UInt32 addr, UInt32 val);
        
        [DllImport(@"C:\Users\Grant\Documents\Visual Studio 2012\Projects\TempMemH\Debug\MemCDll.dll")]
        public static extern UInt32 ReadAddress(UInt32 addr, UInt32 val);
        
        [DllImport(@"C:\Users\Grant\Documents\Visual Studio 2012\Projects\TempMemH\Debug\MemCDll.dll")]
        public static extern void Free_Scan();
        
        [DllImport(@"C:\Users\Grant\Documents\Visual Studio 2012\Projects\TempMemH\Debug\MemCDll.dll")]
        public static extern void IncreasedBy(UInt32 val);
        
        [DllImport(@"C:\Users\Grant\Documents\Visual Studio 2012\Projects\TempMemH\Debug\MemCDll.dll")]
        public static extern void DecreasedBy(UInt32 val);
        
        [DllImport(@"C:\Users\Grant\Documents\Visual Studio 2012\Projects\TempMemH\Debug\MemCDll.dll")]
        public static extern void GreaterThan(UInt32 val);
        
        [DllImport(@"C:\Users\Grant\Documents\Visual Studio 2012\Projects\TempMemH\Debug\MemCDll.dll")]
        public static extern void LessThan(UInt32 val);
        
        [DllImport(@"C:\Users\Grant\Documents\Visual Studio 2012\Projects\TempMemH\Debug\MemCDll.dll")]
        public static extern void Changed();
        
        [DllImport(@"C:\Users\Grant\Documents\Visual Studio 2012\Projects\TempMemH\Debug\MemCDll.dll")]
        public static extern void Remained();
        
        [DllImport(@"C:\Users\Grant\Documents\Visual Studio 2012\Projects\TempMemH\Debug\MemCDll.dll")]
        public static extern IntPtr MatchResult();

        [DllImport(@"C:\Users\Grant\Documents\Visual Studio 2012\Projects\TempMemH\Debug\MemCDll.dll")]
        public static extern void Free_Returned_Matches();
    }
}
