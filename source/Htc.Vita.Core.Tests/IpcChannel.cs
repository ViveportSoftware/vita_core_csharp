using System;
using System.Collections.Generic;
using Xunit;

namespace Htc.Vita.Core.Tests
{
    public static class IpcChannel
    {
        [Fact]
        public static void Provider_0_GetInstance()
        {
            if (!Runtime.Platform.IsWindows)
            {
                return;
            }
            var provider = Runtime.IpcChannel.Provider.GetInstance();
            Assert.NotNull(provider);
        }

        [Fact]
        public static void Provider_1_SetName()
        {
            if (!Runtime.Platform.IsWindows)
            {
                return;
            }
            var provider = Runtime.IpcChannel.Provider.GetInstance();
            Assert.NotNull(provider);
            Assert.True(provider.SetName("" + Util.Convert.ToTimestampInMilli(DateTime.UtcNow)));
        }

        [Fact]
        public static void Provider_2_Start_Stop()
        {
            if (!Runtime.Platform.IsWindows)
            {
                return;
            }
            var provider = Runtime.IpcChannel.Provider.GetInstance();
            Assert.NotNull(provider);
            Assert.True(provider.SetName("" + Util.Convert.ToTimestampInMilli(DateTime.UtcNow)));
            Assert.True(provider.Start());
            Assert.True(provider.Stop());
        }

        [Fact]
        public static void Provider_2_StartTwice_Stop()
        {
            if (!Runtime.Platform.IsWindows)
            {
                return;
            }
            var provider = Runtime.IpcChannel.Provider.GetInstance();
            Assert.NotNull(provider);
            Assert.True(provider.SetName("" + Util.Convert.ToTimestampInMilli(DateTime.UtcNow)));
            Assert.True(provider.Start());
            Assert.True(provider.Start());
            Assert.True(provider.Stop());
        }

        [Fact]
        public static void Provider_2_IsRunning()
        {
            if (!Runtime.Platform.IsWindows)
            {
                return;
            }
            var provider = Runtime.IpcChannel.Provider.GetInstance();
            Assert.NotNull(provider);
            Assert.True(provider.SetName("" + Util.Convert.ToTimestampInMilli(DateTime.UtcNow)));
            Assert.True(provider.Start());
            Assert.True(provider.IsRunning());
            Assert.True(provider.Stop());
        }

        [Fact]
        public static void Provider_3_OnMessageHandled()
        {
            if (!Runtime.Platform.IsWindows)
            {
                return;
            }
            var provider = Runtime.IpcChannel.Provider.GetInstance();
            Assert.NotNull(provider);
            Assert.True(provider.SetName("" + Util.Convert.ToTimestampInMilli(DateTime.UtcNow)));
            Assert.True(provider.Start());
            provider.OnMessageHandled = (channel, filePropertiesInfo) =>
            {
                Console.WriteLine("channel?.Output: " + channel?.Output);
            };
            Assert.True(provider.Stop());
        }

        [Fact]
        public static void Client_0_GetInstance()
        {
            if (!Runtime.Platform.IsWindows)
            {
                return;
            }
            var client = Runtime.IpcChannel.Client.GetInstance();
            Assert.NotNull(client);
        }

        [Fact]
        public static void Client_1_SetName()
        {
            if (!Runtime.Platform.IsWindows)
            {
                return;
            }
            var client = Runtime.IpcChannel.Client.GetInstance();
            Assert.NotNull(client);
            Assert.True(client.SetName("" + Util.Convert.ToTimestampInMilli(DateTime.UtcNow)));
        }

        [Fact]
        public static void Client_2_IsReady()
        {
            if (!Runtime.Platform.IsWindows)
            {
                return;
            }
            var name = "" + Util.Convert.ToTimestampInMilli(DateTime.UtcNow);
            var provider = Runtime.IpcChannel.Provider.GetInstance();
            Assert.NotNull(provider);
            Assert.True(provider.SetName(name));
            Assert.True(provider.Start());
            var client = Runtime.IpcChannel.Client.GetInstance();
            Assert.NotNull(client);
            Assert.True(client.SetName(name));
            Assert.True(client.IsReady());
            Assert.True(provider.Stop());
        }

        [Fact]
        public static void Client_2_IsReady_WithDigitalSignature()
        {
            if (!Runtime.Platform.IsWindows)
            {
                return;
            }
            var name = "" + Util.Convert.ToTimestampInMilli(DateTime.UtcNow);
            var provider = Runtime.IpcChannel.Provider.GetInstance();
            Assert.NotNull(provider);
            Assert.True(provider.SetName(name));
            Assert.True(provider.Start());
            var client = Runtime.IpcChannel.Client.GetInstance();
            Assert.NotNull(client);
            Assert.True(client.SetName(name));
            var options = new Dictionary<string, string>
            {
                {Runtime.IpcChannel.Client.OptionVerifyProvider, "true"}
            };
            Assert.True(client.IsReady(options));
            Assert.True(client.IsReady());
            Assert.True(provider.Stop());
        }

        [Fact]
        public static void Client_3_Request()
        {
            if (!Runtime.Platform.IsWindows)
            {
                return;
            }
            var name = "" + Util.Convert.ToTimestampInMilli(DateTime.UtcNow);
            var provider = Runtime.IpcChannel.Provider.GetInstance();
            Assert.NotNull(provider);
            Assert.True(provider.SetName(name));
            Assert.True(provider.Start());
            var client = Runtime.IpcChannel.Client.GetInstance();
            Assert.NotNull(client);
            Assert.True(client.SetName(name));
            Assert.True(client.IsReady());
            Assert.True(string.IsNullOrEmpty(client.Request("Test")));
            Assert.True(provider.Stop());
        }

        [Fact]
        public static void Client_3_Request_WithEmptyInput()
        {
            if (!Runtime.Platform.IsWindows)
            {
                return;
            }
            var name = "" + Util.Convert.ToTimestampInMilli(DateTime.UtcNow);
            var provider = Runtime.IpcChannel.Provider.GetInstance();
            Assert.NotNull(provider);
            Assert.True(provider.SetName(name));
            Assert.True(provider.Start());
            var client = Runtime.IpcChannel.Client.GetInstance();
            Assert.NotNull(client);
            Assert.True(client.SetName(name));
            Assert.True(client.IsReady());
            Assert.True(string.IsNullOrEmpty(client.Request(null)));
            Assert.True(provider.Stop());
        }

        [Fact]
        public static void Client_3_Request_WithResponse()
        {
            if (!Runtime.Platform.IsWindows)
            {
                return;
            }
            var name = "" + Util.Convert.ToTimestampInMilli(DateTime.UtcNow);
            var provider = Runtime.IpcChannel.Provider.GetInstance();
            Assert.NotNull(provider);
            Assert.True(provider.SetName(name));
            provider.OnMessageHandled = (channel, filePropertiesInfo) =>
            {
                if (channel == null)
                {
                    return;
                }
                if (channel.Output.Equals("TestRequest"))
                {
                    channel.Input = "TestResponse";
                }
            };
            Assert.True(provider.Start());
            var client = Runtime.IpcChannel.Client.GetInstance();
            Assert.NotNull(client);
            Assert.True(client.SetName(name));
            Assert.True(client.IsReady());
            Assert.Equal("TestResponse", client.Request("TestRequest"));
            Assert.True(provider.Stop());
        }

        [Fact]
        public static void Client_3_Request_WithDigitalSignature()
        {
            if (!Runtime.Platform.IsWindows)
            {
                return;
            }
            var name = "" + Util.Convert.ToTimestampInMilli(DateTime.UtcNow);
            var provider = Runtime.IpcChannel.Provider.GetInstance();
            Assert.NotNull(provider);
            Assert.True(provider.SetName(name));
            provider.OnMessageHandled = (channel, filePropertiesInfo) =>
            {
                if (channel == null)
                {
                    return;
                }
                if (filePropertiesInfo != null && filePropertiesInfo.Verified && channel.Output.Equals("TestRequest"))
                {
                    channel.Input = "TestResponse";
                }
            };
            Assert.True(provider.Start());
            var client = Runtime.IpcChannel.Client.GetInstance();
            Assert.NotNull(client);
            Assert.True(client.SetName(name));
            Assert.True(client.IsReady());
            Assert.False(string.IsNullOrEmpty(client.Request("TestRequest")));
            Assert.True(provider.Stop());
        }
    }
}
