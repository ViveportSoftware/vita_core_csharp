using System;
using System.IO;
using System.Runtime.InteropServices;
using Htc.Vita.Core.Interop;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Diagnostics
{
    public partial class FilePropertiesInfo
    {
        internal static class Authenticode
        {
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
