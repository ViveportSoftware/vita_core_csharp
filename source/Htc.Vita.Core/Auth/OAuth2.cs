using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Htc.Vita.Core.Auth
{
    /// <summary>
    /// Class OAuth2.
    /// </summary>
    public static partial class OAuth2
    {
        /// <summary>
        /// Class AuthorizationCodeClientConfig.
        /// </summary>
        public class AuthorizationCodeClientConfig
        {
            private Uri _accessTokenUri;
            private Uri _authorizationUri;
            private string _clientId;
            private string _clientSecret;
            private Uri _introspectTokenUri;
            private Uri _redirectUri;
            private Uri _refreshTokenUri;
            private ISet<string> _tokenScope = new HashSet<string>();

            /// <summary>
            /// Gets or sets the access token URI.
            /// </summary>
            /// <value>The access token URI.</value>
            public Uri AccessTokenUri
            {
                get
                {
                    return _accessTokenUri;
                }
                set
                {
                    if (value == null)
                    {
                        return;
                    }
                    _accessTokenUri = value;
                }
            }
            /// <summary>
            /// Gets or sets the authorization URI.
            /// </summary>
            /// <value>The authorization URI.</value>
            public Uri AuthorizationUri
            {
                get
                {
                    return _authorizationUri;
                }
                set
                {
                    if (value == null)
                    {
                        return;
                    }
                    _authorizationUri = value;
                }
            }
            /// <summary>
            /// Gets or sets the client identifier.
            /// </summary>
            /// <value>The client identifier.</value>
            public string ClientId
            {
                get
                {
                    return _clientId;
                }
                set
                {
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        return;
                    }
                    _clientId = value;
                }
            }
            /// <summary>
            /// Gets or sets the client secret.
            /// </summary>
            /// <value>The client secret.</value>
            public string ClientSecret
            {
                get
                {
                    return _clientSecret;
                }
                set
                {
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        return;
                    }
                    _clientSecret = value;
                }
            }
            /// <summary>
            /// Gets or sets the introspect token URI.
            /// </summary>
            /// <value>The introspect token URI.</value>
            public Uri IntrospectTokenUri
            {
                get
                {
                    return _introspectTokenUri;
                }
                set
                {
                    if (value == null)
                    {
                        return;
                    }
                    _introspectTokenUri = value;
                }
            }
            /// <summary>
            /// Gets the options.
            /// </summary>
            /// <value>The options.</value>
            public Dictionary<string, object> Options { get; } = new Dictionary<string, object>();
            /// <summary>
            /// Gets or sets the redirect URI.
            /// </summary>
            /// <value>The redirect URI.</value>
            public Uri RedirectUri
            {
                get
                {
                    return _redirectUri;
                }
                set
                {
                    if (value == null)
                    {
                        return;
                    }
                    _redirectUri = value;
                }
            }
            /// <summary>
            /// Gets or sets the refresh token URI.
            /// </summary>
            /// <value>The refresh token URI.</value>
            public Uri RefreshTokenUri
            {
                get
                {
                    return _refreshTokenUri;
                }
                set
                {
                    if (value == null)
                    {
                        return;
                    }
                    _refreshTokenUri = value;
                }
            }
            /// <summary>
            /// Gets the token scope.
            /// </summary>
            /// <value>The token scope.</value>
            public ISet<string> TokenScope
            {
                get
                {
                    return _tokenScope;
                }
                set
                {
                    if (value == null)
                    {
                        return;
                    }
                    _tokenScope = value;
                }
            }
        }

        /// <summary>
        /// Class ClientTokenInfo.
        /// </summary>
        public class ClientTokenInfo
        {
            private int _expiresInSec;
            private Dictionary<string, string> _options = new Dictionary<string, string>();
            private ISet<string> _tokenScope = new HashSet<string>();

            /// <summary>
            /// Gets or sets the access token.
            /// </summary>
            /// <value>The access token.</value>
            public string AccessToken { get; set; }
            /// <summary>
            /// Gets or sets the expires in sec.
            /// </summary>
            /// <value>The expires in sec.</value>
            public int ExpiresInSec
            {
                get
                {
                    return _expiresInSec;
                }
                set
                {
                    if (value < 0)
                    {
                        return;
                    }
                    _expiresInSec = value;
                }
            }
            /// <summary>
            /// Gets the options.
            /// </summary>
            /// <value>The options.</value>
            public Dictionary<string, string> Options
            {
                get
                {
                    return _options;
                }
                set
                {
                    if (value == null)
                    {
                        return;
                    }
                    _options = value;
                }
            }
            /// <summary>
            /// Gets or sets the refresh token.
            /// </summary>
            /// <value>The refresh token.</value>
            public string RefreshToken { get; set; }
            /// <summary>
            /// Gets the token scope.
            /// </summary>
            /// <value>The token scope.</value>
            public ISet<string> TokenScope
            {
                get
                {
                    return _tokenScope;
                }
                set
                {
                    if (value == null)
                    {
                        return;
                    }
                    _tokenScope = value;
                }
            }
            /// <summary>
            /// Gets or sets the type.
            /// </summary>
            /// <value>The type.</value>
            public TokenType Type { get; set; } = TokenType.Unknown;

            /// <summary>
            /// Fills the empty properties with another token.
            /// </summary>
            /// <param name="another">Another token.</param>
            /// <returns>ClientTokenInfo.</returns>
            public ClientTokenInfo FillEmptyPropertiesWith(ClientTokenInfo another)
            {
                if (another == null)
                {
                    return this;
                }

                if (string.IsNullOrWhiteSpace(AccessToken))
                {
                    var anotherAccessToken = another.AccessToken;
                    if (!string.IsNullOrWhiteSpace(anotherAccessToken))
                    {
                        AccessToken = anotherAccessToken;
                    }
                }

                if (ExpiresInSec <= 0)
                {
                    var anotherExpiresInSec = another.ExpiresInSec;
                    if (anotherExpiresInSec > 0)
                    {
                        ExpiresInSec = anotherExpiresInSec;
                    }
                }

                if (Options == null || Options.Count <= 0)
                {
                    var anotherOptions = another.Options;
                    if (anotherOptions != null && anotherOptions.Count > 0)
                    {
                        Options = anotherOptions;
                    }
                }

                if (string.IsNullOrWhiteSpace(RefreshToken))
                {
                    var anotherRefreshToken = another.RefreshToken;
                    if (!string.IsNullOrWhiteSpace(anotherRefreshToken))
                    {
                        RefreshToken = anotherRefreshToken;
                    }
                }

                if (TokenScope == null)
                {
                    var anotherTokenScope = another.TokenScope;
                    if (anotherTokenScope != null)
                    {
                        TokenScope = anotherTokenScope;
                    }
                }

                if (Type == TokenType.Unknown)
                {
                    var anotherType = another.Type;
                    if (anotherType != TokenType.Unknown)
                    {
                        Type = anotherType;
                    }
                }

                return this;
            }
        }

        /// <summary>
        /// Enum GrantType
        /// </summary>
        public enum GrantType
        {
            /// <summary>
            /// The grant type for authorization code
            /// </summary>
            [Description("authorization_code")]
            AuthorizationCode,
            /// <summary>
            /// The grant type for client credentials
            /// </summary>
            [Description("client_credentials")]
            ClientCredentials,
            /// <summary>
            /// The grant type for device code
            /// </summary>
            [Description("urn:ietf:params:oauth:grant-type:device_code")]
            DeviceCode,
            /// <summary>
            /// The grant type for password
            /// </summary>
            [Description("password")]
            Password,
            /// <summary>
            /// The grant type for refresh token
            /// </summary>
            [Description("refresh_token")]
            RefreshToken
        }

        /// <summary>
        /// Enum Key
        /// </summary>
        public enum Key
        {
            /// <summary>
            /// The key for access token
            /// </summary>
            [Description("access_token")]
            AccessToken,
            /// <summary>
            /// The key for client identifier
            /// </summary>
            [Description("client_id")]
            ClientId,
            /// <summary>
            /// The key for client secret
            /// </summary>
            [Description("client_secret")]
            ClientSecret,
            /// <summary>
            /// The key for code
            /// </summary>
            [Description("code")]
            Code,
            /// <summary>
            /// The key for expires-in
            /// </summary>
            [Description("expires_in")]
            ExpiresIn,
            /// <summary>
            /// The key for grant type
            /// </summary>
            [Description("grant_type")]
            GrantType,
            /// <summary>
            /// The key for redirect URI
            /// </summary>
            [Description("redirection_uri")]
            RedirectUri,
            /// <summary>
            /// The key for refresh token
            /// </summary>
            [Description("refresh_token")]
            RefreshToken,
            /// <summary>
            /// The key for response type
            /// </summary>
            [Description("response_type")]
            ResponseType,
            /// <summary>
            /// The key for scope
            /// </summary>
            [Description("scope")]
            Scope,
            /// <summary>
            /// The key for token type
            /// </summary>
            [Description("token_type")]
            TokenType
        }

        /// <summary>
        /// Enum ResponseType
        /// </summary>
        public enum ResponseType
        {
            /// <summary>
            /// The response is code
            /// </summary>
            [Description("code")]
            Code,
            /// <summary>
            /// The response is token
            /// </summary>
            [Description("token")]
            Token
        }

        /// <summary>
        /// Enum TokenType
        /// </summary>
        public enum TokenType
        {
            /// <summary>
            /// Token type is unknown
            /// </summary>
            [Description("Unknown")]
            Unknown,
            /// <summary>
            /// Bearer token
            /// </summary>
            [Description("Bearer")]
            Bearer
        }
    }
}
