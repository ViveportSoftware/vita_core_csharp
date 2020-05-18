using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading;
using Htc.Vita.Core.Crypto;
using Htc.Vita.Core.Diagnostics;
using Htc.Vita.Core.Interop;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Runtime
{
    public class NamedPipeIpcChannel
    {
        public class Client : IpcChannel.Client
        {
            private const int PipeBufferSize = 512;

            private string _pipeName;

            public Client()
            {
                _pipeName = Sha1.GetInstance().GenerateInHex("");
            }

            protected override bool OnIsReady(Dictionary<string, string> options)
            {
                var shouldVerifyProvider = false;
                if (options.ContainsKey(OptionVerifyProvider))
                {
                    shouldVerifyProvider = Util.Convert.ToBool(options[OptionVerifyProvider]);
                }
                using (var clientStream = new NamedPipeClientStream(_pipeName))
                {
                    try
                    {
                        clientStream.Connect(100);
                        clientStream.ReadMode = PipeTransmissionMode.Message;
                        var outputBuilder = new StringBuilder();
                        var outputBuffer = new byte[PipeBufferSize];
                        var verified = !shouldVerifyProvider || VerifyServerSignature(clientStream);
                        clientStream.Write(outputBuffer, 0, outputBuffer.Length);
                        do
                        {
                            clientStream.Read(outputBuffer, 0, outputBuffer.Length);
                            var outputChunk = Encoding.UTF8.GetString(outputBuffer);
                            outputBuilder.Append(outputChunk);
                            outputBuffer = new byte[outputBuffer.Length];
                        } while (!clientStream.IsMessageComplete);
                        return verified;
                    }
                    catch (TimeoutException)
                    {
                        // Ignore
                    }
                    catch (IOException)
                    {
                        // Ignore
                    }
                }
                return false;
            }

            protected override string OnRequest(string input)
            {
                using (var clientStream = new NamedPipeClientStream(_pipeName))
                {
                    clientStream.Connect();
                    clientStream.ReadMode = PipeTransmissionMode.Message;
                    var outputBuilder = new StringBuilder();
                    var outputBuffer = new byte[PipeBufferSize];
                    var inputBytes = Encoding.UTF8.GetBytes(input);
                    clientStream.Write(inputBytes, 0, inputBytes.Length);
                    do
                    {
                        clientStream.Read(outputBuffer, 0, outputBuffer.Length);
                        var outputChunk = Encoding.UTF8.GetString(outputBuffer);
                        outputBuilder.Append(outputChunk);
                        outputBuffer = new byte[outputBuffer.Length];
                    }
                    while (!clientStream.IsMessageComplete);
                    return FilterOutInvalidChars(outputBuilder.ToString());
                }
            }

            protected override bool OnSetName(string name)
            {
                if (!string.IsNullOrWhiteSpace(name))
                {
                    _pipeName = Sha1.GetInstance().GenerateInHex(name);
                }
                return true;
            }
        }

        public class Provider : IpcChannel.Provider
        {
            private const int PipeBufferSize = 512;
            private const int PipeThreadNumber = 10;

            private readonly Thread[] _workerThreads = new Thread[PipeThreadNumber];

            private bool _isRunning;
            private bool _shouldStopWorkers;
            private string _pipeName;

            public Provider()
            {
                _pipeName = Sha1.GetInstance().GenerateInHex("");
            }

            protected override bool OnIsRunning()
            {
                return _isRunning;
            }

            protected override bool OnSetName(string name)
            {
                if (!string.IsNullOrWhiteSpace(name))
                {
                    _pipeName = Sha1.GetInstance().GenerateInHex(name);
                }
                return true;
            }

            protected override bool OnStart()
            {
                Logger.GetInstance(typeof(Provider)).Info("Channel name: " + _pipeName);

                if (_isRunning)
                {
                    return true;
                }
                _isRunning = true;

                _shouldStopWorkers = false;
                for (var i = 0; i < _workerThreads.Length; i++)
                {
                    _workerThreads[i] = new Thread(OnHandleRequest)
                    {
                            IsBackground = true
                    };
                    _workerThreads[i].Start();
                }
                return true;
            }

            protected override bool OnStop()
            {
                if (!_isRunning)
                {
                    return true;
                }

                _shouldStopWorkers = true;
                var runningThreadNumber = PipeThreadNumber;
                while (runningThreadNumber > 0)
                {
                    ConnectSelfToBreakConnectionWaiting(_pipeName, runningThreadNumber / 2 + 1);

                    for (var i = 0; i < PipeThreadNumber; i++)
                    {
                        if (_workerThreads[i] == null || !_workerThreads[i].Join(250))
                        {
                            continue;
                        }
                        _workerThreads[i] = null;
                        runningThreadNumber--;
                    }
                }
                _isRunning = false;
                return true;
            }

            private void OnHandleRequest(object data)
            {
                var threadId = Thread.CurrentThread.ManagedThreadId;
                var pipeSecurity = new PipeSecurity();
                pipeSecurity.AddAccessRule(
                        new PipeAccessRule(
                                new SecurityIdentifier(WellKnownSidType.BuiltinUsersSid, null),
                                PipeAccessRights.ReadWrite | PipeAccessRights.CreateNewInstance,
                                AccessControlType.Allow
                        )
                );
                pipeSecurity.AddAccessRule(
                        new PipeAccessRule(
                                new SecurityIdentifier(WellKnownSidType.CreatorOwnerSid, null),
                                PipeAccessRights.FullControl,
                                AccessControlType.Allow
                        )
                );
                pipeSecurity.AddAccessRule(
                        new PipeAccessRule(
                                new SecurityIdentifier(WellKnownSidType.LocalSystemSid, null),
                                PipeAccessRights.FullControl,
                                AccessControlType.Allow
                        )
                );

                try
                {
#if NET45
                    using (var serverStream = new NamedPipeServerStream(
                            _pipeName,
                            PipeDirection.InOut,
                            PipeThreadNumber,
                            PipeTransmissionMode.Message,
                            PipeOptions.None,
                            /* default */ 0,
                            /* default */ 0,
                            pipeSecurity
                    ))
#else
                    using (var serverStream = new NamedPipeServerStream(
                            _pipeName,
                            PipeDirection.InOut,
                            PipeThreadNumber,
                            PipeTransmissionMode.Message,
                            PipeOptions.None
                    ))
#endif
                    {
                        while (!_shouldStopWorkers)
                        {
                            if (serverStream.IsConnected)
                            {
                                serverStream.Disconnect();
                            }
                            serverStream.WaitForConnection();

                            var outputBuilder = new StringBuilder();
                            var outputBuffer = new byte[PipeBufferSize];
                            do
                            {
                                serverStream.Read(outputBuffer, 0, outputBuffer.Length);
                                var outputChunk = Encoding.UTF8.GetString(outputBuffer);
                                outputBuilder.Append(outputChunk);
                                outputBuffer = new byte[outputBuffer.Length];
                            }
                            while (!serverStream.IsMessageComplete);
                            var channel = new IpcChannel
                            {
                                    Output = FilterOutInvalidChars(outputBuilder.ToString())
                            };
                            if (!string.IsNullOrEmpty(channel.Output))
                            {
                                if (OnMessageHandled == null)
                                {
                                    Logger.GetInstance(typeof(Provider)).Error("Can not find OnMessageHandled delegates to handle messages");
                                }
                                else
                                {
                                    OnMessageHandled(channel, GetClientSignature(serverStream));
                                }
                            }
                            if (!string.IsNullOrEmpty(channel.Input))
                            {
                                var inputBytes = Encoding.UTF8.GetBytes(channel.Input);
                                serverStream.Write(inputBytes, 0, inputBytes.Length);
                            }
                        }
                        if (serverStream.IsConnected)
                        {
                            serverStream.Disconnect();
                        }
                    }
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(Provider)).Error("Error happened on thread[" + threadId + "]: " + e.Message);
                }
            }

            private static void ConnectSelfToBreakConnectionWaiting(string pipeName, int times)
            {
                if (string.IsNullOrWhiteSpace(pipeName) || times < 0)
                {
                    return;
                }

                for (var i = 0; i < times; i++)
                {
                    using (var clientStream = new NamedPipeClientStream(pipeName))
                    {
                        try
                        {
                            clientStream.Connect(100);
                        }
                        catch (TimeoutException)
                        {
                            continue;
                        }
                        clientStream.ReadMode = PipeTransmissionMode.Message;
                        var inputBuilder = new StringBuilder();
                        var inputBuffer = new byte[PipeBufferSize];
                        var outputBuffer = new byte[1];
                        clientStream.Write(
                                outputBuffer,
                                0,
                                outputBuffer.Length
                        );
                        do
                        {
                            clientStream.Read(
                                    inputBuffer,
                                    0,
                                    inputBuffer.Length
                            );
                            var inputChunk = Encoding.UTF8.GetString(inputBuffer);
                            inputBuilder.Append(inputChunk);
                            inputBuffer = new byte[inputBuffer.Length];
                        }
                        while (!clientStream.IsMessageComplete);
                        // Logger.GetInstance(typeof(Provider)).Info("Dump return: \"" + FilterOutInvalidChars(inputBuilder.ToString()) + "\"");
                    }
                }
            }
        }

        private static string FilterOutInvalidChars(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }
            return input.Replace("\0", string.Empty);
        }

        private static bool VerifyServerSignature(PipeStream pipeStream)
        {
            if (pipeStream == null)
            {
                return false;
            }
            var processId = 0u;
            if (!Windows.GetNamedPipeServerProcessId(pipeStream.SafePipeHandle, ref processId))
            {
                Logger.GetInstance(typeof(NamedPipeIpcChannel)).Error("Can not get named pipe server process id, error code: " + Marshal.GetLastWin32Error());
                return false;
            }
            string processPath = null;
            try
            {
                using (var process = Process.GetProcessById((int) processId))
                {
                    processPath = process.MainModule?.FileName;
                }
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(NamedPipeIpcChannel)).Error("Can not get named pipe server process path: " + e.Message);
            }
            if (processPath == null)
            {
                return false;
            }
            var filePropertiesInfo = FilePropertiesInfo.GetPropertiesInfo(new FileInfo(processPath));
            return filePropertiesInfo != null && filePropertiesInfo.Verified;
        }

        private static FilePropertiesInfo GetClientSignature(PipeStream pipeStream)
        {
            if (pipeStream == null)
            {
                return null;
            }
            var processId = 0u;
            if (!Windows.GetNamedPipeClientProcessId(pipeStream.SafePipeHandle, ref processId))
            {
                Logger.GetInstance(typeof(NamedPipeIpcChannel)).Error("Can not get named pipe client process id, error code: " + Marshal.GetLastWin32Error());
                return null;
            }

            var processPath = ProcessManager.GetProcessPathById((int) processId);
            if (string.IsNullOrWhiteSpace(processPath))
            {
                return null;
            }

            return FilePropertiesInfo.GetPropertiesInfo(new FileInfo(processPath));
        }
    }
}
