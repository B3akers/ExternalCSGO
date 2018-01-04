using SimpleExternalCheatCSGO.API;
using SimpleExternalCheatCSGO.CodeInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace SimpleExternalCheatCSGO
{
    public struct GameModule
    {
        public IntPtr BaseAddress;
        public int Size;
        public GameModule(IntPtr baseaddress, int size)
        {
            this.BaseAddress = baseaddress;
            this.Size = size;
        }
    }

    public class CSGOProcess
    {
        public IntPtr ProcessHandle = IntPtr.Zero;

        public Process GameProcess = null;

        public Dictionary<string, GameModule> CSGOModules = new Dictionary<string, GameModule>();

        public void DumpInfo()
        {
            Console.WriteLine("--------");

            Console.WriteLine("Process PID - " + Globals._csgo.GameProcess.Id);
            Console.WriteLine("Handle - 0x" + Globals._csgo.ProcessHandle.ToString("X"));

            Console.WriteLine("--------");

            foreach (var mod in CSGOModules)
            {
                Console.WriteLine("");
                Console.WriteLine(" " + mod.Key + ".dll");
                Console.WriteLine("- BaseAddress - 0x" + mod.Value.BaseAddress.ToString("X"));
                Console.WriteLine("- Size        - 0x" + mod.Value.Size.ToString("X"));
            }

            Console.WriteLine("");
            Console.WriteLine("");
        }

        public bool CheckHandle()
        {
            bool bFail = false;
            try
            {
                GameProcess = Process.GetProcessById(GameProcess.Id);
            }
            catch { bFail = true; }

            if (GameProcess == null || ProcessHandle == IntPtr.Zero || bFail)
            {
                if (MainThread.TriggerThread != null && MainThread.TriggerThread.IsAlive)
                    MainThread.TriggerThread.Abort();

                Environment.Exit(0);
                Console.WriteLine("Invalid handle!");
                Clear();
                return false;
            }
            return true;
        }

        public void Clear()
        {
            Globals._alloc.Free();
            Globals._alloc.AllocatedSize.Clear();

            SendClantag.Address = IntPtr.Zero;
            LineGoesThroughSmoke.Address = IntPtr.Zero;
            RevealRank.Address = IntPtr.Zero;
            ClientCmd.Address = IntPtr.Zero;

            GameProcess = null;
            CSGOModules.Clear();
            WinAPI.CloseHandle(ProcessHandle);
        }

        public bool LoadCSGO()
        {
            try
            {
                var _loaded = false;
                while (!_loaded)
                {
                    var _processes = Process.GetProcessesByName("csgo");

                    if (_processes.Length <= 0)
                    {
                        Thread.Sleep(50);
                        continue;
                    }

                    var _process = _processes.FirstOrDefault();

                    if (_processes.Length > 1)
                    {
                        Console.WriteLine("--------");
                        for (var i = 1; i < +_processes.Length; ++i)
                        {
                            var _proc = _processes[i - 1];
                            Console.WriteLine(i + ". csgo.exe - " + _proc.Id);
                        }
                        Console.WriteLine("--------");

                        Console.Write("Select index: ");
                        int iIndex = 0;
                        try
                        {
                            iIndex = Convert.ToInt32(Console.ReadLine()) - 1;
                        }
                        catch { }

                        _process = _processes[iIndex];
                    }

                    int _ProcessID = _process.Id;
                    bool _modulesLoaded = false;
                    Console.WriteLine("Getting modules...");
                    while (!_modulesLoaded)
                    {
                        CSGOModules.Clear();
                        _process = Process.GetProcessById(_ProcessID);

                        bool _clientLoaded = false;
                        bool _engineLoaded = false;
                        bool _vstdlibLoaded = false;
                        bool _inputsystemLoaded = false;
                        bool _steam_apiLoaded = false;

                        foreach (ProcessModule _module in _process.Modules)
                        {
                            if (!_clientLoaded && _module.ModuleName == "client.dll" && _module.BaseAddress != IntPtr.Zero)
                            {
                                GameModule _clientmodule = new GameModule(_module.BaseAddress, _module.ModuleMemorySize);
                                CSGOModules.Add("client", _clientmodule);
                                _clientLoaded = true;
                            }
                            if (!_engineLoaded && _module.ModuleName == "engine.dll" && _module.BaseAddress != IntPtr.Zero)
                            {
                                GameModule _enginemodule = new GameModule(_module.BaseAddress, _module.ModuleMemorySize);
                                CSGOModules.Add("engine", _enginemodule);
                                _engineLoaded = true;
                            }
                            if (!_vstdlibLoaded && _module.ModuleName == "vstdlib.dll" && _module.BaseAddress != IntPtr.Zero)
                            {
                                GameModule _vstdlibmodule = new GameModule(_module.BaseAddress, _module.ModuleMemorySize);
                                CSGOModules.Add("vstdlib", _vstdlibmodule);
                                _vstdlibLoaded = true;
                            }
                            if (!_inputsystemLoaded && _module.ModuleName == "inputsystem.dll" && _module.BaseAddress != IntPtr.Zero)
                            {
                                GameModule _inputsystemmodule = new GameModule(_module.BaseAddress, _module.ModuleMemorySize);
                                CSGOModules.Add("inputsystem", _inputsystemmodule);
                                _inputsystemLoaded = true;
                            }
                            if (!_steam_apiLoaded && _module.ModuleName == "steam_api.dll" && _module.BaseAddress != IntPtr.Zero)
                            {
                                GameModule _steam_api = new GameModule(_module.BaseAddress, _module.ModuleMemorySize);
                                CSGOModules.Add("steam_api", _steam_api);
                                _steam_apiLoaded = true;
                            }
                            if (_engineLoaded && _clientLoaded && _vstdlibLoaded && _inputsystemLoaded && _steam_apiLoaded)
                            {
                                _modulesLoaded = true;
                                break;
                            }
                        }
                        Thread.Sleep(50);
                    }
                    GameProcess = _process;

                    ProcessHandle = WinAPI.OpenProcess(0x1F0FFF, false, GameProcess.Id);

                    _loaded = true;
                }
                return true;
            }
            catch { return false; }
        }
    }
}
