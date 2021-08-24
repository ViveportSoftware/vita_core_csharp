using System;
using System.Threading;
using System.Threading.Tasks;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Auth
{
    public static partial class OAuth2
    {
        public abstract partial class AuthorizationCodeClient
        {
            /// <summary>
            /// Authorizes this instance.
            /// </summary>
            /// <returns>Task&lt;AuthorizeResult&gt;.</returns>
            public Task<AuthorizeResult> AuthorizeAsync()
            {
                return AuthorizeAsync(CancellationToken.None);
            }

            /// <summary>
            /// Authorizes this instance.
            /// </summary>
            /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
            /// <returns>A Task&lt;AuthorizeResult&gt; representing the asynchronous operation.</returns>
            public async Task<AuthorizeResult> AuthorizeAsync(CancellationToken cancellationToken)
            {
                AuthorizeResult result = null;
                try
                {
                    result = await OnAuthorizeAsync(cancellationToken).ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(AuthorizationCodeClient)).Error(e.ToString());
                }
                return result ?? new AuthorizeResult();
            }

            /// <summary>
            /// Introspects the token.
            /// </summary>
            /// <param name="token">The token.</param>
            /// <returns>Task&lt;IntrospectTokenResult&gt;.</returns>
            public Task<IntrospectTokenResult> IntrospectTokenAsync(ClientTokenInfo token)
            {
                return IntrospectTokenAsync(
                        token,
                        CancellationToken.None
                );
            }

            /// <summary>
            /// Introspects the token.
            /// </summary>
            /// <param name="token">The token.</param>
            /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
            /// <returns>A Task&lt;IntrospectTokenResult&gt; representing the asynchronous operation.</returns>
            public async Task<IntrospectTokenResult> IntrospectTokenAsync(
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
                    result = await OnIntrospectTokenAsync(
                            token,
                            cancellationToken
                    ).ConfigureAwait(false);
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
            /// <returns>Task&lt;IntrospectTokenResult&gt;.</returns>
            public Task<IntrospectTokenResult> IntrospectTokenAsync(string accessTokenString)
            {
                return IntrospectTokenAsync(
                        accessTokenString,
                        CancellationToken.None
                );
            }

            /// <summary>
            /// Introspects the token.
            /// </summary>
            /// <param name="accessTokenString">The access token string.</param>
            /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
            /// <returns>Task&lt;IntrospectTokenResult&gt;.</returns>
            public Task<IntrospectTokenResult> IntrospectTokenAsync(
                    string accessTokenString,
                    CancellationToken cancellationToken)
            {
                return IntrospectTokenAsync(
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
            /// <returns>Task&lt;RedeemTokenResult&gt;.</returns>
            public Task<RedeemTokenResult> RedeemTokenAsync(string authorizationCode)
            {
                return RedeemTokenAsync(
                        authorizationCode,
                        CancellationToken.None
                );
            }

            /// <summary>
            /// Redeems the token.
            /// </summary>
            /// <param name="authorizationCode">The authorization code.</param>
            /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
            /// <returns>A Task&lt;RedeemTokenResult&gt; representing the asynchronous operation.</returns>
            public async Task<RedeemTokenResult> RedeemTokenAsync(
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
                    result = await OnRedeemTokenAsync(
                            authorizationCode,
                            cancellationToken
                    ).ConfigureAwait(false);
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
            /// <returns>Task&lt;RefreshTokenResult&gt;.</returns>
            public Task<RefreshTokenResult> RefreshTokenAsync(ClientTokenInfo token)
            {
                return RefreshTokenAsync(
                        token,
                        CancellationToken.None
                );
            }

            /// <summary>
            /// Refreshes the token.
            /// </summary>
            /// <param name="token">The token.</param>
            /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
            /// <returns>A Task&lt;RefreshTokenResult&gt; representing the asynchronous operation.</returns>
            public async Task<RefreshTokenResult> RefreshTokenAsync(
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
                    result = await OnRefreshTokenAsync(
                            token,
                            cancellationToken
                    ).ConfigureAwait(false);
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
            /// <returns>Task&lt;RefreshTokenResult&gt;.</returns>
            public Task<RefreshTokenResult> RefreshTokenAsync(string refreshTokenString)
            {
                return RefreshTokenAsync(
                        refreshTokenString,
                        CancellationToken.None
                );
            }

            /// <summary>
            /// Refreshes the token.
            /// </summary>
            /// <param name="refreshTokenString">The refresh token string.</param>
            /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
            /// <returns>Task&lt;RefreshTokenResult&gt;.</returns>
            public Task<RefreshTokenResult> RefreshTokenAsync(
                    string refreshTokenString,
                    CancellationToken cancellationToken)
            {
                return RefreshTokenAsync(
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
            /// <returns>Task&lt;AuthorizeResult&gt;.</returns>
            protected abstract Task<AuthorizeResult> OnAuthorizeAsync(CancellationToken cancellationToken);
            /// <summary>
            /// Called when introspecting token.
            /// </summary>
            /// <param name="token">The token.</param>
            /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
            /// <returns>Task&lt;IntrospectTokenResult&gt;.</returns>
            protected abstract Task<IntrospectTokenResult> OnIntrospectTokenAsync(
                    ClientTokenInfo token,
                    CancellationToken cancellationToken
            );
            /// <summary>
            /// Called when redeeming token.
            /// </summary>
            /// <param name="authorizationCode">The authorization code.</param>
            /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
            /// <returns>Task&lt;RedeemTokenResult&gt;.</returns>
            protected abstract Task<RedeemTokenResult> OnRedeemTokenAsync(
                    string authorizationCode,
                    CancellationToken cancellationToken
            );
            /// <summary>
            /// Called when refreshing token.
            /// </summary>
            /// <param name="token">The token.</param>
            /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
            /// <returns>Task&lt;RefreshTokenResult&gt;.</returns>
            protected abstract Task<RefreshTokenResult> OnRefreshTokenAsync(
                    ClientTokenInfo token,
                    CancellationToken cancellationToken
            );
        }
    }
}
