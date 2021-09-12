using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Htc.Vita.Core.Auth;
using Htc.Vita.Core.Net;
using Htc.Vita.Core.Util;
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
            authorizeResult = client.AuthorizeAsync().Result;
            authorizeStatus = authorizeResult.Status;
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
            const string authorizationCode = "testAuthorizationCode";
            var redeemTokenResult = client.RedeemToken(authorizationCode);
            var redeemTokenStatus = redeemTokenResult.Status;
            Assert.Equal(OAuth2.AuthorizationCodeClient.RedeemTokenStatus.NotImplemented, redeemTokenStatus);
            redeemTokenResult = client.RedeemTokenAsync(authorizationCode).Result;
            redeemTokenStatus = redeemTokenResult.Status;
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
            redeemTokenResult = client.RedeemTokenAsync(null).Result;
            redeemTokenStatus = redeemTokenResult.Status;
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
            const string refreshTokenString = "testRefreshTokenString";
            var refreshTokenResult = client.RefreshToken(refreshTokenString);
            var refreshTokenStatus = refreshTokenResult.Status;
            Assert.Equal(OAuth2.AuthorizationCodeClient.RefreshTokenStatus.NotImplemented, refreshTokenStatus);
            refreshTokenResult = client.RefreshTokenAsync(refreshTokenString).Result;
            refreshTokenStatus = refreshTokenResult.Status;
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
            const string refreshTokenString = "";
            var refreshTokenResult = client.RefreshToken(refreshTokenString);
            var refreshTokenStatus = refreshTokenResult.Status;
            Assert.Equal(OAuth2.AuthorizationCodeClient.RefreshTokenStatus.InvalidToken, refreshTokenStatus);
            refreshTokenResult = client.RefreshTokenAsync(refreshTokenString).Result;
            refreshTokenStatus = refreshTokenResult.Status;
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
            const string accessTokenString = "testAccessTokenString";
            var introspectTokenResult = client.IntrospectToken(accessTokenString);
            var introspectTokenStatus = introspectTokenResult.Status;
            Assert.Equal(OAuth2.AuthorizationCodeClient.IntrospectTokenStatus.NotImplemented, introspectTokenStatus);
            introspectTokenResult = client.IntrospectTokenAsync(accessTokenString).Result;
            introspectTokenStatus = introspectTokenResult.Status;
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
            const string accessTokenString = "";
            var introspectTokenResult = client.IntrospectToken(accessTokenString);
            var introspectTokenStatus = introspectTokenResult.Status;
            Assert.Equal(OAuth2.AuthorizationCodeClient.IntrospectTokenStatus.InvalidToken, introspectTokenStatus);
            introspectTokenResult = client.IntrospectTokenAsync(accessTokenString).Result;
            introspectTokenStatus = introspectTokenResult.Status;
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
                Assert.Equal(OAuth2.AuthorizationCodeReceiver.ReceiveStatus.UnsupportedReceiver, receiveStatus);
            }
        }

        [Fact]
        public static void AuthorizationCodeReceiver_1_Receive_withRedirectUriAndCode()
        {
            var clientAssistantFactory = OAuth2.ClientAssistantFactory.GetInstance();
            var unusedLocalPort = NetworkManager.GetInstance().GetUnusedLocalPort().LocalPort;
            Assert.True(unusedLocalPort > 0);
            var redirectUriString = $"http://localhost:{unusedLocalPort}/";
            const string authorizationCode = "testAuthorizationCode";
            var options = new Dictionary<string, object>
            {
                    { OAuth2.AuthorizationCodeReceiver.OptionRedirectUri, redirectUriString }
            };
            Task.Run(() =>
            {
                    using (WebRequestFactory.GetInstance()
                            .GetHttpWebRequest(new Uri($"{redirectUriString}?{OAuth2.Key.Code.GetDescription()}={authorizationCode}"))
                            .GetResponse())
                    {
                        // Skip
                    }
            });
            using (var authorizationCodeReceiver = clientAssistantFactory.GetAuthorizationCodeReceiver(options, CancellationToken.None))
            {
                Assert.NotNull(authorizationCodeReceiver);
                var receiveResult = authorizationCodeReceiver.Receive();
                var receiveStatus = receiveResult.Status;
                Assert.Equal(OAuth2.AuthorizationCodeReceiver.ReceiveStatus.Ok, receiveStatus);
                Assert.Equal(authorizationCode, receiveResult.Code);
            }
        }

        [Fact]
        public static void AuthorizationCodeUserAgent_0_PublicConstants()
        {
            Assert.False(string.IsNullOrWhiteSpace(OAuth2.AuthorizationCodeUserAgent.ObjectAndroidWebviewInstance));
            Assert.False(string.IsNullOrWhiteSpace(OAuth2.AuthorizationCodeUserAgent.OptionAndroidJavascriptActionMapWithUrlPrefixOnPageFinished));
            Assert.False(string.IsNullOrWhiteSpace(OAuth2.AuthorizationCodeUserAgent.OptionAuthorizationUrl));
        }

        [Fact]
        public static void AuthorizationCodeUserAgent_1_Launch()
        {
            var clientAssistantFactory = OAuth2.ClientAssistantFactory.GetInstance();
            Assert.NotNull(clientAssistantFactory);
            using (var userAgent = clientAssistantFactory.GetAuthorizationCodeUserAgent())
            {
                Assert.NotNull(userAgent);
                var launchResult = userAgent.Launch();
                var launchStatus = launchResult.Status;
                Assert.Equal(OAuth2.AuthorizationCodeUserAgent.LaunchStatus.InvalidAuthorizationUri, launchStatus);
            }
        }

        [Fact]
        public static void AuthorizationCodeUserAgent_1_Launch_withAuthorizationUrl()
        {
            var clientAssistantFactory = OAuth2.ClientAssistantFactory.GetInstance();
            const string authorizationUrlString = "https://www.microsoft.com/";
            var options = new Dictionary<string, object>
            {
                    { OAuth2.AuthorizationCodeUserAgent.OptionAuthorizationUrl, authorizationUrlString }
            };
            using (var authorizationCodeUserAgent = clientAssistantFactory.GetAuthorizationCodeUserAgent(options, CancellationToken.None))
            {
                Assert.NotNull(authorizationCodeUserAgent);
                var launchResult = authorizationCodeUserAgent.Launch();
                var launchStatus = launchResult.Status;
                Assert.Equal(OAuth2.AuthorizationCodeUserAgent.LaunchStatus.Ok, launchStatus);
            }
        }
    }
}
