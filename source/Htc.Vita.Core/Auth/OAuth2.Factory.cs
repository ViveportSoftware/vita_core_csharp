using System;
using System.Collections.Generic;
using System.Threading;
using Htc.Vita.Core.Log;
using Htc.Vita.Core.Util;

namespace Htc.Vita.Core.Auth
{
    public static partial class OAuth2
    {
        /// <summary>
        /// Class ClientAssistantFactory.
        /// </summary>
        public abstract class ClientAssistantFactory
        {
            static ClientAssistantFactory()
            {
                TypeRegistry.RegisterDefault<ClientAssistantFactory, DummyOAuth2.DummyClientAssistantFactory>();
            }

            /// <summary>
            /// Registers this instance type.
            /// </summary>
            /// <typeparam name="T"></typeparam>
            public static void Register<T>()
                    where T : ClientAssistantFactory, new()
            {
                TypeRegistry.Register<ClientAssistantFactory, T>();
            }

            /// <summary>
            /// Gets the instance.
            /// </summary>
            /// <returns>ClientAssistantFactory.</returns>
            public static ClientAssistantFactory GetInstance()
            {
                return TypeRegistry.GetInstance<ClientAssistantFactory>();
            }

            /// <summary>
            /// Gets the instance.
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <returns>ClientAssistantFactory.</returns>
            public static ClientAssistantFactory GetInstance<T>()
                    where T : ClientAssistantFactory, new()
            {
                return TypeRegistry.GetInstance<ClientAssistantFactory, T>();
            }

            /// <summary>
            /// Gets the authorization code receiver.
            /// </summary>
            /// <returns>AuthorizationCodeReceiver.</returns>
            public AuthorizationCodeReceiver GetAuthorizationCodeReceiver()
            {
                return GetAuthorizationCodeReceiver(
                        null,
                        CancellationToken.None
                );
            }

            /// <summary>
            /// Gets the authorization code receiver.
            /// </summary>
            /// <param name="options">The options.</param>
            /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
            /// <returns>AuthorizationCodeReceiver.</returns>
            public AuthorizationCodeReceiver GetAuthorizationCodeReceiver(
                    Dictionary<string, string> options,
                    CancellationToken cancellationToken)
            {
                AuthorizationCodeReceiver result = null;
                try
                {
                    result = OnGetAuthorizationCodeReceiver(
                            options,
                            cancellationToken
                    );
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(ClientAssistantFactory)).Error(e.ToString());
                }
                return result;
            }

            /// <summary>
            /// Gets the authorization code user agent.
            /// </summary>
            /// <returns>AuthorizationCodeUserAgent.</returns>
            public AuthorizationCodeUserAgent GetAuthorizationCodeUserAgent()
            {
                return GetAuthorizationCodeUserAgent(
                        null,
                        CancellationToken.None
                );
            }

            /// <summary>
            /// Gets the authorization code user agent.
            /// </summary>
            /// <param name="options">The options.</param>
            /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
            /// <returns>AuthorizationCodeUserAgent.</returns>
            public AuthorizationCodeUserAgent GetAuthorizationCodeUserAgent(
                    Dictionary<string, string> options,
                    CancellationToken cancellationToken)
            {
                AuthorizationCodeUserAgent result = null;
                try
                {
                    result = OnGetAuthorizationCodeUserAgent(
                            options,
                            cancellationToken
                    );
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(ClientAssistantFactory)).Error(e.ToString());
                }
                return result;
            }

            /// <summary>
            /// Called when getting authorization code receiver.
            /// </summary>
            /// <param name="options">The options.</param>
            /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
            /// <returns>AuthorizationCodeReceiver.</returns>
            protected abstract AuthorizationCodeReceiver OnGetAuthorizationCodeReceiver(
                    Dictionary<string, string> options,
                    CancellationToken cancellationToken
            );

            /// <summary>
            /// Called when getting authorization code user agent.
            /// </summary>
            /// <param name="options">The options.</param>
            /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
            /// <returns>AuthorizationCodeUserAgent.</returns>
            protected abstract AuthorizationCodeUserAgent OnGetAuthorizationCodeUserAgent(
                    Dictionary<string, string> options,
                    CancellationToken cancellationToken
            );
        }

        /// <summary>
        /// Class ClientFactory.
        /// </summary>
        public abstract class ClientFactory
        {
            static ClientFactory()
            {
                TypeRegistry.RegisterDefault<ClientFactory, DummyOAuth2.DummyClientFactory>();
            }

            /// <summary>
            /// Registers this instance type.
            /// </summary>
            /// <typeparam name="T"></typeparam>
            public static void Register<T>()
                where T : ClientFactory, new()
            {
                TypeRegistry.Register<ClientFactory, T>();
            }

            /// <summary>
            /// Gets the instance.
            /// </summary>
            /// <returns>ClientFactory.</returns>
            public static ClientFactory GetInstance()
            {
                return TypeRegistry.GetInstance<ClientFactory>();
            }

            /// <summary>
            /// Gets the instance.
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <returns>ClientFactory.</returns>
            public static ClientFactory GetInstance<T>()
                where T : ClientFactory, new()
            {
                return TypeRegistry.GetInstance<ClientFactory, T>();
            }

            /// <summary>
            /// Gets the authorization code client.
            /// </summary>
            /// <param name="config">The configuration.</param>
            /// <returns>AuthorizationCodeClient.</returns>
            public AuthorizationCodeClient GetAuthorizationCodeClient(AuthorizationCodeClientConfig config)
            {
                AuthorizationCodeClient result = null;
                try
                {
                    result = OnGetAuthorizationCodeClient(config);
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(ClientFactory)).Error(e.ToString());
                }
                return result;
            }

            /// <summary>
            /// Called when getting authorization code client.
            /// </summary>
            /// <param name="config">The configuration.</param>
            /// <returns>AuthorizationCodeClient.</returns>
            protected abstract AuthorizationCodeClient OnGetAuthorizationCodeClient(AuthorizationCodeClientConfig config);
        }
    }
}
