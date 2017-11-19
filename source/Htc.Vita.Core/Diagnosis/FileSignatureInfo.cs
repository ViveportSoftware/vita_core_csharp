using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using Htc.Vita.Core.Interop;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Diagnosis
{
    public class FileSignatureInfo
    {
        private static readonly Logger Log = Logger.GetInstance(typeof(FileSignatureInfo));

        private readonly X509Certificate _certificate;

        public string IssuerDistinguishedName { get; }
        public string IssuerName { get; }
        public string PublicKey { get; }
        public string SubjectDistinguishedName { get; }
        public string SubjectName { get; }
        public bool Verified { get; }

        private FileSignatureInfo(FileInfo fileInfo)
        {
            if (fileInfo == null)
            {
                return;
            }
            if (!fileInfo.Exists)
            {
                Log.Warn("Can not find " + fileInfo.FullName + " to get certificate");
                return;
            }
            try
            {
                _certificate = X509Certificate.CreateFromSignedFile(fileInfo.FullName);
            }
            catch (Exception)
            {
                Log.Warn("Can not find certificate from file " + fileInfo.FullName);
            }
            if (_certificate != null)
            {
                IssuerDistinguishedName = _certificate.Issuer;
                IssuerName = DistinguishedName.Parse(IssuerDistinguishedName).O;
                SubjectDistinguishedName = _certificate.Subject;
                SubjectName = DistinguishedName.Parse(SubjectDistinguishedName).O;
                PublicKey = _certificate.GetPublicKeyString();
                Verified = Authenticode.IsVerified(fileInfo);
            }
        }

        public static FileSignatureInfo GetSignatureInfo(FileInfo fileInfo)
        {
            return new FileSignatureInfo(fileInfo);
        }

        internal class Authenticode
        {
            public static bool IsVerified(FileInfo fileInfo)
            {
                if (fileInfo == null || !fileInfo.Exists)
                {
                    return false;
                }

                var winTrustFileInfo = new Windows.WinTrustFileInfo
                {
                        cbStruct = (uint)Marshal.SizeOf(typeof(Windows.WinTrustFileInfo)),
                        pcwszFilePath = fileInfo.FullName,
                        hFile = IntPtr.Zero,
                        pgKnownSubject = IntPtr.Zero
                };
                var winTrustFileInfoPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(Windows.WinTrustFileInfo)));
                Marshal.StructureToPtr(winTrustFileInfo, winTrustFileInfoPtr, false);

                var infoUnionChoice = new Windows.WinTrustDataUnionChoice
                {
                        pFile = winTrustFileInfoPtr
                };

                var wintrustData = new Windows.WinTrustData
                {
                        cbStruct = (uint)Marshal.SizeOf(typeof(Windows.WinTrustData)),
                        pPolicyCallbackData = IntPtr.Zero,
                        pSIPCallbackData = IntPtr.Zero,
                        dwUIChoice = Windows.WinTrustDataUi.None,
                        fdwRevocationChecks = Windows.WinTrustDataRevoke.None,
                        dwUnionChoice = Windows.WinTrustDataChoice.File,
                        infoUnion = infoUnionChoice,
                        dwStateAction = Windows.WinTrustDataStateAction.Ignore,
                        hWVTStateData = IntPtr.Zero,
                        pwszURLReference = IntPtr.Zero,
                        dwProvFlags = Windows.WinTrustDataProviderFlag.SaferFlag,
                        dwUIContext = Windows.WinTrustDataUiContext.Execute
                };
                var winTrustDataPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(Windows.WinTrustData)));
                Marshal.StructureToPtr(wintrustData, winTrustDataPtr, false);

                var result = Windows.WinVerifyTrust(
                        IntPtr.Zero,
                        ref Windows.WinTrustActionGenericVerifyV2,
                        winTrustDataPtr
                );

                var success = result == 0;
                if (!success)
                {
                    if (result == Windows.TRUST_E_PROVIDER_UNKNOWN)
                    {
                        Log.Error("WinVerifyTrust result: TRUST_E_PROVIDER_UNKNOWN");
                    }
                    else if (result == Windows.TRUST_E_ACTION_UNKNOWN)
                    {
                        Log.Error("WinVerifyTrust result: TRUST_E_ACTION_UNKNOWN");
                    }
                    else if (result == Windows.TRUST_E_SUBJECT_FORM_UNKNOWN)
                    {
                        Log.Error("WinVerifyTrust result: TRUST_E_SUBJECT_FORM_UNKNOWN");
                    }
                    else if (result == Windows.TRUST_E_SUBJECT_NOT_TRUSTED)
                    {
                        Log.Warn("Can not trust " + fileInfo.FullName);
                    }
                    else
                    {
                        Log.Error("WinVerifyTrust result: 0x" + result.ToString("X"));
                    }
                }

                Marshal.DestroyStructure(winTrustDataPtr, typeof(Windows.WinTrustData));
                Marshal.FreeHGlobal(winTrustDataPtr);
                Marshal.DestroyStructure(winTrustFileInfoPtr, typeof(Windows.WinTrustFileInfo));
                Marshal.FreeHGlobal(winTrustFileInfoPtr);

                return success;
            }
        }

        internal class DistinguishedName
        {
            private readonly List<KeyValuePair<string, string>> _pairs = new List<KeyValuePair<string, string>>();

            public string O
            {
                get
                {
                    foreach (var pair in _pairs)
                    {
                        if ("O".Equals(pair.Key))
                        {
                            return pair.Value;
                        }
                    }
                    return "";
                }
            }

            public DistinguishedName(string content)
            {
                content = !string.IsNullOrWhiteSpace(content) ? content.Trim() : string.Empty;

                while (true)
                {
                    var equalIndex = content.IndexOf("=", StringComparison.Ordinal);
                    if (equalIndex <= 0)
                    {
                        break;
                    }

                    var escapedIndex = content.IndexOf("\\", StringComparison.Ordinal);
                    if (escapedIndex >= 0 && escapedIndex < equalIndex)
                    {
                        break;
                    }

                    var commaIndex = content.IndexOf(",", StringComparison.Ordinal);
                    if (commaIndex >= 0 && commaIndex < equalIndex)
                    {
                        break;
                    }

                    var key = content.Substring(0, equalIndex - 0).Trim();
                    string value;
                    if (commaIndex == escapedIndex + 1)
                    {
                        commaIndex = content.IndexOf(",", commaIndex + 1, StringComparison.Ordinal);
                    }
                    if (commaIndex < 0)
                    {
                        value = content.Substring(
                                Math.Min(
                                        equalIndex + 1,
                                        content.Length
                                )
                        ).Replace("\\", "");
                        _pairs.Add(new KeyValuePair<string, string>(key, value));
                        break;
                    }

                    value = content.Substring(
                            Math.Min(
                                    equalIndex + 1,
                                    content.Length
                            ),
                            commaIndex - equalIndex - 1
                    ).Replace("\\", "");
                    content = content.Substring(commaIndex + 1);
                    _pairs.Add(new KeyValuePair<string, string>(key, value));
                }
            }

            public static DistinguishedName Parse(string distinguishedName)
            {
                if (string.IsNullOrWhiteSpace(distinguishedName))
                {
                    return null;
                }
                return new DistinguishedName(distinguishedName);
            }
        }
    }
}
