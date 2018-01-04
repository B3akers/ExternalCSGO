using SimpleExternalCheatCSGO.API;
using SimpleExternalCheatCSGO.SDK;
using SimpleExternalCheatCSGO.Structs;
using SimpleExternalCheatCSGO.CodeInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace SimpleExternalCheatCSGO
{
    class Program
    {
        static void Main()
        {
            StartCheat();
        }

        public static void StartCheat()
        {
            Stopwatch netvar_Watch = new Stopwatch();

            Stopwatch cheat_Watch = new Stopwatch();

            Stopwatch signatures_Watch = new Stopwatch();

            cheat_Watch.Start();

            Console.WriteLine("[Version 0.1] - Build Time " + new FileInfo(Application.ExecutablePath).LastWriteTime.ToString("yyyy-MM-dd - HH:mm:ss"));

            Console.WriteLine("Loading csgo process...");

            if (!Globals._csgo.LoadCSGO())
            {
                Console.WriteLine("Failed to load csgo process!");
                Environment.Exit(0);
            }

            Globals._csgo.DumpInfo();

            Console.WriteLine("Scanning netvars...");
            
            netvar_Watch.Start();

            Globals._netvar.Init();

            netvar_Watch.Stop();

            Console.WriteLine("NetVar scan completed in " + netvar_Watch.Elapsed.ToString("ss\\.ff") + "s");

            Console.WriteLine("Scanning patterns...");

            signatures_Watch.Start();

            Globals._signatures.Init();

            signatures_Watch.Stop();

            Console.WriteLine("Signatures scan completed in " + signatures_Watch.Elapsed.ToString("ss\\.ff") + "s");

            cheat_Watch.Stop();

            Console.WriteLine("Cheat loaded in   " + cheat_Watch.Elapsed.ToString("ss\\.ff") + "s");

            MainThread.Start();
        }
    }
}
