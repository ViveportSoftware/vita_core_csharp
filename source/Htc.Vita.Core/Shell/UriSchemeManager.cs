using System;
using System.Collections.Generic;
using System.Linq;
using Htc.Vita.Core.Log;
using Htc.Vita.Core.Util;

namespace Htc.Vita.Core.Shell
{
    /// <summary>
    /// Class UriSchemeManager.
    /// </summary>
    public abstract partial class UriSchemeManager
    {
        /// <summary>
        /// The option is used to accept whitelist only
        /// </summary>
        public static readonly string OptionAcceptWhitelistOnly = "option_accept_whitelist_only";

        static UriSchemeManager()
        {
            TypeRegistry.RegisterDefault<UriSchemeManager, RegistryUriSchemeManager>();
        }

        /// <summary>
        /// Registers the instance type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void Register<T>()
                where T : UriSchemeManager, new()
        {
            TypeRegistry.Register<UriSchemeManager, T>();
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <returns>UriSchemeManager.</returns>
        public static UriSchemeManager GetInstance()
        {
            return TypeRegistry.GetInstance<UriSchemeManager>();
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>UriSchemeManager.</returns>
        public static UriSchemeManager GetInstance<T>()
                where T : UriSchemeManager, new()
        {
            return TypeRegistry.GetInstance<UriSchemeManager, T>();
        }

        /// <summary>
        /// Gets the system URI scheme.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>UriSchemeInfo.</returns>
        public UriSchemeInfo GetSystemUriScheme(string name)
        {
            return GetSystemUriScheme(
                    name,
                    null
            );
        }

        /// <summary>
        /// Gets the system URI scheme.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="options">The options.</param>
        /// <returns>UriSchemeInfo.</returns>
        public UriSchemeInfo GetSystemUriScheme(
                string name,
                Dictionary<string, string> options)
        {
            if (!name.All(c => char.IsLetterOrDigit(c) || c == '+' || c == '-' || c == '.'))
            {
                Logger.GetInstance(typeof(UriSchemeManager)).Error($"Do not find valid scheme name: \"{name}\"");
                return new UriSchemeInfo
                {
                        Name = name
                };
            }

            var opts = options ?? new Dictionary<string, string>();
            UriSchemeInfo result = null;
            try
            {
                result = OnGetSystemUriScheme(
                        name,
                        opts
                );
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(UriSchemeManager)).Error($"Can not get system uri scheme: {e.Message}");
            }

            return result ?? new UriSchemeInfo
            {
                Name = name
            };
        }

        /// <summary>
        /// Determines whether system URI scheme is valid with the specified name .
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns><c>true</c> if system URI scheme is valid with the specified name; otherwise, <c>false</c>.</returns>
        public bool IsSystemUriSchemeValid(string name)
        {
            return IsSystemUriSchemeValid(
                    name,
                    null
            );
        }

        /// <summary>
        /// Determines whether system URI scheme is valid with the specified name .
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="options">The options.</param>
        /// <returns><c>true</c> if system URI scheme is valid with the specified name; otherwise, <c>false</c>.</returns>
        public bool IsSystemUriSchemeValid(
                string name,
                Dictionary<string, string> options)
        {
            var uriSchemeInfo = GetSystemUriScheme(
                    name,
                    options
            );
            if (uriSchemeInfo == null)
            {
                return false;
            }

            return IsSystemUriSchemeValid(uriSchemeInfo);
        }

        /// <summary>
        /// Determines whether the system URI scheme is valid.
        /// </summary>
        /// <param name="uriSchemeInfo">The URI scheme information.</param>
        /// <returns><c>true</c> if system URI scheme is valid; otherwise, <c>false</c>.</returns>
        public bool IsSystemUriSchemeValid(UriSchemeInfo uriSchemeInfo)
        {
            if (uriSchemeInfo == null)
            {
                return false;
            }

            return !string.IsNullOrWhiteSpace(uriSchemeInfo.Name)
                    && !string.IsNullOrWhiteSpace(uriSchemeInfo.CommandPath)
                    && !string.IsNullOrWhiteSpace(uriSchemeInfo.DefaultIcon);
        }

        /// <summary>
        /// Called when getting system URI scheme.
        /// </summary>
        /// <param name="schemeName">The scheme name.</param>
        /// <param name="options">The options.</param>
        /// <returns>UriSchemeInfo.</returns>
        protected abstract UriSchemeInfo OnGetSystemUriScheme(
                string schemeName,
                Dictionary<string, string> options
        );
    }
}
