using System.Threading;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Auth
{
    /// <summary>
    /// Class DummyOAuth2.
    /// </summary>
    public static partial class DummyOAuth2
    {
        /// <summary>
        /// Class DummyAuthorizationCodeClient.
        /// Implements the <see cref="OAuth2.AuthorizationCodeClient" />
        /// </summary>
        /// <seealso cref="OAuth2.AuthorizationCodeClient" />
        public partial class DummyAuthorizationCodeClient : OAuth2.AuthorizationCodeClient
        {
            private OAuth2.AuthorizationCodeClientConfig _config;

            /// <inheritdoc />
            protected override AuthorizeResult OnAuthorize(CancellationToken cancellationToken)
            {
                return new AuthorizeResult
                {
                        Status = AuthorizeStatus.NotImplemented
                };
            }

            /// <inheritdoc />
            protected override OAuth2.AuthorizationCodeClient OnInitialize(OAuth2.AuthorizationCodeClientConfig config)
            {
                _config = config;
                return this;
            }

            /// <inheritdoc />
            protected override IntrospectTokenResult OnIntrospectToken(
                    OAuth2.ClientTokenInfo token,
                    CancellationToken cancellationToken)
            {
                return new IntrospectTokenResult
                {
                        Status = IntrospectTokenStatus.NotImplemented
                };
            }

            /// <inheritdoc />
            protected override OAuth2.AuthorizationCodeClientConfig OnGetConfig()
            {
                return _config;
            }

            /// <inheritdoc />
            protected override OAuth2.ClientTokenInfo OnGetToken()
            {
                return null;
            }

            /// <inheritdoc />
            protected override RedeemTokenResult OnRedeemToken(
                    string authorizationCode,
                    CancellationToken cancellationToken)
            {
                return new RedeemTokenResult
                {
                        Status = RedeemTokenStatus.NotImplemented
                };
            }

            /// <inheritdoc />
            protected override RefreshTokenResult OnRefreshToken(
                    OAuth2.ClientTokenInfo token,
                    CancellationToken cancellationToken)
            {
                return new RefreshTokenResult
                {
                        Status = RefreshTokenStatus.NotImplemented
                };
            }
        }

        /// <summary>
        /// Class DummyClientFactory.
        /// Implements the <see cref="OAuth2.ClientFactory" />
        /// </summary>
        /// <seealso cref="OAuth2.ClientFactory" />
        public class DummyClientFactory : OAuth2.ClientFactory
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="DummyClientFactory"/> class.
            /// </summary>
            public DummyClientFactory()
            {
                Logger.GetInstance(typeof(DummyClientFactory)).Error($"You are using dummy {nameof(OAuth2.ClientFactory)} instance!!");
            }

            /// <inheritdoc />
            protected override OAuth2.AuthorizationCodeClient OnGetAuthorizationCodeClient(OAuth2.AuthorizationCodeClientConfig config)
            {
                return new DummyAuthorizationCodeClient().Initialize(config);
            }
        }
    }
}
