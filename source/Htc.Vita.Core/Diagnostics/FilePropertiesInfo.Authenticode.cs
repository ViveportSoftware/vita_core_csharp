using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography.Pkcs;
using Htc.Vita.Core.Interop;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Diagnostics
{
    public partial class FilePropertiesInfo
    {
        internal static class Authenticode
        {
            private const string OidRsaCounterSignature = "1.2.840.113549.1.9.6";
            private const string OidRsaSigningTime = "1.2.840.113549.1.9.5";

            internal static List<DateTime> GetTimestampList(FileInfo fileInfo)
            {
                var result = new List<DateTime>();
                if (fileInfo == null || !fileInfo.Exists)
                {
                    return result;
                }

                Windows.SafeCertContextHandle certContext = null;
                Windows.SafeCertStoreHandle certStore = null;
                Windows.SafeCryptMsgHandle cryptMsg = null;
                byte[] encodedMessage = null;
                try
                {
                    Windows.CertEncoding certEncoding;
                    Windows.CertQueryContent certQueryContent;
                    Windows.CertQueryFormat certQueryFormat;
                    var success = Windows.CryptQueryObject(
                            Windows.CertQueryObject.File,
                            Marshal.StringToHGlobalUni(fileInfo.FullName),
                            Windows.CertQueryContentFlag.All,
                            Windows.CertQueryFormatFlag.All,
                            0,
                            out certEncoding,
                            out certQueryContent,
                            out certQueryFormat,
                            out certStore,
                            out cryptMsg,
                            out certContext
                    );
                    if (!success)
                    {
                        Logger.GetInstance(typeof(Authenticode)).Error($"Can not query crypt object for {fileInfo.FullName}, error code: {Marshal.GetLastWin32Error()}");
                        return result;
                    }

                    var cbData = 0;
                    success = Windows.CryptMsgGetParam(
                            cryptMsg,
                            Windows.CertMessageParameterType.EncodedMessage,
                            0,
                            null,
                            ref cbData
                    );
                    if (!success)
                    {
                        Logger.GetInstance(typeof(Authenticode)).Error($"Can not get crypt message parameter size, error code: {Marshal.GetLastWin32Error()}");
                        return result;
                    }

                    encodedMessage = new byte[cbData];
                    success = Windows.CryptMsgGetParam(
                            cryptMsg,
                            Windows.CertMessageParameterType.EncodedMessage,
                            0,
                            encodedMessage,
                            ref cbData
                    );
                    if (!success)
                    {
                        Logger.GetInstance(typeof(Authenticode)).Error($"Can not get crypt message parameter, error code: {Marshal.GetLastWin32Error()}");
                        return result;
                    }
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(Authenticode)).Error($"Can not extract encoded message. error: {e.Message}");
                }
                finally
                {
                    certContext?.Dispose();
                    certStore?.Dispose();
                    cryptMsg?.Dispose();
                }

                if (encodedMessage == null)
                {
                    Logger.GetInstance(typeof(Authenticode)).Error("Can not find available encoded message.");
                    return result;
                }

                try
                {
                    var signedCms = new SignedCms();
                    signedCms.Decode(encodedMessage);
                    foreach (var signerInfo in signedCms.SignerInfos)
                    {
                        if (signerInfo == null)
                        {
                            continue;
                        }

                        foreach (var unsignedAttribute in signerInfo.UnsignedAttributes)
                        {
                            if (unsignedAttribute == null)
                            {
                                continue;
                            }

                            if (!OidRsaCounterSignature.Equals(unsignedAttribute.Oid.Value))
                            {
                                continue;
                            }

                            foreach (var counterSignerInfo in signerInfo.CounterSignerInfos)
                            {
                                if (counterSignerInfo == null)
                                {
                                    continue;
                                }

                                foreach (var signedAttribute in counterSignerInfo.SignedAttributes)
                                {
                                    if (!OidRsaSigningTime.Equals(signedAttribute.Oid.Value))
                                    {
                                        continue;
                                    }

                                    foreach (var value in signedAttribute.Values)
                                    {
                                        var pkcs9SigningTime = value as Pkcs9SigningTime;
                                        if (pkcs9SigningTime == null)
                                        {
                                            continue;
                                        }

                                        result.Add(pkcs9SigningTime.SigningTime);
                                    }
                                }
                            }
                        }
                    }

                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(Authenticode)).Error($"Can not parse timestamp: {e.Message}");
                }

                return result;
            }

            internal static bool IsVerified(FileInfo fileInfo)
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

                var winTrustData = new Windows.WinTrustData
                {
                        cbStruct = (uint)Marshal.SizeOf(typeof(Windows.WinTrustData)),
                        pPolicyCallbackData = IntPtr.Zero,
                        pSIPCallbackData = IntPtr.Zero,
                        dwUIChoice = Windows.WinTrustDataUI.None,
                        fdwRevocationChecks = Windows.WinTrustDataRevoke.None,
                        dwUnionChoice = Windows.WinTrustDataChoice.File,
                        infoUnion = infoUnionChoice,
                        dwStateAction = Windows.WinTrustDataStateAction.Ignore,
                        hWVTStateData = IntPtr.Zero,
                        pwszURLReference = IntPtr.Zero,
                        dwProvFlags = Windows.WinTrustDataProviderFlag.SaferFlag,
                        dwUIContext = Windows.WinTrustDataUIContext.Execute,
                        pSignatureSettings = IntPtr.Zero
                };
                var winTrustDataPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(Windows.WinTrustData)));
                Marshal.StructureToPtr(winTrustData, winTrustDataPtr, false);

                var actionId = Guid.Parse(Windows.WinTrustActionGenericVerifyV2.ToString("D"));
                var result = Windows.WinVerifyTrust(
                        IntPtr.Zero,
                        ref actionId,
                        winTrustDataPtr
                );

                var success = result == 0;
                if (!success)
                {
                    if (result == (uint)Windows.TrustError.ProviderUnknown)
                    {
                        Logger.GetInstance(typeof(FilePropertiesInfo)).Error("WinVerifyTrust result: TRUST_E_PROVIDER_UNKNOWN");
                    }
                    else if (result == (uint)Windows.TrustError.ActionUnknown)
                    {
                        Logger.GetInstance(typeof(FilePropertiesInfo)).Error("WinVerifyTrust result: TRUST_E_ACTION_UNKNOWN");
                    }
                    else if (result == (uint)Windows.TrustError.SubjectFormUnknown)
                    {
                        Logger.GetInstance(typeof(FilePropertiesInfo)).Error("WinVerifyTrust result: TRUST_E_SUBJECT_FORM_UNKNOWN");
                    }
                    else if (result == (uint)Windows.TrustError.SubjectNotTrusted)
                    {
                        Logger.GetInstance(typeof(FilePropertiesInfo)).Warn("Can not trust " + fileInfo.FullName);
                    }
                    else
                    {
                        Logger.GetInstance(typeof(FilePropertiesInfo)).Error("WinVerifyTrust result: 0x" + result.ToString("X"));
                    }
                }

                Marshal.DestroyStructure(winTrustDataPtr, typeof(Windows.WinTrustData));
                Marshal.FreeHGlobal(winTrustDataPtr);
                Marshal.DestroyStructure(winTrustFileInfoPtr, typeof(Windows.WinTrustFileInfo));
                Marshal.FreeHGlobal(winTrustFileInfoPtr);

                return success;
            }
        }
    }
}
