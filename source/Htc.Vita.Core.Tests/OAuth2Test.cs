using Htc.Vita.Core.Auth;
using Xunit;

namespace Htc.Vita.Core.Tests
{
    public static class OAuth2Test
    {
        [Fact]
        public static void ClientFactory_0_GetInstance()
        {
            var clientFactory = OAuth2.ClientFactory.GetInstance();
            Assert.NotNull(clientFactory);
        }

        [Fact]
        public static void ClientFactory_1_GetAuthorizationCodeClient()
        {
            var clientFactory = OAuth2.ClientFactory.GetInstance();
            Assert.NotNull(clientFactory);
            var client0 = clientFactory.GetAuthorizationCodeClient(null);
            Assert.NotNull(client0);
            var clientConfig = new OAuth2.AuthorizationCodeClientConfig();
            var client1 = clientFactory.GetAuthorizationCodeClient(clientConfig);
            Assert.NotNull(client1);
            Assert.NotEqual(client0, client1);
        }

        [Fact]
        public static void AuthorizationCodeClient_0_Authorize()
        {
            var clientFactory = OAuth2.ClientFactory.GetInstance();
            Assert.NotNull(clientFactory);
            var clientConfig = new OAuth2.AuthorizationCodeClientConfig();
            var client = clientFactory.GetAuthorizationCodeClient(clientConfig);
            Assert.NotNull(client);
            var authorizeResult = client.Authorize();
            var authorizeStatus = authorizeResult.Status;
            Assert.Equal(OAuth2.AuthorizationCodeClient.AuthorizeStatus.NotImplemented, authorizeStatus);
        }

        [Fact]
        public static void AuthorizationCodeClient_1_GetConfig()
        {
            var clientFactory = OAuth2.ClientFactory.GetInstance();
            Assert.NotNull(clientFactory);
            var clientConfig0 = new OAuth2.AuthorizationCodeClientConfig();
            var client = clientFactory.GetAuthorizationCodeClient(clientConfig0);
            Assert.NotNull(client);
            var clientConfig1 = client.GetConfig();
            Assert.NotNull(clientConfig1);
            Assert.Equal(clientConfig0, clientConfig1);
        }

        [Fact]
        public static void AuthorizationCodeClient_2_GetToken()
        {
            var clientFactory = OAuth2.ClientFactory.GetInstance();
            Assert.NotNull(clientFactory);
            var clientConfig = new OAuth2.AuthorizationCodeClientConfig();
            var client = clientFactory.GetAuthorizationCodeClient(clientConfig);
            Assert.NotNull(client);
            var token = client.GetToken();
            Assert.Null(token);
        }

        [Fact]
        public static void AuthorizationCodeClient_3_RedeemToken()
        {
            var clientFactory = OAuth2.ClientFactory.GetInstance();
            Assert.NotNull(clientFactory);
            var clientConfig = new OAuth2.AuthorizationCodeClientConfig();
            var client = clientFactory.GetAuthorizationCodeClient(clientConfig);
            Assert.NotNull(client);
            var redeemTokenResult = client.RedeemToken("testAuthorizationCode");
            var redeemTokenStatus = redeemTokenResult.Status;
            Assert.Equal(OAuth2.AuthorizationCodeClient.RedeemTokenStatus.NotImplemented, redeemTokenStatus);
        }

        [Fact]
        public static void AuthorizationCodeClient_3_RedeemToken_withoutAuthorizationCode()
        {
            var clientFactory = OAuth2.ClientFactory.GetInstance();
            Assert.NotNull(clientFactory);
            var clientConfig = new OAuth2.AuthorizationCodeClientConfig();
            var client = clientFactory.GetAuthorizationCodeClient(clientConfig);
            Assert.NotNull(client);
            var redeemTokenResult = client.RedeemToken(null);
            var redeemTokenStatus = redeemTokenResult.Status;
            Assert.Equal(OAuth2.AuthorizationCodeClient.RedeemTokenStatus.InvalidAuthorizationCode, redeemTokenStatus);
        }

        [Fact]
        public static void AuthorizationCodeClient_4_RefreshToken()
        {
            var clientFactory = OAuth2.ClientFactory.GetInstance();
            Assert.NotNull(clientFactory);
            var clientConfig = new OAuth2.AuthorizationCodeClientConfig();
            var client = clientFactory.GetAuthorizationCodeClient(clientConfig);
            Assert.NotNull(client);
            var refreshTokenResult = client.RefreshToken("testRefreshTokenString");
            var refreshTokenStatus = refreshTokenResult.Status;
            Assert.Equal(OAuth2.AuthorizationCodeClient.RefreshTokenStatus.NotImplemented, refreshTokenStatus);
        }

        [Fact]
        public static void AuthorizationCodeClient_4_RefreshToken_withoutRefreshTokenString()
        {
            var clientFactory = OAuth2.ClientFactory.GetInstance();
            Assert.NotNull(clientFactory);
            var clientConfig = new OAuth2.AuthorizationCodeClientConfig();
            var client = clientFactory.GetAuthorizationCodeClient(clientConfig);
            Assert.NotNull(client);
            var refreshTokenResult = client.RefreshToken("");
            var refreshTokenStatus = refreshTokenResult.Status;
            Assert.Equal(OAuth2.AuthorizationCodeClient.RefreshTokenStatus.InvalidToken, refreshTokenStatus);
        }

        [Fact]
        public static void AuthorizationCodeClient_5_IntrospectToken()
        {
            var clientFactory = OAuth2.ClientFactory.GetInstance();
            Assert.NotNull(clientFactory);
            var clientConfig = new OAuth2.AuthorizationCodeClientConfig();
            var client = clientFactory.GetAuthorizationCodeClient(clientConfig);
            Assert.NotNull(client);
            var introspectTokenResult = client.IntrospectToken("testAccessTokenString");
            var introspectTokenStatus = introspectTokenResult.Status;
            Assert.Equal(OAuth2.AuthorizationCodeClient.IntrospectTokenStatus.NotImplemented, introspectTokenStatus);
        }

        [Fact]
        public static void AuthorizationCodeClient_5_IntrospectToken_withoutAccessTokenString()
        {
            var clientFactory = OAuth2.ClientFactory.GetInstance();
            Assert.NotNull(clientFactory);
            var clientConfig = new OAuth2.AuthorizationCodeClientConfig();
            var client = clientFactory.GetAuthorizationCodeClient(clientConfig);
            Assert.NotNull(client);
            var introspectTokenResult = client.IntrospectToken("");
            var introspectTokenStatus = introspectTokenResult.Status;
            Assert.Equal(OAuth2.AuthorizationCodeClient.IntrospectTokenStatus.InvalidToken, introspectTokenStatus);
        }

        [Fact]
        public static void ClientAssistantFactory_0_GetInstance()
        {
            var clientAssistantFactory = OAuth2.ClientAssistantFactory.GetInstance();
            Assert.NotNull(clientAssistantFactory);
        }

        [Fact]
        public static void ClientAssistantFactory_1_GetAuthorizationCodeReceiver()
        {
            var clientAssistantFactory = OAuth2.ClientAssistantFactory.GetInstance();
            Assert.NotNull(clientAssistantFactory);
            using (var receiver = clientAssistantFactory.GetAuthorizationCodeReceiver())
            {
                Assert.NotNull(receiver);
            }
        }

        [Fact]
        public static void ClientAssistantFactory_2_GetAuthorizationCodeUserAgent()
        {
            var clientAssistantFactory = OAuth2.ClientAssistantFactory.GetInstance();
            Assert.NotNull(clientAssistantFactory);
            using (var userAgent = clientAssistantFactory.GetAuthorizationCodeUserAgent())
            {
                Assert.NotNull(userAgent);
            }
        }

        [Fact]
        public static void AuthorizationCodeReceiver_0_PublicConstants()
        {
            Assert.False(string.IsNullOrWhiteSpace(OAuth2.AuthorizationCodeReceiver.OptionRedirectUri));
        }

        [Fact]
        public static void AuthorizationCodeReceiver_1_Receive()
        {
            var clientAssistantFactory = OAuth2.ClientAssistantFactory.GetInstance();
            Assert.NotNull(clientAssistantFactory);
            using (var receiver = clientAssistantFactory.GetAuthorizationCodeReceiver())
            {
                Assert.NotNull(receiver);
                var receiveResult = receiver.Receive();
                var receiveStatus = receiveResult.Status;
                Assert.Equal(OAuth2.AuthorizationCodeReceiver.ReceiveStatus.NotImplemented, receiveStatus);
            }
        }

        [Fact]
        public static void AuthorizationCodeUserAgent_0_PublicConstants()
        {
            Assert.False(string.IsNullOrWhiteSpace(OAuth2.AuthorizationCodeUserAgent.OptionAuthorizationUrl));
        }

        [Fact]
        public static void AuthorizationCodeUserAgent_0_Launch()
        {
            var clientAssistantFactory = OAuth2.ClientAssistantFactory.GetInstance();
            Assert.NotNull(clientAssistantFactory);
            using (var userAgent = clientAssistantFactory.GetAuthorizationCodeUserAgent())
            {
                Assert.NotNull(userAgent);
                var launchResult = userAgent.Launch();
                var launchStatus = launchResult.Status;
                Assert.Equal(OAuth2.AuthorizationCodeUserAgent.LaunchStatus.NotImplemented, launchStatus);
            }
        }
    }
}
