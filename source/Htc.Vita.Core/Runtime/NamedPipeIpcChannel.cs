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
    /// <summary>
    /// Class NamedPipeIpcChannel.
    /// </summary>
    public class NamedPipeIpcChannel
    {
#pragma warning disable CA1416
        /// <summary>
        /// Class Client.
        /// Implements the <see cref="IpcChannel.Client" />
        /// </summary>
        /// <seealso cref="IpcChannel.Client" />
        public class Client : IpcChannel.Client
        {
            private const int PipeBufferSize = 512;

            private readonly Dictionary<string, string> _translatedNameMap = new Dictionary<string, string>();

            private string _pipeName;

            /// <summary>
            /// Initializes a new instance of the <see cref="Client" /> class.
            /// </summary>
            public Client()
            {
                _pipeName = "";
            }

            /// <inheritdoc />
            protected override bool OnIsReady(Dictionary<string, string> options)
            {
                var shouldVerifyProvider = false;
                if (options.ContainsKey(OptionVerifyProvider))
                {
                    shouldVerifyProvider = Util.Convert.ToBool(options[OptionVerifyProvider]);
                }
                using (var clientStream = new NamedPipeClientStream(OnOverrideTranslateName(_pipeName)))
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

            /// <summary>
            /// Called when overriding translating name.
            /// </summary>
            /// <param name="name">The name.</param>
            /// <returns>System.String.</returns>
            protected virtual string OnOverrideTranslateName(string name)
            {
                string translatedName = null;
                if (_translatedNameMap.ContainsKey(name))
                {
                    translatedName = _translatedNameMap[name];
                }

                if (!string.IsNullOrWhiteSpace(translatedName))
                {
                    return translatedName;
                }

                translatedName = Sha1.GetInstance().GenerateInHex(name);
                _translatedNameMap[name] = translatedName;

                return translatedName;
            }

            /// <inheritdoc />
            protected override string OnRequest(string input)
            {
                using (var clientStream = new NamedPipeClientStream(OnOverrideTranslateName(_pipeName)))
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

            /// <inheritdoc />
            protected override bool OnSetName(string name)
            {
                if (!string.IsNullOrWhiteSpace(name))
                {
                    _pipeName = name;
                }
                return true;
            }
        }

        /// <summary>
        /// Class Provider.
        /// Implements the <see cref="IpcChannel.Provider" />
        /// </summary>
        /// <seealso cref="IpcChannel.Provider" />
        public class Provider : IpcChannel.Provider
        {
            private const int PipeBufferSize = 512;
            private const int PipeThreadNumber = 16;

            private readonly Dictionary<string, string> _translatedNameMap = new Dictionary<string, string>();

            private readonly Thread[] _workerThreads = new Thread[PipeThreadNumber];

            private bool _isRunning;
            private bool _shouldStopWorkers;
            private string _pipeName;

            /// <summary>
            /// Initializes a new instance of the <see cref="Provider" /> class.
            /// </summary>
            public Provider()
            {
                _pipeName = "";
            }

            private static Windows.PipeModes ConvertPipeModeFrom(PipeTransmissionMode transmissionMode)
            {
                if (transmissionMode == PipeTransmissionMode.Message)
                {
                    return Windows.PipeModes.TypeMessage | Windows.PipeModes.ReadModeMessage;
                }
                return Windows.PipeModes.TypeByte | Windows.PipeModes.ReadModeByte;
            }

            private static Windows.PipeOpenModes ConvertPipeOpenModeFrom(
                    PipeDirection pipeDirection,
                    PipeOptions pipeOptions,
                    int maxNumberOfServerInstances)
            {
                var result = Windows.PipeOpenModes.None;
                if (pipeDirection == PipeDirection.In)
                {
                    result = Windows.PipeOpenModes.AccessInbound;
                }
                else if (pipeDirection == PipeDirection.Out)
                {
                    result = Windows.PipeOpenModes.AccessOutbound;
                }
                else if (pipeDirection == PipeDirection.InOut)
                {
                    result = Windows.PipeOpenModes.AccessDuplex;
                }

                if (pipeOptions == PipeOptions.Asynchronous)
                {
                    result |= Windows.PipeOpenModes.FlagOverlapped;
                }
                else if (pipeOptions == PipeOptions.WriteThrough)
                {
                    result |= Windows.PipeOpenModes.FlagWriteThrough;
                }
                else if (pipeOptions == (PipeOptions)0x20000000 /* PipeOptions.CurrentUserOnly */)
                {
                    result |= Windows.PipeOpenModes.CurrentUserOnly;
                }

                if (maxNumberOfServerInstances == 1)
                {
                    result |= Windows.PipeOpenModes.FlagFirstPipeInstance;
                }

                return result;
            }

            private static NamedPipeServerStream NewNamedPipeServerStreamInstance(
                    string pipeName,
                    PipeDirection direction,
                    int maxNumberOfServerInstances,
                    PipeTransmissionMode transmissionMode,
                    PipeOptions options,
                    int inBufferSize,
                    int outBufferSize,
                    PipeSecurity pipeSecurity)
            {
                if (pipeSecurity == null)
                {
                    return new NamedPipeServerStream(
                            pipeName,
                            direction,
                            maxNumberOfServerInstances,
                            transmissionMode,
                            options,
                            inBufferSize,
                            outBufferSize
                    );
                }

                if (string.IsNullOrWhiteSpace(pipeName))
                {
                    throw new ArgumentNullException(nameof(pipeName));
                }
                if ("anonymous".Equals(pipeName))
                {
                    throw new ArgumentException("pipeName \"anonymous\" is reserved");
                }
                if (inBufferSize < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(inBufferSize), "buffer size should be non-negative");
                }
                if ((maxNumberOfServerInstances < 1 || maxNumberOfServerInstances > 254)
                        && maxNumberOfServerInstances != -1)
                {
                    throw new ArgumentOutOfRangeException(nameof(maxNumberOfServerInstances), "requested server instance should between 1 to 254");
                }

                var fullPath = Path.GetFullPath($"\\\\.\\pipe\\{pipeName}");
                var pipeOpenMode = ConvertPipeOpenModeFrom(
                        direction,
                        options,
                        maxNumberOfServerInstances
                );
                var pipeMode = ConvertPipeModeFrom(transmissionMode);
                if (maxNumberOfServerInstances == -1)
                {
                    maxNumberOfServerInstances = byte.MaxValue;
                }

                var securityDescriptorInBytes = pipeSecurity.GetSecurityDescriptorBinaryForm();
                var securityDescriptorHandle = Marshal.AllocHGlobal(securityDescriptorInBytes.Length);
                Marshal.Copy(
                        securityDescriptorInBytes,
                        0,
                        securityDescriptorHandle,
                        securityDescriptorInBytes.Length
                );
                var securityAttributes = new Windows.SecurityAttributes
                {
                        lpSecurityDescriptor = securityDescriptorHandle
                };
                securityAttributes.nLength = Marshal.SizeOf(securityAttributes);

                try
                {
                    var safePipeHandle = Windows.CreateNamedPipeW(
                            fullPath,
                            pipeOpenMode,
                            pipeMode,
                            (uint) maxNumberOfServerInstances,
                            (uint) outBufferSize,
                            (uint) inBufferSize,
                            0,
                            securityAttributes
                    );
                    if (safePipeHandle.IsInvalid)
                    {
                        return null;
                    }

                    return new NamedPipeServerStream(
                            direction,
                            options.HasFlag(PipeOptions.Asynchronous),
                            false,
                            safePipeHandle
                    )
                    {
                            ReadMode = transmissionMode
                    };
                }
                finally
                {
                    Marshal.FreeHGlobal(securityDescriptorHandle);
                }
            }

            /// <inheritdoc />
            protected override bool OnIsRunning()
            {
                return _isRunning;
            }

            /// <inheritdoc />
            protected override bool OnSetName(string name)
            {
                if (!string.IsNullOrWhiteSpace(name))
                {
                    _pipeName = name;
                }
                return true;
            }

            /// <inheritdoc />
            protected override bool OnStart()
            {
                Logger.GetInstance(typeof(Provider)).Info($"Channel name: {OnOverrideTranslateName(_pipeName)}, thread number: {PipeThreadNumber}");

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

            /// <inheritdoc />
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
                    ConnectSelfToBreakConnectionWaiting(OnOverrideTranslateName(_pipeName), runningThreadNumber / 2 + 1);

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

                while (!_shouldStopWorkers)
                {
                    try
                    {
                        using (var serverStream = NewNamedPipeServerStreamInstance(
                                OnOverrideTranslateName(_pipeName),
                                PipeDirection.InOut,
                                PipeThreadNumber,
                                PipeTransmissionMode.Message,
                                PipeOptions.None,
                                /* default */ 0,
                                /* default */ 0,
                                pipeSecurity
                        ))
                        {
                            if (serverStream == null)
                            {
                                Logger.GetInstance(typeof(Provider)).Error("Can not create named pipe server stream");
                                return;
                            }

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
                        Logger.GetInstance(typeof(Provider)).Error($"Error happened on thread[{threadId}]: {e.Message}");
                    }
                }
            }

            /// <summary>
            /// Called when overriding translating name.
            /// </summary>
            /// <param name="name">The name.</param>
            /// <returns>System.String.</returns>
            protected virtual string OnOverrideTranslateName(string name)
            {
                string translatedName = null;
                if (_translatedNameMap.ContainsKey(name))
                {
                    translatedName = _translatedNameMap[name];
                }

                if (!string.IsNullOrWhiteSpace(translatedName))
                {
                    return translatedName;
                }

                translatedName = Sha1.GetInstance().GenerateInHex(name);
                _translatedNameMap[name] = translatedName;

                return translatedName;
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
                        // Logger.GetInstance(typeof(Provider)).Info($"Dump return: \"{FilterOutInvalidChars(inputBuilder.ToString())}\"");
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
                Logger.GetInstance(typeof(NamedPipeIpcChannel)).Error($"Can not get named pipe server process id, error code: {Marshal.GetLastWin32Error()}");
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
                Logger.GetInstance(typeof(NamedPipeIpcChannel)).Error($"Can not get named pipe server process path: {e.Message}");
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
                Logger.GetInstance(typeof(NamedPipeIpcChannel)).Error($"Can not get named pipe client process id, error code: {Marshal.GetLastWin32Error()}");
                return null;
            }

            var processPath = ProcessManager.GetProcessPathById((int) processId);
            if (string.IsNullOrWhiteSpace(processPath))
            {
                return null;
            }

            return FilePropertiesInfo.GetPropertiesInfo(new FileInfo(processPath));
        }
#pragma warning restore CA1416
    }
}
