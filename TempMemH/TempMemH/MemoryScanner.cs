using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
namespace MemoryScannerDLL
{
    class MemoryScannerWrapper
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        struct MEMINFO 
        {
           public UInt32 addr;
           public UInt32 val;
        }

        static void Main(string[] args)
        {
            List<MEMINFO> Matches = new List<MEMINFO>();
            MemoryScan.ConditionalScan(7052, 4, 1011);
            int MC = MemoryScan.MatchCount();
            Console.WriteLine("Match Count: {0}", MC);
            
            
            IntPtr MatchRes = MemoryScan.MatchResult();

            for (int I = 0; I < MC; I++)
            {
                int MISS = Marshal.SizeOf(typeof(MEMINFO));
                MatchRes = IntPtr.Add(MatchRes, MISS);
                Matches.Add((MEMINFO)Marshal.PtrToStructure(MatchRes, typeof(MEMINFO)));
                string hexValue = Matches[I].addr.ToString("X");
                Console.WriteLine(hexValue + " " + Matches[I].val);
            }
            Matches.ForEach(P => MemoryScan.WriteAddress(P.addr, P.val+1));
                
       
            MemoryScan.PrintMatches();
            Console.Read();
            
        }
    }
}
