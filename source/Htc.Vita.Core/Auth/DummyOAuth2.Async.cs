using System.Threading;
using System.Threading.Tasks;

namespace Htc.Vita.Core.Auth
{
    public static partial class DummyOAuth2
    {
        public partial class DummyAuthorizationCodeClient
        {
            /// <inheritdoc />
            protected override Task<AuthorizeResult> OnAuthorizeAsync(CancellationToken cancellationToken)
            {
                return Task.Run(
                        () => OnAuthorize(cancellationToken),
                        CancellationToken.None
                );
            }

            /// <inheritdoc />
            protected override Task<IntrospectTokenResult> OnIntrospectTokenAsync(
                    OAuth2.ClientTokenInfo token,
                    CancellationToken cancellationToken)
            {
                return Task.Run(
                        () => OnIntrospectToken(
                                token,
                                cancellationToken
                        ), CancellationToken.None
                );
            }

            /// <inheritdoc />
            protected override Task<RedeemTokenResult> OnRedeemTokenAsync(
                    string authorizationCode,
                    CancellationToken cancellationToken)
            {
                return Task.Run(
                        () => OnRedeemToken(
                                authorizationCode,
                                cancellationToken
                        ), CancellationToken.None
                );
            }

            /// <inheritdoc />
            protected override Task<RefreshTokenResult> OnRefreshTokenAsync(
                    OAuth2.ClientTokenInfo token,
                    CancellationToken cancellationToken)
            {
                return Task.Run(
                        () => OnRefreshToken(
                                token,
                                cancellationToken
                        ), CancellationToken.None
                );
            }
        }
    }
}
