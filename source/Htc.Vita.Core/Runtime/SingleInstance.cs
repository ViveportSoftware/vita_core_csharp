using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Htc.Vita.Core.Log;
using Htc.Vita.Core.Util;

namespace Htc.Vita.Core.Runtime
{
    /// <summary>
    /// Class SingleInstance.
    /// </summary>
    public abstract partial class SingleInstance
    {
        /// <summary>
        /// Occurs when the options message is received.
        /// </summary>
        public event Action<Dictionary<string, string>> OnOptionsMessageReceived;
        /// <summary>
        /// Occurs when the unparsed string message is received.
        /// </summary>
        public event Action<string> OnUnparsedStringMessageReceived;

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        protected string Name { get; set; } = string.Empty;

        static SingleInstance()
        {
            TypeRegistry.RegisterDefault<SingleInstance, DefaultSingleInstance>();
        }

        /// <summary>
        /// Registers this instance type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void Register<T>()
                where T : SingleInstance, new()
        {
            TypeRegistry.Register<SingleInstance, T>();
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <returns>SingleInstance.</returns>
        public static SingleInstance GetInstance()
        {
            return TypeRegistry.GetInstance<SingleInstance>();
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>SingleInstance.</returns>
        public static SingleInstance GetInstance<T>()
                where T : SingleInstance, new()
        {
            return TypeRegistry.GetInstance<SingleInstance, T>();
        }

        /// <summary>
        /// Sets the name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>SingleInstance.</returns>
        public SingleInstance SetName(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                Name = name;
            }

            return this;
        }

        /// <summary>
        /// Determines whether this instance has held mutex.
        /// </summary>
        /// <returns><c>true</c> if this instance has held mutex; otherwise, <c>false</c>.</returns>
        public bool HasHeldMutex()
        {
            return HasHeldMutex(false);
        }

        /// <summary>
        /// Determines whether this instance has held mutex.
        /// </summary>
        /// <param name="visibleInGlobal">If set to <c>true</c> the mutex is visible in global.</param>
        /// <returns><c>true</c> if this instance has held mutex; otherwise, <c>false</c>.</returns>
        public bool HasHeldMutex(bool visibleInGlobal)
        {
            return HasHeldMutex(
                    visibleInGlobal,
                    0
            );
        }

        /// <summary>
        /// Determines whether this instance has held mutex.
        /// </summary>
        /// <param name="visibleInGlobal">If set to <c>true</c> the mutex is visible in global.</param>
        /// <param name="timeoutInMilli">The timeout in millisecond.</param>
        /// <returns><c>true</c> if this instance has held mutex; otherwise, <c>false</c>.</returns>
        /// <exception cref="InvalidOperationException">The name of {nameof(SingleInstance)} is not set.</exception>
        public bool HasHeldMutex(
                bool visibleInGlobal,
                int timeoutInMilli)
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                throw new InvalidOperationException($"The name of {nameof(SingleInstance)} is not set.");
            }

            var result = false;
            try
            {
                result = OnHasHeldMutex(
                        visibleInGlobal,
                        timeoutInMilli
                );
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(SingleInstance)).Error(e.ToString());
            }
            return result;
        }

        /// <summary>
        /// Determines whether this instance is listening message.
        /// </summary>
        /// <returns><c>true</c> if this instance is listening message; otherwise, <c>false</c>.</returns>
        public bool IsListeningMessage()
        {
            var result = false;
            try
            {
                result = OnIsListeningMessage();
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(SingleInstance)).Error(e.ToString());
            }
            return result;
        }

        /// <summary>
        /// Determines whether this instance is ready sending message.
        /// </summary>
        /// <returns><c>true</c> if this instance is ready sending message; otherwise, <c>false</c>.</returns>
        public bool IsReadySendingMessage()
        {
            var result = false;
            try
            {
                result = OnIsReadySendingMessage();
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(SingleInstance)).Error(e.ToString());
            }
            return result;
        }

        /// <summary>
        /// Notifies the options message received.
        /// </summary>
        /// <param name="options">The options.</param>
        protected void NotifyOptionsMessageReceived(Dictionary<string, string> options)
        {
            if (options == null || options.Count <= 0)
            {
                return;
            }

            Task.Run(() =>
            {
                    try
                    {
                        OnOptionsMessageReceived?.Invoke(options);
                    }
                    catch (Exception e)
                    {
                        Logger.GetInstance(typeof(SingleInstance)).Error(e.ToString());
                    }
            });
        }

        /// <summary>
        /// Notifies the unparsed string message received.
        /// </summary>
        /// <param name="rawString">The raw string.</param>
        protected void NotifyUnparsedStringMessageReceived(string rawString)
        {
            if (string.IsNullOrWhiteSpace(rawString))
            {
                return;
            }

            Task.Run(() =>
            {
                    try
                    {
                        OnUnparsedStringMessageReceived?.Invoke(rawString);
                    }
                    catch (Exception e)
                    {
                        Logger.GetInstance(typeof(SingleInstance)).Error(e.ToString());
                    }
            });
        }

        /// <summary>
        /// Sends the options message.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <returns>SendMessageResult.</returns>
        public SendMessageResult SendOptionsMessage(Dictionary<string, string> options)
        {
            if (options == null || options.Count <= 0)
            {
                return new SendMessageResult
                {
                        Status = SendMessageStatus.InvalidData
                };
            }

            SendMessageResult result = null;
            try
            {
                result = OnSendOptionsMessage(options);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(SingleInstance)).Error(e.ToString());
            }
            return result ?? new SendMessageResult();
        }

        /// <summary>
        /// Sends the raw string message.
        /// </summary>
        /// <param name="rawString">The raw string.</param>
        /// <returns>SendMessageResult.</returns>
        public SendMessageResult SendRawStringMessage(string rawString)
        {
            if (string.IsNullOrWhiteSpace(rawString))
            {
                return new SendMessageResult
                {
                        Status = SendMessageStatus.InvalidData
                };
            }

            SendMessageResult result = null;
            try
            {
                result = OnSendRawStringMessage(rawString);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(SingleInstance)).Error(e.ToString());
            }
            return result ?? new SendMessageResult();
        }

        /// <summary>
        /// Sets the message verification policy.
        /// </summary>
        /// <param name="messageVerificationPolicy">The message verification policy.</param>
        /// <returns>SingleInstance.</returns>
        public SingleInstance SetMessageVerificationPolicy(MessageVerificationPolicy messageVerificationPolicy)
        {
            SingleInstance result = null;
            try
            {
                result = OnSetMessageVerificationPolicy(messageVerificationPolicy);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(SingleInstance)).Error(e.ToString());
            }
            return result ?? this;
        }

        /// <summary>
        /// Starts the message listening.
        /// </summary>
        /// <returns><c>true</c> if starting the message listening successfully, <c>false</c> otherwise.</returns>
        public bool StartMessageListening()
        {
            var result = false;
            try
            {
                result = OnStartMessageListening();
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(SingleInstance)).Error(e.ToString());
            }
            return result;
        }

        /// <summary>
        /// Stops the message listening.
        /// </summary>
        /// <returns><c>true</c> if stopping the message listening successfully, <c>false</c> otherwise.</returns>
        public bool StopMessageListening()
        {
            var result = false;
            try
            {
                result = OnStopMessageListening();
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(SingleInstance)).Error(e.ToString());
            }
            return result;
        }

        /// <summary>
        /// Called when this instance has held mutex.
        /// </summary>
        /// <param name="visibleInGlobal">If set to <c>true</c> the mutex is visible in global.</param>
        /// <param name="timeoutInMilli">The timeout in millisecond.</param>
        /// <returns><c>true</c> if this instance has held mutex, <c>false</c> otherwise.</returns>
        protected abstract bool OnHasHeldMutex(
                bool visibleInGlobal,
                int timeoutInMilli
        );
        /// <summary>
        /// Called when this instance is listening message.
        /// </summary>
        /// <returns><c>true</c> if this instance is listening message, <c>false</c> otherwise.</returns>
        protected abstract bool OnIsListeningMessage();
        /// <summary>
        /// Called when this instance is ready sending message.
        /// </summary>
        /// <returns><c>true</c> if this instance is ready sending message, <c>false</c> otherwise.</returns>
        protected abstract bool OnIsReadySendingMessage();
        /// <summary>
        /// Called when sending options message.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <returns>SendMessageResult.</returns>
        protected abstract SendMessageResult OnSendOptionsMessage(Dictionary<string, string> options);
        /// <summary>
        /// Called when sending raw string message.
        /// </summary>
        /// <param name="rawString">The raw string.</param>
        /// <returns>SendMessageResult.</returns>
        protected abstract SendMessageResult OnSendRawStringMessage(string rawString);
        /// <summary>
        /// Called when setting message verification policy.
        /// </summary>
        /// <param name="messageVerificationPolicy">The message verification policy.</param>
        /// <returns>SingleInstance.</returns>
        protected abstract SingleInstance OnSetMessageVerificationPolicy(MessageVerificationPolicy messageVerificationPolicy);
        /// <summary>
        /// Called when starting message listening.
        /// </summary>
        /// <returns><c>true</c> if starting the message listening successfully, <c>false</c> otherwise.</returns>
        protected abstract bool OnStartMessageListening();
        /// <summary>
        /// Called when stopping message listening.
        /// </summary>
        /// <returns><c>true</c> if stopping the message listening successfully, <c>false</c> otherwise.</returns>
        protected abstract bool OnStopMessageListening();
    }
}
