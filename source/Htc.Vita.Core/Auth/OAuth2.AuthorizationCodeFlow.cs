using System;
using System.Collections.Generic;
using System.Threading;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Auth
{
    public static partial class OAuth2
    {
        /// <summary>
        /// Class AuthorizationCodeClient.
        /// </summary>
        public abstract partial class AuthorizationCodeClient
        {
            /// <summary>
            /// Authorizes this instance.
            /// </summary>
            /// <returns>AuthorizeResult.</returns>
            public AuthorizeResult Authorize()
            {
                return Authorize(CancellationToken.None);
            }

            /// <summary>
            /// Authorizes this instance.
            /// </summary>
            /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
            /// <returns>AuthorizeResult.</returns>
            public AuthorizeResult Authorize(CancellationToken cancellationToken)
            {
                AuthorizeResult result = null;
                try
                {
                    result = OnAuthorize(cancellationToken);
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(AuthorizationCodeClient)).Error(e.ToString());
                }
                return result ?? new AuthorizeResult();
            }

            /// <summary>
            /// Gets the configuration.
            /// </summary>
            /// <returns>AuthorizationCodeClientConfig.</returns>
            public AuthorizationCodeClientConfig GetConfig()
            {
                AuthorizationCodeClientConfig result = null;
                try
                {
                    result = OnGetConfig();
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(AuthorizationCodeClient)).Error(e.ToString());
                }
                return result;
            }

            /// <summary>
            /// Gets the token.
            /// </summary>
            /// <returns>ClientTokenInfo.</returns>
            public ClientTokenInfo GetToken()
            {
                ClientTokenInfo result = null;
                try
                {
                    result = OnGetToken();
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(AuthorizationCodeClient)).Error(e.ToString());
                }
                return result;
            }

            /// <summary>
            /// Initializes this instance.
            /// </summary>
            /// <param name="config">The configuration.</param>
            /// <returns>AuthorizationCodeClient.</returns>
            public AuthorizationCodeClient Initialize(AuthorizationCodeClientConfig config)
            {
                AuthorizationCodeClient result = null;
                try
                {
                    result = OnInitialize(config);
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(AuthorizationCodeClient)).Error(e.ToString());
                }
                return result ?? this;
            }

            /// <summary>
            /// Introspects the token.
            /// </summary>
            /// <param name="token">The token.</param>
            /// <returns>IntrospectTokenResult.</returns>
            public IntrospectTokenResult IntrospectToken(ClientTokenInfo token)
            {
                return IntrospectToken(
                        token,
                        CancellationToken.None
                );
            }

            /// <summary>
            /// Introspects the token.
            /// </summary>
            /// <param name="token">The token.</param>
            /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
            /// <returns>IntrospectTokenResult.</returns>
            public IntrospectTokenResult IntrospectToken(
                    ClientTokenInfo token,
                    CancellationToken cancellationToken)
            {
                if (token == null || string.IsNullOrWhiteSpace(token.AccessToken))
                {
                    return new IntrospectTokenResult
                    {
                            Status = IntrospectTokenStatus.InvalidToken
                    };
                }

                IntrospectTokenResult result = null;
                try
                {
                    result = OnIntrospectToken(
                            token,
                            cancellationToken
                    );
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(AuthorizationCodeClient)).Error(e.ToString());
                }
                return result ?? new IntrospectTokenResult();
            }


            /// <summary>
            /// Introspects the token.
            /// </summary>
            /// <param name="accessTokenString">The access token string.</param>
            /// <returns>IntrospectTokenResult.</returns>
            public IntrospectTokenResult IntrospectToken(string accessTokenString)
            {
                return IntrospectToken(
                        accessTokenString,
                        CancellationToken.None
                );
            }

            /// <summary>
            /// Introspects the token.
            /// </summary>
            /// <param name="accessTokenString">The access token string.</param>
            /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
            /// <returns>IntrospectTokenResult.</returns>
            public IntrospectTokenResult IntrospectToken(
                    string accessTokenString,
                    CancellationToken cancellationToken)
            {
                return IntrospectToken(
                        new ClientTokenInfo
                        {
                                AccessToken = accessTokenString
                        },
                        cancellationToken
                );
            }

            /// <summary>
            /// Redeems the token.
            /// </summary>
            /// <param name="authorizationCode">The authorization code.</param>
            /// <returns>RedeemTokenResult.</returns>
            public RedeemTokenResult RedeemToken(string authorizationCode)
            {
                return RedeemToken(
                        authorizationCode,
                        CancellationToken.None
                );
            }

            /// <summary>
            /// Redeems the token.
            /// </summary>
            /// <param name="authorizationCode">The authorization code.</param>
            /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
            /// <returns>RedeemTokenResult.</returns>
            public RedeemTokenResult RedeemToken(
                    string authorizationCode,
                    CancellationToken cancellationToken)
            {
                if (string.IsNullOrWhiteSpace(authorizationCode))
                {
                    return new RedeemTokenResult
                    {
                            Status = RedeemTokenStatus.InvalidAuthorizationCode
                    };
                }

                RedeemTokenResult result = null;
                try
                {
                    result = OnRedeemToken(
                            authorizationCode,
                            cancellationToken
                    );
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(AuthorizationCodeClient)).Error(e.ToString());
                }
                return result ?? new RedeemTokenResult();
            }

            /// <summary>
            /// Refreshes the token.
            /// </summary>
            /// <param name="token">The token.</param>
            /// <returns>RefreshTokenResult.</returns>
            public RefreshTokenResult RefreshToken(ClientTokenInfo token)
            {
                return RefreshToken(
                        token,
                        CancellationToken.None
                );
            }

            /// <summary>
            /// Refreshes the token.
            /// </summary>
            /// <param name="token">The token.</param>
            /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
            /// <returns>RefreshTokenResult.</returns>
            public RefreshTokenResult RefreshToken(
                    ClientTokenInfo token,
                    CancellationToken cancellationToken)
            {
                if (token == null || string.IsNullOrWhiteSpace(token.RefreshToken))
                {
                    return new RefreshTokenResult
                    {
                            Status = RefreshTokenStatus.InvalidToken
                    };
                }

                RefreshTokenResult result = null;
                try
                {
                    result = OnRefreshToken(
                            token,
                            cancellationToken
                    );
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(AuthorizationCodeClient)).Error(e.ToString());
                }
                return result ?? new RefreshTokenResult();
            }

            /// <summary>
            /// Refreshes the token.
            /// </summary>
            /// <param name="refreshTokenString">The refresh token string.</param>
            /// <returns>RefreshTokenResult.</returns>
            public RefreshTokenResult RefreshToken(string refreshTokenString)
            {
                return RefreshToken(
                        refreshTokenString,
                        CancellationToken.None
                );
            }

            /// <summary>
            /// Refreshes the token.
            /// </summary>
            /// <param name="refreshTokenString">The refresh token string.</param>
            /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
            /// <returns>RefreshTokenResult.</returns>
            public RefreshTokenResult RefreshToken(
                    string refreshTokenString,
                    CancellationToken cancellationToken)
            {
                return RefreshToken(
                        new ClientTokenInfo
                        {
                                RefreshToken = refreshTokenString
                        },
                        cancellationToken
                );
            }

            /// <summary>
            /// Called when authorizing this instance.
            /// </summary>
            /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
            /// <returns>AuthorizeResult.</returns>
            protected abstract AuthorizeResult OnAuthorize(CancellationToken cancellationToken);
            /// <summary>
            /// Called when getting configuration.
            /// </summary>
            /// <returns>AuthorizationCodeClientConfig.</returns>
            protected abstract AuthorizationCodeClientConfig OnGetConfig();
            /// <summary>
            /// Called when getting token.
            /// </summary>
            /// <returns>ClientTokenInfo.</returns>
            protected abstract ClientTokenInfo OnGetToken();
            /// <summary>
            /// Called when initializing this instance.
            /// </summary>
            /// <param name="config">The configuration.</param>
            /// <returns>AuthorizationCodeClient.</returns>
            protected abstract AuthorizationCodeClient OnInitialize(AuthorizationCodeClientConfig config);
            /// <summary>
            /// Called when introspecting token.
            /// </summary>
            /// <param name="token">The token.</param>
            /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
            /// <returns>IntrospectTokenResult.</returns>
            protected abstract IntrospectTokenResult OnIntrospectToken(
                    ClientTokenInfo token,
                    CancellationToken cancellationToken
            );
            /// <summary>
            /// Called when redeeming token.
            /// </summary>
            /// <param name="authorizationCode">The authorization code.</param>
            /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
            /// <returns>RedeemTokenResult.</returns>
            protected abstract RedeemTokenResult OnRedeemToken(
                    string authorizationCode,
                    CancellationToken cancellationToken
            );
            /// <summary>
            /// Called when refreshing token.
            /// </summary>
            /// <param name="token">The token.</param>
            /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
            /// <returns>RefreshTokenResult.</returns>
            protected abstract RefreshTokenResult OnRefreshToken(
                    ClientTokenInfo token,
                    CancellationToken cancellationToken
            );
        }

        /// <summary>
        /// Class AuthorizationCodeReceiver.
        /// Implements the <see cref="IDisposable" />
        /// </summary>
        /// <seealso cref="IDisposable" />
        public abstract partial class AuthorizationCodeReceiver : IDisposable
        {
            /// <summary>
            /// The option redirect URI
            /// </summary>
            /// <value>The option redirect URI.</value>
            public static string OptionRedirectUri => "redirect_uri";

            /// <summary>
            /// Gets a value indicating whether this <see cref="AuthorizationCodeReceiver" /> is disposed.
            /// </summary>
            /// <value><c>true</c> if disposed; otherwise, <c>false</c>.</value>
            protected bool Disposed { get; private set; }

            /// <summary>
            /// Finalizes an instance of the <see cref="AuthorizationCodeReceiver" /> class
            /// </summary>
            ~AuthorizationCodeReceiver()
            {
                Dispose(false);
            }

            /// <inheritdoc />
            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            /// <summary>
            /// Releases unmanaged and - optionally - managed resources.
            /// </summary>
            /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
            protected virtual void Dispose(bool disposing)
            {
                Disposed = true;
            }

            /// <summary>
            /// Initializes this instance.
            /// </summary>
            /// <param name="options">The options.</param>
            /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
            /// <returns>AuthorizationCodeReceiver.</returns>
            public AuthorizationCodeReceiver Initialize(
                    Dictionary<string, string> options,
                    CancellationToken cancellationToken)
            {
                AuthorizationCodeReceiver result = null;
                try
                {
                    result = OnInitialize(
                            options ?? new Dictionary<string, string>(),
                            cancellationToken
                    );
                }
                catch (ObjectDisposedException)
                {
                    throw;
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(AuthorizationCodeReceiver)).Error(e.ToString());
                }
                return result ?? this;
            }

            /// <summary>
            /// Receives the authorization code.
            /// </summary>
            /// <returns>ReceiveResult.</returns>
            public ReceiveResult Receive()
            {
                ReceiveResult result = null;
                try
                {
                    result = OnReceive();
                }
                catch (ObjectDisposedException)
                {
                    throw;
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(AuthorizationCodeReceiver)).Error(e.ToString());
                }
                return result ?? new ReceiveResult();
            }

            /// <summary>
            /// Called when initializing this instance.
            /// </summary>
            /// <param name="options">The options.</param>
            /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
            /// <returns>AuthorizationCodeReceiver.</returns>
            protected abstract AuthorizationCodeReceiver OnInitialize(
                    Dictionary<string, string> options,
                    CancellationToken cancellationToken
            );
            /// <summary>
            /// Called when receiving the authorization code.
            /// </summary>
            /// <returns>ReceiveResult.</returns>
            protected abstract ReceiveResult OnReceive();
        }

        /// <summary>
        /// Class AuthorizationCodeUserAgent.
        /// Implements the <see cref="IDisposable" />
        /// </summary>
        /// <seealso cref="IDisposable" />
        public abstract partial class AuthorizationCodeUserAgent : IDisposable
        {
            /// <summary>
            /// The option authorization URL
            /// </summary>
            /// <value>The option authorization URL.</value>
            public static string OptionAuthorizationUrl => "authorization_uri";

            /// <summary>
            /// Gets a value indicating whether this <see cref="AuthorizationCodeUserAgent"/> is disposed.
            /// </summary>
            /// <value><c>true</c> if disposed; otherwise, <c>false</c>.</value>
            protected bool Disposed { get; private set; }

            /// <summary>
            /// Finalizes an instance of the <see cref="AuthorizationCodeUserAgent" /> class
            /// </summary>
            ~AuthorizationCodeUserAgent()
            {
                Dispose(false);
            }

            /// <inheritdoc />
            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            /// <summary>
            /// Releases unmanaged and - optionally - managed resources.
            /// </summary>
            /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
            protected virtual void Dispose(bool disposing)
            {
                Disposed = true;
            }

            /// <summary>
            /// Initializes this instance.
            /// </summary>
            /// <param name="options">The options.</param>
            /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
            /// <returns>AuthorizationCodeUserAgent.</returns>
            public AuthorizationCodeUserAgent Initialize(
                    Dictionary<string, string> options,
                    CancellationToken cancellationToken)
            {
                AuthorizationCodeUserAgent result = null;
                try
                {
                    result = OnInitialize(
                            options ?? new Dictionary<string, string>(),
                            cancellationToken
                    );
                }
                catch (ObjectDisposedException)
                {
                    throw;
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(AuthorizationCodeUserAgent)).Error(e.ToString());
                }
                return result ?? this;
            }

            /// <summary>
            /// Launches this instance.
            /// </summary>
            /// <returns>LaunchResult.</returns>
            public LaunchResult Launch()
            {
                LaunchResult result = null;
                try
                {
                    result = OnLaunch();
                }
                catch (ObjectDisposedException)
                {
                    throw;
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(AuthorizationCodeUserAgent)).Error(e.ToString());
                }
                return result ?? new LaunchResult();
            }

            /// <summary>
            /// Called when initializing this instance.
            /// </summary>
            /// <param name="options">The options.</param>
            /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
            /// <returns>AuthorizationCodeUserAgent.</returns>
            protected abstract AuthorizationCodeUserAgent OnInitialize(
                    Dictionary<string, string> options,
                    CancellationToken cancellationToken
            );
            /// <summary>
            /// Called when launching this instance.
            /// </summary>
            /// <returns>LaunchResult.</returns>
            protected abstract LaunchResult OnLaunch();
        }
    }
}
