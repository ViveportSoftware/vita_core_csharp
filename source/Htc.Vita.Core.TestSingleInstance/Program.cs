using System;
using System.Collections.Generic;
using Htc.Vita.Core.Log;
using Htc.Vita.Core.Runtime;

namespace Htc.Vita.Core.TestSingleInstance
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var singleInstance = SingleInstance.GetInstance().SetName("123");

            var hasHeldMutex = singleInstance.HasHeldMutex();
            Logger.GetInstance(typeof(Program)).Info($"hasHeldMutex: {hasHeldMutex}");

            if (hasHeldMutex)
            {
                singleInstance.SetMessageVerificationPolicy(SingleInstance.MessageVerificationPolicy.SameBinary);
                singleInstance.OnOptionsMessageReceived += SingleInstance_OnOptionsMessageReceived;
                singleInstance.OnUnparsedStringMessageReceived += SingleInstance_OnUnparsedStringMessageReceived;
                singleInstance.StartMessageListening();
            }
            else
            {
                var isReadySendingMessage = singleInstance.IsReadySendingMessage();
                if (!isReadySendingMessage)
                {
                    Logger.GetInstance(typeof(Program)).Warn("This instance is not ready sending message.");
                }
                else
                {
                    singleInstance.SendRawStringMessage("test raw string");
                    singleInstance.SendOptionsMessage(new Dictionary<string, string>
                    {
                            { "key1", "value1" },
                            { "key2", "value2" }
                    });
                }
            }

            Console.ReadKey();
            if (singleInstance.IsListeningMessage())
            {
                Logger.GetInstance(typeof(Program)).Warn("Try to stop message listening.");
                singleInstance.StopMessageListening();
            }
        }

        private static void SingleInstance_OnOptionsMessageReceived(Dictionary<string, string> options)
        {
            foreach (var key in options.Keys)
            {
                Logger.GetInstance(typeof(Program)).Info($"options[{key}]: {options[key]}");
            }
        }

        private static void SingleInstance_OnUnparsedStringMessageReceived(string rawString)
        {
            Logger.GetInstance(typeof(Program)).Info($"raw string: {rawString}");
        }
    }
}
