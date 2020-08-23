using System;
using System.Collections.Generic;
using Htc.Vita.Core.Diagnostics;
using Htc.Vita.Core.Log;
using Htc.Vita.Core.Util;

namespace Htc.Vita.Core.Runtime
{
    /// <summary>
    /// Class IpcChannel.
    /// </summary>
    public class IpcChannel
    {
        /// <summary>
        /// Gets or sets the input.
        /// </summary>
        /// <value>The input.</value>
        public string Input { get; set; }
        /// <summary>
        /// Gets or sets the output.
        /// </summary>
        /// <value>The output.</value>
        public string Output { get; set; }

        /// <summary>
        /// Class Client.
        /// </summary>
        public abstract class Client
        {
            /// <summary>
            /// The option to verify provider
            /// </summary>
            public static readonly string OptionVerifyProvider = "option_verify_provider";

            static Client()
            {
                TypeRegistry.RegisterDefault<Client, NamedPipeIpcChannel.Client>();
            }

            /// <summary>
            /// Registers the instance type.
            /// </summary>
            /// <typeparam name="T"></typeparam>
            public static void Register<T>()
                    where T : Client, new()
            {
                TypeRegistry.Register<Client, T>();
            }

            /// <summary>
            /// Gets the instance.
            /// </summary>
            /// <returns>Client.</returns>
            public static Client GetInstance()
            {
                return TypeRegistry.GetInstance<Client>();
            }

            /// <summary>
            /// Gets the instance.
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <returns>Client.</returns>
            public static Client GetInstance<T>()
                    where T : Client, new()
            {
                return TypeRegistry.GetInstance<Client, T>();
            }

            /// <summary>
            /// Determines whether this channel is ready.
            /// </summary>
            /// <returns><c>true</c> if this instance is ready; otherwise, <c>false</c>.</returns>
            public bool IsReady()
            {
                return IsReady(null);
            }

            /// <summary>
            /// Determines whether this channel is ready with the specified options.
            /// </summary>
            /// <param name="options">The options.</param>
            /// <returns><c>true</c> if this channel is ready; otherwise, <c>false</c>.</returns>
            public bool IsReady(Dictionary<string, string> options)
            {
                var opts = options ?? new Dictionary<string, string>();
                var result = false;
                try
                {
                    result = OnIsReady(opts);
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(Client)).Error(e.ToString());
                }
                return result;
            }

            /// <summary>
            /// Requests the specified input.
            /// </summary>
            /// <param name="input">The input.</param>
            /// <returns>System.String.</returns>
            public string Request(string input)
            {
                if (input == null)
                {
                    return null;
                }
                string result = null;
                try
                {
                    result = OnRequest(input);
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(Client)).Error(e.ToString());
                }
                return result;
            }

            /// <summary>
            /// Sets the channel name.
            /// </summary>
            /// <param name="name">The channel name.</param>
            /// <returns><c>true</c> if success, <c>false</c> otherwise.</returns>
            public bool SetName(string name)
            {
                var result = false;
                try
                {
                    result = OnSetName(name);
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(Client)).Error(e.ToString());
                }
                return result;
            }

            /// <summary>
            /// Called when the channel is ready.
            /// </summary>
            /// <param name="options">The options.</param>
            /// <returns><c>true</c> if the channel is ready, <c>false</c> otherwise.</returns>
            protected abstract bool OnIsReady(Dictionary<string, string> options);
            /// <summary>
            /// Called when requesting.
            /// </summary>
            /// <param name="input">The input.</param>
            /// <returns>System.String.</returns>
            protected abstract string OnRequest(string input);
            /// <summary>
            /// Called when setting channel name.
            /// </summary>
            /// <param name="name">The channel name.</param>
            /// <returns><c>true</c> if success, <c>false</c> otherwise.</returns>
            protected abstract bool OnSetName(string name);
        }

        /// <summary>
        /// Class Provider.
        /// </summary>
        public abstract class Provider
        {
            /// <summary>
            /// Gets or sets the message handler.
            /// </summary>
            /// <value>The on message handled.</value>
            public Action<IpcChannel, FilePropertiesInfo> OnMessageHandled { protected get; set; }

            static Provider()
            {
                TypeRegistry.RegisterDefault<Provider, NamedPipeIpcChannel.Provider>();
            }

            /// <summary>
            /// Registers the instance type.
            /// </summary>
            /// <typeparam name="T"></typeparam>
            public static void Register<T>()
                    where T : Provider, new()
            {
                TypeRegistry.Register<Provider, T>();
            }

            /// <summary>
            /// Gets the instance.
            /// </summary>
            /// <returns>Provider.</returns>
            public static Provider GetInstance()
            {
                return TypeRegistry.GetInstance<Provider>();
            }

            /// <summary>
            /// Gets the instance.
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <returns>Provider.</returns>
            public static Provider GetInstance<T>()
                    where T : Provider, new()
            {
                return TypeRegistry.GetInstance<Provider, T>();
            }

            /// <summary>
            /// Determines whether this instance is running.
            /// </summary>
            /// <returns><c>true</c> if this instance is running; otherwise, <c>false</c>.</returns>
            public bool IsRunning()
            {
                var result = false;
                try
                {
                    result = OnIsRunning();
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(Provider)).Error(e.ToString());
                }
                return result;
            }

            /// <summary>
            /// Sets the channel name.
            /// </summary>
            /// <param name="name">The channel name.</param>
            /// <returns><c>true</c> if success, <c>false</c> otherwise.</returns>
            public bool SetName(string name)
            {
                var result = false;
                try
                {
                    result = OnSetName(name);
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(Provider)).Error(e.ToString());
                }
                return result;
            }

            /// <summary>
            /// Starts this instance.
            /// </summary>
            /// <returns><c>true</c> if success, <c>false</c> otherwise.</returns>
            public bool Start()
            {
                var result = false;
                try
                {
                    result = OnStart();
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(Provider)).Error(e.ToString());
                }
                return result;
            }

            /// <summary>
            /// Stops this instance.
            /// </summary>
            /// <returns><c>true</c> if success, <c>false</c> otherwise.</returns>
            public bool Stop()
            {
                var result = false;
                try
                {
                    result = OnStop();
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(Provider)).Error(e.ToString());
                }
                return result;
            }

            /// <summary>
            /// Called when the instance is running.
            /// </summary>
            /// <returns><c>true</c> if the instance is running, <c>false</c> otherwise.</returns>
            protected abstract bool OnIsRunning();
            /// <summary>
            /// Called when setting channel name.
            /// </summary>
            /// <param name="name">The channel name.</param>
            /// <returns><c>true</c> if success, <c>false</c> otherwise.</returns>
            protected abstract bool OnSetName(string name);
            /// <summary>
            /// Called when starting.
            /// </summary>
            /// <returns><c>true</c> if success, <c>false</c> otherwise.</returns>
            protected abstract bool OnStart();
            /// <summary>
            /// Called when stopping.
            /// </summary>
            /// <returns><c>true</c> if success, <c>false</c> otherwise.</returns>
            protected abstract bool OnStop();
        }
    }
}
