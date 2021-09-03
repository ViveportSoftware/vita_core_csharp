using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Htc.Vita.Core.Log;
using Htc.Vita.Core.Net;
using Htc.Vita.Core.Runtime;
using Htc.Vita.Core.Util;
using Convert = Htc.Vita.Core.Util.Convert;

namespace Htc.Vita.Core.Auth
{
    /// <summary>
    /// Class DefaultOAuth2.
    /// </summary>
    public static class DefaultOAuth2
    {
        /// <summary>
        /// Class DefaultAuthorizationCodeReceiver.
        /// Implements the <see cref="OAuth2.AuthorizationCodeReceiver" />
        /// </summary>
        /// <seealso cref="OAuth2.AuthorizationCodeReceiver" />
        public class DefaultAuthorizationCodeReceiver : OAuth2.AuthorizationCodeReceiver
        {
            private const string DefaultEncodedFavIcon = ""
                    + "AAABAAEAEBAAAAEAIABoBAAAFgAAACgAAAAQAAAAIAAAAAEAIAAAAAAAAAQAAAAA"
                    + "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAANVZyMAAAAAAAAAAAAAAAAAAA"
                    + "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAANVZyMDVW"
                    + "cv82ZH/ENVZyEAAAAAAAAAAAAAAAAAAAAAAAAAAANVZyQDVWchAAAAAAAAAAAAAA"
                    + "AAAAAAAAAAAAAAAAAAA1VnL/SrLK/zVadvg2ZoJbAAAAADVWciA2ZYCQNV978DVW"
                    + "cv81VnJAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAANVZy/13j+f9W2vD/PXaR/TVh"
                    + "fOI0Xnr/LavG/yXM6P8zZYH/NVZyEAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAADVW"
                    + "cv9d4/n/XeP5/1vi+P9Atcz/LdDq/yfQ6/8nyuX/NmWA2AAAAAAAAAAAAAAAAAAA"
                    + "AAAAAAAAAAAAAAAAAAA1Wnb4Wd/1/13j+f9d4/n/P93z/zvc8v8v1e7/LLDL/zVg"
                    + "fHkAAAAAAAAAAAAAAAAAAAAAAAAAADVWchA1YXzQSaC5/Xrm+/9x5fv/X+P5/z/d"
                    + "8/873PL/L9Xu/zF/m/w1VnJAAAAAAAAAAAAAAAAAAAAAADVWcjA1XnruWMbd/3/n"
                    + "/P+B5/z/gef8/3rm+/9M4PX/O9zy/y/V7v8rt9L/NWF84jVWchAAAAAAAAAAADZm"
                    + "gls1W3f6T8ff/3jm+/+B5/z/hOf8/4Tn/P+B5/z/dOX6/zvc8v862/H/K9Ls/y+i"
                    + "vf81YXy/AAAAAAAAAAA2a4ZMNmWA2Dhfev9Ckqv7UMLZ/3Dh9v+c6/z/i+j8/4Dn"
                    + "+/9E3vT/O9zy/zvc8v8x0er/MYai/TZifp4AAAAAAAAAAAAAAAAAAAAANVZyQDZl"
                    + "gJA4Y374guj7/47o/P945vv/SLDI/zZwi+81XHj7NVZy/zVWcv81VnL/NVZyEAAA"
                    + "AAAAAAAAAAAAAAAAAAAAAAAANmJ+nlzH3f+L6fz/cuP5/zhjfvg1VnJANVZyIAAA"
                    + "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAADVWcjA/fpn9b+X7/1CZ"
                    + "sv41VnJQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA"
                    + "AAAAAAAANWF84kqpwv82ZH/EAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA"
                    + "AAAAAAAAAAAAAAAAAAAAAAAAAAAAADVgfHk1W3f5NVZyIAAAAAAAAAAAAAAAAAAA"
                    + "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAANVZyMAAA"
                    + "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA//8AAPP/AADxxwAA8AcAAPAH"
                    + "AADwDwAA4A8AAMAHAACAAwAAgAEAAPABAAD4PwAA/H8AAPx/AAD+/wAA//8AAA==";
            private const string KeyContentType = "Content-Type";

            private static string _messageAuthorizationDone = string.Empty;
            private static string _messageAuthorizationFailed = string.Empty;

            private readonly CountdownEvent _countdownEvent = new CountdownEvent(1);
            private readonly bool _isHttpListenerSupported;

            private string _authorizationCode;
            private CancellationToken _cancellationToken;
            private HttpListener _httpListener;
            private bool _shouldKeepListening;

            /// <summary>
            /// Gets or sets the redirect URI.
            /// </summary>
            /// <value>The redirect URI.</value>
            protected Uri RedirectUri { get; set; }

            /// <summary>
            /// Gets or sets the message when authorize done.
            /// </summary>
            /// <value>The message when authorize done.</value>
            public static string MessageAuthorizeDone
            {
                get
                {
                    return _messageAuthorizationDone;
                }
                set
                {
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        return;
                    }
                    _messageAuthorizationDone = value;
                }
            }
            /// <summary>
            /// Gets or sets the message when authorize failed.
            /// </summary>
            /// <value>The message when authorize failed.</value>
            public static string MessageAuthorizeFailed
            {
                get
                {
                    return _messageAuthorizationFailed;
                }
                set
                {
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        return;
                    }
                    _messageAuthorizationFailed = value;
                }
            }
            /// <summary>
            /// Gets or sets the encoded favorite icon.
            /// </summary>
            /// <value>The encoded favorite icon.</value>
            public static string EncodedFavIcon { get; set; } = DefaultEncodedFavIcon;

            /// <summary>
            /// Initializes a new instance of the <see cref="DefaultAuthorizationCodeReceiver" /> class.
            /// </summary>
            public DefaultAuthorizationCodeReceiver()
            {
                _isHttpListenerSupported = HttpListener.IsSupported;
                _shouldKeepListening = true;
            }

            private void ConnectSelfToBreakConnectionWaiting()
            {
                if (RedirectUri == null)
                {
                    return;
                }

                Task.Run(() =>
                {
                        WaitHandle.WaitAny(new[]
                        {
                                _countdownEvent.WaitHandle,
                                _cancellationToken.WaitHandle
                        });
                        if (!_cancellationToken.IsCancellationRequested)
                        {
                            return;
                        }

                        using (WebRequestFactory.GetInstance()
                                .GetHttpWebRequest(RedirectUri)
                                .GetResponse())
                        {
                            // Skip
                        }
                }, CancellationToken.None);
            }

            /// <inheritdoc />
            protected override void Dispose(bool disposing)
            {
                if (!Disposed && disposing)
                {
                    _httpListener?.Close();
                    _httpListener = null;
                    _countdownEvent.Dispose();
                }

                base.Dispose(disposing);
            }

            private static void HandleAuthorizationCodeRequest(HttpListenerContext context)
            {
                if (context == null)
                {
                    return;
                }

                var data = Encoding.UTF8.GetBytes(MessageAuthorizeDone);
                var response = context.Response;
                response.Headers.Add(KeyContentType, GetContentType(MessageAuthorizeDone).GetDescription());
                response.ContentLength64 = data.Length;
                response.StatusCode = (int)HttpStatusCode.OK;
                try
                {
                    using (var output = response.OutputStream)
                    {
                        output.Write(data, 0, data.Length);
                        output.Flush();
                    }
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(DefaultAuthorizationCodeReceiver)).Error(e.ToString());
                }
            }

            private static Mime.ContentType GetContentType(string data)
            {
                if (string.IsNullOrWhiteSpace(data))
                {
                    return Mime.ContentType.Text_Plain;
                }

                if (data.StartsWith("<"))
                {
                    return Mime.ContentType.Text_Html;
                }

                return Mime.ContentType.Text_Plain;
            }

            private static void HandleBadRequest(HttpListenerContext context)
            {
                if (context == null)
                {
                    return;
                }

                var data = Encoding.UTF8.GetBytes(MessageAuthorizeFailed);
                var response = context.Response;
                response.Headers.Add(KeyContentType, GetContentType(MessageAuthorizeFailed).GetDescription());
                response.ContentLength64 = data.Length;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                try
                {
                    using (var output = response.OutputStream)
                    {
                        output.Write(data, 0, data.Length);
                        output.Flush();
                    }
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(DefaultAuthorizationCodeReceiver)).Error(e.ToString());
                }
            }

            private static void HandleFaviconRequest(HttpListenerContext context)
            {
                if (context == null)
                {
                    return;
                }

                var data = Convert.FromBase64String(EncodedFavIcon)
                        ?? Convert.FromBase64String(DefaultEncodedFavIcon);
                var response = context.Response;
                response.Headers.Add(KeyContentType, Mime.ContentType.Image_VndMicrosoftIcon.GetDescription());
                response.ContentLength64 = data.Length;
                response.StatusCode = (int)HttpStatusCode.OK;
                try
                {
                    using (var output = response.OutputStream)
                    {
                        output.Write(data, 0, data.Length);
                        output.Flush();
                    }
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(DefaultAuthorizationCodeReceiver)).Error(e.ToString());
                }
            }

            private void HandleHttpRequest(object o)
            {
                var httpListenerContext = o as HttpListenerContext;
                if (httpListenerContext == null)
                {
                    return;
                }

                if (_cancellationToken.IsCancellationRequested)
                {
                    _countdownEvent.Signal();
                }

                var request = httpListenerContext.Request;
                var rawUrl = request?.RawUrl;
                if (!string.IsNullOrWhiteSpace(rawUrl) && rawUrl.StartsWith("/favicon.ico"))
                {
                    HandleFaviconRequest(httpListenerContext);
                    return;
                }

                var parameters = request?.QueryString;
                if (parameters == null || !parameters.HasKeys())
                {
                    Logger.GetInstance(typeof(DefaultAuthorizationCodeReceiver)).Info($"Can not find query string in URL: {rawUrl}");
                    HandleBadRequest(httpListenerContext);
                    return;
                }

                var code = parameters[OAuth2.Key.Code.GetDescription()];
                if (string.IsNullOrWhiteSpace(code))
                {
                    Logger.GetInstance(typeof(DefaultAuthorizationCodeReceiver)).Info($"Can not find authorization code in URL: {rawUrl}");
                    HandleBadRequest(httpListenerContext);
                    return;
                }

                _authorizationCode = code;
                HandleAuthorizationCodeRequest(httpListenerContext);

                // for simultaneous favicon access
                SpinWait.SpinUntil(() => false, TimeSpan.FromMilliseconds(200));

                _shouldKeepListening = false;
                _countdownEvent.Signal();
            }

            /// <inheritdoc />
            protected override OAuth2.AuthorizationCodeReceiver OnInitialize(
                    Dictionary<string, object> options,
                    CancellationToken cancellationToken)
            {
                _cancellationToken = cancellationToken;

                if (!_isHttpListenerSupported)
                {
                    Logger.GetInstance(typeof(DefaultAuthorizationCodeReceiver)).Error($"{nameof(HttpListener)} is not supported.");
                    return this;
                }

                RedirectUri = options.ParseUri(OptionRedirectUri);
                if (RedirectUri == null)
                {
                    Logger.GetInstance(typeof(DefaultAuthorizationCodeReceiver)).Error("Redirect URI is invalid");
                    return this;
                }

                try
                {
                    _httpListener = new HttpListener();
                    _httpListener.Prefixes.Add(RedirectUri.ToString());
                    _httpListener.Start();
                    Task.Run(() =>
                    {
                            while (_shouldKeepListening && !cancellationToken.IsCancellationRequested)
                            {
                                try
                                {
                                    ThreadPool.QueueUserWorkItem(
                                            HandleHttpRequest,
                                            _httpListener.GetContext()
                                    );
                                }
                                catch (HttpListenerException e)
                                {
                                    if (e.ErrorCode == 995)
                                    {
                                        Logger.GetInstance(typeof(DefaultAuthorizationCodeReceiver)).Debug(e.Message);
                                    }
                                    else if (e.ErrorCode == 500 && Platform.IsMono)
                                    {
                                        Logger.GetInstance(typeof(DefaultAuthorizationCodeReceiver)).Debug(e.Message);
                                    }
                                    else
                                    {
                                        Logger.GetInstance(typeof(DefaultAuthorizationCodeReceiver)).Error($"{e.Message}, error code: {e.ErrorCode}");
                                    }
                                }
                                catch (Exception e)
                                {
                                    Logger.GetInstance(typeof(DefaultAuthorizationCodeReceiver)).Error(e.ToString());
                                }
                            }
                    }, cancellationToken);
                    ConnectSelfToBreakConnectionWaiting();
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(DefaultAuthorizationCodeReceiver)).Error(e.ToString());
                    _httpListener?.Close();
                    _httpListener = null;
                }

                return this;
            }

            /// <inheritdoc />
            protected override ReceiveResult OnReceive()
            {
                if (!_isHttpListenerSupported)
                {
                    return new ReceiveResult
                    {
                            Status = ReceiveStatus.UnsupportedReceiver
                    };
                }

                if (RedirectUri == null)
                {
                    return new ReceiveResult
                    {
                            Status = ReceiveStatus.UnsupportedReceiver
                    };
                }

                try
                {
                    _countdownEvent.Wait(_cancellationToken);
                }
                catch (OperationCanceledException)
                {
                    return new ReceiveResult
                    {
                            Status = ReceiveStatus.CancelledOperation
                    };
                }

                if (string.IsNullOrWhiteSpace(_authorizationCode))
                {
                    return new ReceiveResult
                    {
                            Status = ReceiveStatus.InvalidAuthorizationCode
                    };
                }

                return new ReceiveResult
                {
                        Code = _authorizationCode,
                        Status = ReceiveStatus.Ok
                };
            }
        }

        /// <summary>
        /// Class DefaultAuthorizationCodeUserAgent.
        /// Implements the <see cref="OAuth2.AuthorizationCodeUserAgent" />
        /// </summary>
        /// <seealso cref="OAuth2.AuthorizationCodeUserAgent" />
        public class DefaultAuthorizationCodeUserAgent : OAuth2.AuthorizationCodeUserAgent
        {
            private readonly object _lock = new object();

            private CancellationToken _cancellationToken;

            /// <summary>
            /// Gets or sets the authorization URL.
            /// </summary>
            /// <value>The authorization URL.</value>
            protected Uri AuthorizationUrl { get; set; }

            /// <inheritdoc />
            protected override OAuth2.AuthorizationCodeUserAgent OnInitialize(
                    Dictionary<string, object> options,
                    CancellationToken cancellationToken)
            {
                lock (_lock)
                {

                    AuthorizationUrl = options.ParseUri(OptionAuthorizationUrl);
                    _cancellationToken = cancellationToken;
                    try
                    {
                        OnOverrideInitialize(
                                options,
                                cancellationToken
                        );
                    }
                    catch (Exception e)
                    {
                        Logger.GetInstance(typeof(DefaultAuthorizationCodeUserAgent)).Error(e.ToString());
                    }
                }
                return this;
            }

            /// <inheritdoc />
            protected override LaunchResult OnLaunch()
            {
                lock (_lock)
                {
                    if (AuthorizationUrl == null)
                    {
                        return new LaunchResult
                        {
                                Status = LaunchStatus.InvalidAuthorizationUri
                        };
                    }

                    if (_cancellationToken.IsCancellationRequested)
                    {
                        return new LaunchResult
                        {
                                Status = LaunchStatus.CancelledOperation
                        };
                    }

                    var scheme = AuthorizationUrl.Scheme;
                    if (!"http".Equals(scheme, StringComparison.InvariantCultureIgnoreCase)
                            && !"https".Equals(scheme, StringComparison.InvariantCultureIgnoreCase))
                    {
                        return new LaunchResult
                        {
                                Status = LaunchStatus.InvalidAuthorizationUri
                        };
                    }

                    try
                    {
                        if (OnOverrideLaunch())
                        {
                            return new LaunchResult
                            {
                                    Status = LaunchStatus.Ok
                            };
                        }
                    }
                    catch (Exception e)
                    {
                        Logger.GetInstance(typeof(DefaultAuthorizationCodeUserAgent)).Error(e.ToString());
                    }

                    return new LaunchResult
                    {
                            Status = LaunchStatus.UnsupportedUserAgent
                    };
                }
            }

            /// <summary>
            /// Called when overriding initializing.
            /// </summary>
            /// <param name="options">The options.</param>
            /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
            /// <returns><c>true</c> if initializing successfully, <c>false</c> otherwise.</returns>
            protected virtual bool OnOverrideInitialize(
                    Dictionary<string, object> options,
                    CancellationToken cancellationToken)
            {
                return true;
            }

            /// <summary>
            /// Called when overriding launching.
            /// </summary>
            /// <returns><c>true</c> if launching successfully, <c>false</c> otherwise.</returns>
            protected virtual bool OnOverrideLaunch()
            {
                if (!Platform.IsWindows)
                {
                    return false;
                }

                var authorizationUrl = AuthorizationUrl.AbsoluteUri;
                var processStartInfo = new ProcessStartInfo
                {
                        Arguments = $"url.dll,FileProtocolHandler \"{authorizationUrl}\"",
                        CreateNoWindow = true,
                        FileName = "C:\\Windows\\System32\\rundll32.exe"
                };
                try
                {
                    using (Process.Start(processStartInfo))
                    {
                        // Skip
                    }
                    return true;
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(DefaultAuthorizationCodeUserAgent)).Error($"Can not launch URL: \"{authorizationUrl}\", message: {e.Message}");
                }

                return false;
            }
        }

        /// <summary>
        /// Class DefaultClientAssistantFactory.
        /// Implements the <see cref="OAuth2.ClientAssistantFactory" />
        /// </summary>
        /// <seealso cref="OAuth2.ClientAssistantFactory" />
        public class DefaultClientAssistantFactory : OAuth2.ClientAssistantFactory
        {
            /// <inheritdoc />
            protected override OAuth2.AuthorizationCodeReceiver OnGetAuthorizationCodeReceiver(
                    Dictionary<string, object> options,
                    CancellationToken cancellationToken)
            {
                return new DefaultAuthorizationCodeReceiver().Initialize(
                        options,
                        cancellationToken
                );
            }

            /// <inheritdoc />
            protected override OAuth2.AuthorizationCodeUserAgent OnGetAuthorizationCodeUserAgent(
                    Dictionary<string, object> options,
                    CancellationToken cancellationToken)
            {
                return new DefaultAuthorizationCodeUserAgent().Initialize(
                        options,
                        cancellationToken
                );
            }
        }
    }
}
