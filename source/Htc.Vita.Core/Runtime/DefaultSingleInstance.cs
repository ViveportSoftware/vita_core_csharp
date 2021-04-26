using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;
using Htc.Vita.Core.Crypto;
using Htc.Vita.Core.Diagnostics;
using Htc.Vita.Core.Json;
using Htc.Vita.Core.Log;
using Htc.Vita.Core.Util;

namespace Htc.Vita.Core.Runtime
{
    /// <summary>
    /// Class DefaultSingleInstance.
    /// Implements the <see cref="SingleInstance" />
    /// </summary>
    /// <seealso cref="SingleInstance" />
    public partial class DefaultSingleInstance : SingleInstance
    {
        private const string MessageResponseInsufficientPermission = "insufficient_permission";
        private const string MessageResponseOk = "ok";
        private const string MessageResponseUnknown = "unknown";

        private MessageVerificationPolicy _messageVerificationPolicy = MessageVerificationPolicy.None;
        private Mutex _mutex;

        /// <summary>
        /// Gets or sets the ipc channel client.
        /// </summary>
        /// <value>The ipc channel client.</value>
        protected IpcChannel.Client IpcChannelClient { get; set; }

        /// <summary>
        /// Gets or sets the ipc channel provider.
        /// </summary>
        /// <value>The ipc channel provider.</value>
        protected IpcChannel.Provider IpcChannelProvider { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultSingleInstance"/> class.
        /// </summary>
        public DefaultSingleInstance()
        {
            IpcChannelClient = IpcChannel.Client.GetInstance<DefaultNamedPipeIpcChannel.Client>();
            IpcChannelProvider = IpcChannel.Provider.GetInstance<DefaultNamedPipeIpcChannel.Provider>();
        }

        private void OnHandleReceivedMessage(
                IpcChannel ipcChannel,
                FilePropertiesInfo filePropertiesInfo)
        {
            if (ipcChannel == null)
            {
                return;
            }

            var currentAssembly = Assembly.GetEntryAssembly() ?? Assembly.GetCallingAssembly();
            var currentAssemblyLocation = currentAssembly.Location;
            var senderAssemblyLocation = filePropertiesInfo.Instance.FullName;

            if (_messageVerificationPolicy.IsFlagApplied(MessageVerificationPolicy.SameLocation))
            {
                if (string.IsNullOrWhiteSpace(currentAssemblyLocation))
                {
                    Logger.GetInstance(typeof(DefaultSingleInstance)).Error("Can not find current assembly location");
                    ipcChannel.Input = MessageResponseUnknown;
                    return;
                }

                if (string.IsNullOrWhiteSpace(senderAssemblyLocation))
                {
                    Logger.GetInstance(typeof(DefaultSingleInstance)).Error("Can not find sender assembly location");
                    ipcChannel.Input = MessageResponseUnknown;
                    return;
                }

                if (!currentAssemblyLocation.Equals(senderAssemblyLocation))
                {
                    Logger.GetInstance(typeof(DefaultSingleInstance)).Warn($"Current assembly location [{currentAssemblyLocation}] is different from sender assembly location [{senderAssemblyLocation}]");
                    ipcChannel.Input = MessageResponseInsufficientPermission;
                    return;
                }
            }

            if (_messageVerificationPolicy.IsFlagApplied(MessageVerificationPolicy.SameBinary))
            {
                if (string.IsNullOrWhiteSpace(currentAssemblyLocation))
                {
                    Logger.GetInstance(typeof(DefaultSingleInstance)).Error("Can not find current assembly location");
                    ipcChannel.Input = MessageResponseUnknown;
                    return;
                }

                if (string.IsNullOrWhiteSpace(senderAssemblyLocation))
                {
                    Logger.GetInstance(typeof(DefaultSingleInstance)).Error("Can not find sender assembly location");
                    ipcChannel.Input = MessageResponseUnknown;
                    return;
                }

                var currentAssemblyChecksum = Sha1.GetInstance().GenerateInBase64(new FileInfo(currentAssemblyLocation));
                var senderAssemblyChecksum = Sha1.GetInstance().GenerateInBase64(new FileInfo(senderAssemblyLocation));

                if (!currentAssemblyChecksum.Equals(senderAssemblyChecksum))
                {
                    Logger.GetInstance(typeof(DefaultSingleInstance)).Warn($"Current assembly checksum [{currentAssemblyChecksum}] is different from sender assembly checksum [{senderAssemblyChecksum}]");
                    ipcChannel.Input = MessageResponseInsufficientPermission;
                    return;
                }
            }

            var message = ipcChannel.Output ?? string.Empty;
            var options = ParseOptionsFrom(message);
            if (options != null)
            {
                NotifyOptionsMessageReceived(options);
            }
            else
            {
                NotifyUnparsedStringMessageReceived(message);
            }

            ipcChannel.Input = MessageResponseOk;
        }

        /// <inheritdoc />
        protected override bool OnHasHeldMutex(
                bool visibleInGlobal,
                int timeoutInMilli)
        {
            if (_mutex != null)
            {
                return true;
            }

            var fullName = Name;
            if (Platform.IsWindows)
            {
                var prefix = "Local";
                if (visibleInGlobal)
                {
                    prefix = "Global";
                }
                fullName = $"{prefix}\\{Name}";
            }

            Mutex mutex = null;
            try
            {
                bool isHeldByThis;
                mutex = new Mutex(true, fullName, out isHeldByThis);
                if (!isHeldByThis)
                {
                    if (timeoutInMilli < -1 || timeoutInMilli == 0 || !mutex.WaitOne(timeoutInMilli))
                    {
                        mutex.Dispose();
                        mutex = null;
                    }
                }

                if (mutex != null)
                {
                    if (Platform.IsWindows)
                    {
                        var mutexSecurity = new MutexSecurity();
                        mutexSecurity.AddAccessRule(new MutexAccessRule(
                                new SecurityIdentifier(WellKnownSidType.AuthenticatedUserSid, null),
                                MutexRights.Synchronize | MutexRights.Modify,
                                AccessControlType.Allow
                        ));
                        mutex.SetAccessControl(mutexSecurity);
                    }
                    _mutex = mutex;
                    return true;
                }
            }
            catch (Exception)
            {
                mutex?.Dispose();
            }

            return false;
        }

        /// <inheritdoc />
        protected override bool OnIsListeningMessage()
        {
            return IpcChannelProvider?.IsRunning() ?? false;
        }

        /// <inheritdoc />
        protected override bool OnIsReadySendingMessage()
        {
            if (IpcChannelClient == null)
            {
                return false;
            }
            if (!IpcChannelClient.SetName(Name))
            {
                return false;
            }
            return IpcChannelClient.IsReady();
        }

        /// <inheritdoc />
        protected override SendMessageResult OnSendOptionsMessage(Dictionary<string, string> options)
        {
            if (!OnIsReadySendingMessage())
            {
                return new SendMessageResult
                {
                        Status = SendMessageStatus.ConnectionNotReady
                };
            }

            var request = options.ToJsonObject().ToString();
            var response = IpcChannelClient?.Request(request);
            return new SendMessageResult
            {
                    Status = Util.Convert.ToTypeByDescription<SendMessageStatus>(response)
            };
        }

        /// <inheritdoc />
        protected override SendMessageResult OnSendRawStringMessage(string rawString)
        {
            if (!OnIsReadySendingMessage())
            {
                return new SendMessageResult
                {
                        Status = SendMessageStatus.ConnectionNotReady
                };
            }

            var response = IpcChannelClient?.Request(rawString);
            return new SendMessageResult
            {
                    Status = Util.Convert.ToTypeByDescription<SendMessageStatus>(response)
            };
        }

        /// <inheritdoc />
        protected override SingleInstance OnSetMessageVerificationPolicy(MessageVerificationPolicy messageVerificationPolicy)
        {
            _messageVerificationPolicy = messageVerificationPolicy;
            return this;
        }

        /// <inheritdoc />
        protected override bool OnStartMessageListening()
        {
            if (IpcChannelProvider == null)
            {
                return false;
            }

            IpcChannelProvider.OnMessageHandled = OnHandleReceivedMessage;
            if (!IpcChannelProvider.SetName(Name))
            {
                return false;
            }
            return IpcChannelProvider.Start();
        }

        /// <inheritdoc />
        protected override bool OnStopMessageListening()
        {
            return IpcChannelProvider?.Stop() ?? false;
        }

        private static Dictionary<string, string> ParseOptionsFrom(string data)
        {
            if (string.IsNullOrWhiteSpace(data))
            {
                return null;
            }

            if (!data.StartsWith("{") || !data.EndsWith("}"))
            {
                return null;
            }

            var jsonObject = JsonFactory.GetInstance().GetJsonObject(data);
            if (jsonObject == null)
            {
                return null;
            }

            var result = new Dictionary<string, string>();
            foreach (var key in jsonObject.AllKeys())
            {
                if (string.IsNullOrWhiteSpace(key))
                {
                    continue;
                }

                var value = jsonObject.ParseString(key);
                if (value != null)
                {
                    result[key] = value;
                }
            }

            return result;
        }
    }
}
