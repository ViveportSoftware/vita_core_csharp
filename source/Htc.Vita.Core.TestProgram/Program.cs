using System;
using Htc.Vita.Core.Net;
using Htc.Vita.Core.Runtime;

namespace Htc.Vita.Core.TestProgram
{
    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine("NetworkInterface.IsNetworkAvailable(): " + NetworkInterface.IsNetworkAvailable());
            Console.WriteLine("NetworkInterface.IsInternetAvailable(): " + NetworkInterface.IsInternetAvailable());
            Console.ReadKey();

            var processWatcherFactory = ProcessWatcherFactory.GetInstance();
            var notepadProcessWatcher = processWatcherFactory.CreateProcessWatcher("notepad.exe");
            notepadProcessWatcher.ProcessCreated += OnProcessCreated;
            notepadProcessWatcher.ProcessDeleted += OnProcessDeleted;
            notepadProcessWatcher.Start();
            Console.ReadKey();

            notepadProcessWatcher.Stop();
            Console.ReadKey();

            var allProcessWatcher = processWatcherFactory.CreateProcessWatcher();
            allProcessWatcher.ProcessCreated += OnProcessCreated;
            allProcessWatcher.ProcessDeleted += OnProcessDeleted;
            allProcessWatcher.Start();
            Console.ReadKey();

            allProcessWatcher.Stop();
            Console.ReadKey();

            Console.WriteLine("WARNING!! This program will reboot system");
            Console.ReadKey();

            Platform.Exit(Platform.ExitType.Reboot);
            Console.ReadKey();
        }

        private static void OnProcessCreated(ProcessWatcher.ProcessInfo processInfo)
        {
            Console.WriteLine("" + processInfo.Path + " (" + processInfo.Id + ") is created");
        }

        private static void OnProcessDeleted(ProcessWatcher.ProcessInfo processInfo)
        {
            Console.WriteLine("" + processInfo.Path + " (" + processInfo.Id + ") is deleted");
        }
    }
}
