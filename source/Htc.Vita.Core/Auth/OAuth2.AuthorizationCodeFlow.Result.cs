namespace Htc.Vita.Core.Auth
{
    public static partial class OAuth2
    {
        public partial class AuthorizationCodeClient
        {
            /// <summary>
            /// Class AuthorizeResult.
            /// </summary>
            public class AuthorizeResult
            {
                /// <summary>
                /// Gets or sets the code.
                /// </summary>
                /// <value>The code.</value>
                public string Code { get; set; }
                /// <summary>
                /// Gets or sets the status.
                /// </summary>
                /// <value>The status.</value>
                public AuthorizeStatus Status { get; set; } = AuthorizeStatus.Unknown;
            }

            /// <summary>
            /// Class IntrospectTokenResult.
            /// </summary>
            public class IntrospectTokenResult
            {
                /// <summary>
                /// Gets or sets the status.
                /// </summary>
                /// <value>The status.</value>
                public IntrospectTokenStatus Status { get; set; } = IntrospectTokenStatus.Unknown;
                /// <summary>
                /// Gets or sets the token.
                /// </summary>
                /// <value>The token.</value>
                public ClientTokenInfo Token { get; set; }
            }

            /// <summary>
            /// Class RedeemTokenResult.
            /// </summary>
            public class RedeemTokenResult
            {
                /// <summary>
                /// Gets or sets the status.
                /// </summary>
                /// <value>The status.</value>
                public RedeemTokenStatus Status { get; set; } = RedeemTokenStatus.Unknown;
                /// <summary>
                /// Gets or sets the token.
                /// </summary>
                /// <value>The token.</value>
                public ClientTokenInfo Token { get; set; }
            }

            /// <summary>
            /// Class RefreshTokenResult.
            /// </summary>
            public class RefreshTokenResult
            {
                /// <summary>
                /// Gets or sets the status.
                /// </summary>
                /// <value>The status.</value>
                public RefreshTokenStatus Status { get; set; } = RefreshTokenStatus.Unknown;
                /// <summary>
                /// Gets or sets the token.
                /// </summary>
                /// <value>The token.</value>
                public ClientTokenInfo Token { get; set; }
            }

            /// <summary>
            /// Enum AuthorizeStatus
            /// </summary>
            public enum AuthorizeStatus
            {
                /// <summary>
                /// Unknown
                /// </summary>
                Unknown,
                /// <summary>
                /// Ok
                /// </summary>
                Ok,
                /// <summary>
                /// Network error
                /// </summary>
                NetworkError,
                /// <summary>
                /// Server busy
                /// </summary>
                ServerBusy,
                /// <summary>
                /// Server error
                /// </summary>
                ServerError,
                /// <summary>
                /// Cancelled operation
                /// </summary>
                CancelledOperation,
                /// <summary>
                /// Invalid authorization URI
                /// </summary>
                InvalidAuthorizationUri,
                /// <summary>
                /// Invalid configuration
                /// </summary>
                InvalidConfig,
                /// <summary>
                /// Invalid redirect URI
                /// </summary>
                InvalidRedirectUri,
                /// <summary>
                /// Not implemented
                /// </summary>
                NotImplemented,
                /// <summary>
                /// Unsupported receiver
                /// </summary>
                UnsupportedReceiver,
                /// <summary>
                /// Unsupported user agent
                /// </summary>
                UnsupportedUserAgent
            }

            /// <summary>
            /// Enum IntrospectTokenStatus
            /// </summary>
            public enum IntrospectTokenStatus
            {
                /// <summary>
                /// Unknown
                /// </summary>
                Unknown,
                /// <summary>
                /// Ok
                /// </summary>
                Ok,
                /// <summary>
                /// Network error
                /// </summary>
                NetworkError,
                /// <summary>
                /// Server busy
                /// </summary>
                ServerBusy,
                /// <summary>
                /// Server error
                /// </summary>
                ServerError,
                /// <summary>
                /// Cancelled operation
                /// </summary>
                CancelledOperation,
                /// <summary>
                /// Changed credential
                /// </summary>
                ChangedCredential,
                /// <summary>
                /// Expired token
                /// </summary>
                ExpiredToken,
                /// <summary>
                /// Invalid configuration
                /// </summary>
                InvalidConfig,
                /// <summary>
                /// Invalid token introspection URI
                /// </summary>
                InvalidTokenIntrospectionUri,
                /// <summary>
                /// Invalid token
                /// </summary>
                InvalidToken,
                /// <summary>
                /// Non-existent account
                /// </summary>
                NonExistentAccount,
                /// <summary>
                /// Not implemented
                /// </summary>
                NotImplemented
            }

            /// <summary>
            /// Enum RedeemTokenStatus
            /// </summary>
            public enum RedeemTokenStatus
            {
                /// <summary>
                /// Unknown
                /// </summary>
                Unknown,
                /// <summary>
                /// Ok
                /// </summary>
                Ok,
                /// <summary>
                /// Network error
                /// </summary>
                NetworkError,
                /// <summary>
                /// Server busy
                /// </summary>
                ServerBusy,
                /// <summary>
                /// Server error
                /// </summary>
                ServerError,
                /// <summary>
                /// Cancelled operation
                /// </summary>
                CancelledOperation,
                /// <summary>
                /// Invalid authorization code
                /// </summary>
                InvalidAuthorizationCode,
                /// <summary>
                /// Invalid configuration
                /// </summary>
                InvalidConfig,
                /// <summary>
                /// Not implemented
                /// </summary>
                NotImplemented
            }

            /// <summary>
            /// Enum RefreshTokenStatus
            /// </summary>
            public enum RefreshTokenStatus
            {
                /// <summary>
                /// Unknown
                /// </summary>
                Unknown,
                /// <summary>
                /// Ok
                /// </summary>
                Ok,
                /// <summary>
                /// Network error
                /// </summary>
                NetworkError,
                /// <summary>
                /// Server busy
                /// </summary>
                ServerBusy,
                /// <summary>
                /// Server error
                /// </summary>
                ServerError,
                /// <summary>
                /// Cancelled operation
                /// </summary>
                CancelledOperation,
                /// <summary>
                /// Invalid configuration
                /// </summary>
                InvalidConfig,
                /// <summary>
                /// Invalid token refresh URI
                /// </summary>
                InvalidTokenRefreshUri,
                /// <summary>
                /// Invalid token
                /// </summary>
                InvalidToken,
                /// <summary>
                /// Not implemented
                /// </summary>
                NotImplemented
            }
        }

        public partial class AuthorizationCodeReceiver
        {
            /// <summary>
            /// Class ReceiveResult.
            /// </summary>
            public class ReceiveResult
            {
                /// <summary>
                /// Gets or sets the code.
                /// </summary>
                /// <value>The code.</value>
                public string Code { get; set; }
                /// <summary>
                /// Gets or sets the status.
                /// </summary>
                /// <value>The status.</value>
                public ReceiveStatus Status { get; set; } = ReceiveStatus.Unknown;
            }

            /// <summary>
            /// Enum ReceiveStatus
            /// </summary>
            public enum ReceiveStatus
            {
                /// <summary>
                /// Unknown
                /// </summary>
                Unknown,
                /// <summary>
                /// Ok
                /// </summary>
                Ok,
                /// <summary>
                /// Cancelled operation
                /// </summary>
                CancelledOperation,
                /// <summary>
                /// Invalid authorization code
                /// </summary>
                InvalidAuthorizationCode,
                /// <summary>
                /// Not implemented
                /// </summary>
                NotImplemented,
                /// <summary>
                /// Unsupported receiver
                /// </summary>
                UnsupportedReceiver
            }
        }

        public partial class AuthorizationCodeUserAgent
        {
            /// <summary>
            /// Class LaunchResult.
            /// </summary>
            public class LaunchResult
            {
                /// <summary>
                /// Gets or sets the status.
                /// </summary>
                /// <value>The status.</value>
                public LaunchStatus Status { get; set; }
            }

            /// <summary>
            /// Enum LaunchStatus
            /// </summary>
            public enum LaunchStatus
            {
                /// <summary>
                /// Unknown
                /// </summary>
                Unknown,
                /// <summary>
                /// Ok
                /// </summary>
                Ok,
                /// <summary>
                /// Cancelled operation
                /// </summary>
                CancelledOperation,
                /// <summary>
                /// Invalid authorization URI
                /// </summary>
                InvalidAuthorizationUri,
                /// <summary>
                /// Not implemented
                /// </summary>
                NotImplemented,
                /// <summary>
                /// Unsupported user agent
                /// </summary>
                UnsupportedUserAgent
            }
        }
    }
}
