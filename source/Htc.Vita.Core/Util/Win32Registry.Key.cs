using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using Htc.Vita.Core.Interop;

namespace Htc.Vita.Core.Util
{
    public static partial class Win32Registry
    {
        /// <summary>
        /// Class Key.
        /// Implements the <see cref="IDisposable" />
        /// </summary>
        /// <seealso cref="IDisposable" />
        public class Key : IDisposable
        {
            private const int MaxKeyLength = 255;
            private const int MaxValueNameLength = 16383;

            private static readonly Dictionary<Hive, string> RegistryHiveWithName = new Dictionary<Hive, string>
            {
                    { Hive.ClassesRoot, "HKEY_CLASSES_ROOT" },
                    { Hive.CurrentUser, "HKEY_CURRENT_USER" },
                    { Hive.LocalMachine, "HKEY_LOCAL_MACHINE" },
                    { Hive.Users, "HKEY_USERS" },
                    { Hive.PerformanceData, "HKEY_PERFORMANCE_DATA" },
                    { Hive.CurrentConfig, "HKEY_CURRENT_CONFIG" }
            };

            private readonly KeyStateFlag _keyStateFlag;
            private readonly View _view;

            private volatile Windows.SafeRegistryHandle _handle;
            private volatile string _name;
            private volatile KeyPermissionCheck _keyPermissionCheck;

            /// <summary>
            /// Gets the subkey count.
            /// </summary>
            /// <value>The subkey count.</value>
            /// <exception cref="ObjectDisposedException">Cannot access a closed registry key.</exception>
            public int SubKeyCount
            {
                get
                {
                    if (_handle == null)
                    {
                        throw new ObjectDisposedException(_name, "Cannot access a closed registry key.");
                    }
                    return DoGetSubKeyCount();
                }
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="Key"/> class.
            /// </summary>
            /// <param name="registryHandle">The registry handle.</param>
            /// <param name="view">The view.</param>
            /// <param name="isWritable">Indicate if it is writable.</param>
            /// <param name="isSystemKey">Indicate if it is a system key.</param>
            private Key(
                    Windows.SafeRegistryHandle registryHandle,
                    View view,
                    bool isWritable,
                    bool isSystemKey)
            {
                _handle = registryHandle;
                _name = "";
                _view = view;

                if (isSystemKey)
                {
                    _keyStateFlag |= KeyStateFlag.SystemKey;
                }
                if (isWritable)
                {
                    _keyStateFlag |= KeyStateFlag.WriteAccess;
                }
            }

            /// <summary>
            /// Creates the subkey.
            /// </summary>
            /// <param name="subKeyName">The subkey name.</param>
            /// <returns>Key.</returns>
            public Key CreateSubKey(string subKeyName)
            {
                return CreateSubKey(
                        subKeyName,
                        _keyPermissionCheck
                );
            }

            /// <summary>
            /// Creates the subkey.
            /// </summary>
            /// <param name="subKeyName">The subkey name.</param>
            /// <param name="keyPermissionCheck">The key permission check.</param>
            /// <returns>Key.</returns>
            public Key CreateSubKey(
                    string subKeyName,
                    KeyPermissionCheck keyPermissionCheck)
            {
                return DoOpenSubKey(
                        subKeyName,
                        keyPermissionCheck,
                        keyPermissionCheck != KeyPermissionCheck.ReadSubTree,
                        false
                ) ?? DoCreateSubKey(
                        subKeyName,
                        keyPermissionCheck
                );
            }

            /// <summary>
            /// Deletes the subkey tree.
            /// </summary>
            /// <param name="subKeyName">The subkey name.</param>
            /// <param name="throwOnMissingSubKey">If set to <c>true</c>, throw on missing subkey.</param>
            /// <exception cref="ArgumentException">Cannot delete a registry hive's subtree.</exception>
            /// <exception cref="ArgumentException">Cannot delete a subkey tree because the subkey does not exist.</exception>
            /// <exception cref="ObjectDisposedException">Cannot access a closed registry key.</exception>
            /// <exception cref="UnauthorizedAccessException">Cannot write to the registry key.</exception>
            public void DeleteSubKeyTree(
                    string subKeyName,
                    bool throwOnMissingSubKey)
            {
                if (subKeyName.Length == 0 && IsSystemKey())
                {
                    throw new ArgumentException("Cannot delete a registry hive's subtree.");
                }
                if (_handle == null)
                {
                    throw new ObjectDisposedException(_name, "Cannot access a closed registry key.");
                }
                if (!IsWritable())
                {
                    throw new UnauthorizedAccessException("Cannot write to the registry key.");
                }

                var subKey = DoOpenSubKey(
                        subKeyName,
                        KeyPermissionCheck.ReadWriteSubTree,
                        true,
                        false
                );
                if (subKey != null)
                {
                    using (subKey)
                    {
                        var subSubKeyNames = subKey.GetSubKeyNames();
                        foreach (var subSubKeyName in subSubKeyNames)
                        {
                            subKey.DoDeleteSubKeyTree(subSubKeyName);
                        }
                    }

                    DoDeleteSubKey(subKeyName);
                }
                else if (throwOnMissingSubKey)
                {
                    throw new ArgumentException("Cannot delete a subkey tree because the subkey does not exist.");
                }
            }

            /// <summary>
            /// Deletes the value.
            /// </summary>
            /// <param name="valueName">The value name.</param>
            /// <param name="throwOnMissingValue">Tf set to <c>true</c>, throw on missing value.</param>
            public void DeleteValue(
                    string valueName,
                    bool throwOnMissingValue)
            {
                DoDeleteValue(
                        valueName,
                        throwOnMissingValue
                );
            }

            /// <summary>
            /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
            /// </summary>
            public void Dispose()
            {
                if (_handle != null && !IsSystemKey())
                {
                    try
                    {
                        _handle.Dispose();
                    }
                    catch (IOException)
                    {
                        // skip
                    }
                    finally
                    {
                        _handle = null;
                    }
                }
            }

            private Key DoCreateSubKey(
                    string subKeyName,
                    KeyPermissionCheck keyPermissionCheck)
            {
                if (_handle == null)
                {
                    throw new ObjectDisposedException(_name, "Cannot access a closed registry key.");
                }

                var normalizedSubKeyName = NormalizeSubKeyName(subKeyName);

                var securityAttributes = new Windows.SecurityAttributes();
                securityAttributes.nLength = Marshal.SizeOf(securityAttributes);
                int disposition;

                Windows.SafeRegistryHandle handle;
                var errorCode = Windows.RegCreateKeyExW(
                        _handle,
                        normalizedSubKeyName,
                        IntPtr.Zero,
                        null,
                        0,
                        ToRegistryKeyAccessRight(keyPermissionCheck != KeyPermissionCheck.ReadSubTree) | ToRegistryKeyAccessRight(_view),
                        ref securityAttributes,
                        out handle,
                        out disposition
                );

                if (errorCode == Windows.Error.Success && !handle.IsInvalid)
                {
                    return new Key(
                            handle,
                            _view,
                            (keyPermissionCheck != KeyPermissionCheck.ReadSubTree),
                            false
                    )
                    {
                        _name = (normalizedSubKeyName.Length == 0) ? _name : $"{_name}\\{normalizedSubKeyName}",
                        _keyPermissionCheck = keyPermissionCheck
                    };
                }

                if (errorCode != Windows.Error.Success)
                {
                    ThrowException(
                            errorCode,
                            $"{_name}\\{normalizedSubKeyName}"
                    );
                }

                return null;
            }

            private void DoDeleteSubKey(string subKeyName)
            {
                var errorCode = Windows.RegDeleteKeyExW(
                        _handle,
                        subKeyName,
                        ToRegistryKeyAccessRight(_view),
                        IntPtr.Zero
                );
                if (errorCode != Windows.Error.Success)
                {
                    ThrowException(
                            errorCode,
                            null
                    );
                }
            }

            private void DoDeleteSubKeyTree(string subKeyName)
            {
                var subKey = DoOpenSubKey(
                        subKeyName,
                        KeyPermissionCheck.ReadWriteSubTree,
                        true,
                        false
                );
                if (subKey != null)
                {
                    using (subKey)
                    {
                        var subSubKeyNames = subKey.GetSubKeyNames();
                        foreach (var subSubKeyName in subSubKeyNames)
                        {
                            subKey.DoDeleteSubKeyTree(subSubKeyName);
                        }
                    }

                    DoDeleteSubKey(subKeyName);
                }
                else
                {
                    throw new ArgumentException("Cannot delete a subkey tree because the subkey does not exist.");
                }
            }

            private void DoDeleteValue(
                    string valueName,
                    bool throwOnMissingValue)
            {
                if (_handle == null)
                {
                    throw new ObjectDisposedException(_name, "Cannot access a closed registry key.");
                }
                if (!IsWritable())
                {
                    throw new UnauthorizedAccessException("Cannot write to the registry key.");
                }

                var errorCode = Windows.RegDeleteValueW(
                        _handle,
                        valueName
                );

                if (throwOnMissingValue
                        && (errorCode == Windows.Error.FileNotFound || errorCode == Windows.Error.FilenameExceedRange))
                {
                    throw new ArgumentException("No value exists with that name.");
                }
            }

            private int DoGetSubKeyCount()
            {
                var bufferSize = 0u;
                var subKeyCount = 0u;
                var maxSubKeyLen = 0u;
                var maxClassLen = 0u;
                var maxValueLen = 0u;
                var maxValueNameLen = 0u;
                var valueCount = 0u;
                var errorCode = Windows.RegQueryInfoKeyW(
                        _handle,
                        null,
                        ref bufferSize,
                        IntPtr.Zero,
                        ref subKeyCount,
                        ref maxSubKeyLen,
                        ref maxClassLen,
                        ref valueCount,
                        ref maxValueNameLen,
                        ref maxValueLen,
                        IntPtr.Zero,
                        IntPtr.Zero
                );

                if (errorCode != Windows.Error.Success)
                {
                    ThrowException(
                            errorCode,
                            null
                    );
                }

                return (int)subKeyCount;
            }

            private string[] DoGetSubKeyNames(int subKeyCount)
            {
                if (_handle == null)
                {
                    throw new ObjectDisposedException(_name, "Cannot access a closed registry key.");
                }

                var subKeyNameList = new List<string>(subKeyCount);
                var subKeyNameCharArray = new char[MaxKeyLength + 1];
                var subKeyCharArrayLength = subKeyNameCharArray.Length;
                var bufferSize = 0u;
                Windows.Error result;

                while ((result = Windows.RegEnumKeyExW(
                        _handle,
                        (uint)subKeyNameList.Count,
                        subKeyNameCharArray,
                        ref subKeyCharArrayLength,
                        IntPtr.Zero,
                        null,
                        ref bufferSize,
                        IntPtr.Zero)) != Windows.Error.NoMoreItems)
                {
                    if (result == Windows.Error.Success)
                    {
                        subKeyNameList.Add(new string(subKeyNameCharArray, 0, subKeyCharArrayLength));
                        subKeyCharArrayLength = subKeyNameCharArray.Length;
                    }
                    else
                    {
                        ThrowException(
                                result,
                                null
                        );
                    }
                }

                return subKeyNameList.ToArray();
            }

            private object DoGetValue(
                    string valueName,
                    object defaultValue)
            {
                if (_handle == null)
                {
                    throw new ObjectDisposedException(_name, "Cannot access a closed registry key.");
                }

                var data = defaultValue;
                var registryValueType = Windows.RegistryValueType.None;
                uint bufferSize = 0;

                var errorCode = Windows.RegQueryValueExW(
                        _handle,
                        valueName,
                        IntPtr.Zero,
                        ref registryValueType,
                        (byte[])null,
                        ref bufferSize
                );
                if (errorCode != Windows.Error.Success && errorCode != Windows.Error.MoreData)
                {
                    return data;
                }

                if (registryValueType == Windows.RegistryValueType.DWord)
                {
                    var buffer = 0;
                    Windows.RegQueryValueExW(
                            _handle,
                            valueName,
                            IntPtr.Zero,
                            ref registryValueType,
                            ref buffer,
                            ref bufferSize
                    );
                    data = buffer;
                }
                if (registryValueType == Windows.RegistryValueType.QWord && bufferSize == 8)
                {
                    var buffer = 0L;
                    Windows.RegQueryValueExW(
                            _handle,
                            valueName,
                            IntPtr.Zero,
                            ref registryValueType,
                            ref buffer,
                            ref bufferSize
                    );
                    data = buffer;
                }
                if (registryValueType == Windows.RegistryValueType.None
                        || registryValueType == Windows.RegistryValueType.Binary
                        || registryValueType == Windows.RegistryValueType.DWordBigEndian)
                {
                    var buffer = new byte[bufferSize];
                    Windows.RegQueryValueExW(
                            _handle,
                            valueName,
                            IntPtr.Zero,
                            ref registryValueType,
                            buffer,
                            ref bufferSize
                    );
                    data = buffer;
                }
                if (registryValueType == Windows.RegistryValueType.String)
                {
                    bufferSize = NormalizeStringBufferSize(bufferSize);
                    var buffer = new char[bufferSize / 2];
                    Windows.RegQueryValueExW(
                            _handle,
                            valueName,
                            IntPtr.Zero,
                            ref registryValueType,
                            buffer,
                            ref bufferSize
                    );
                    data = ToStringFromBuffer(buffer);
                }
                if (registryValueType == Windows.RegistryValueType.ExpandString)
                {
                    bufferSize = NormalizeStringBufferSize(bufferSize);
                    var buffer = new char[bufferSize / 2];
                    Windows.RegQueryValueExW(
                            _handle,
                            valueName,
                            IntPtr.Zero,
                            ref registryValueType,
                            buffer,
                            ref bufferSize
                    );
                    data = Environment.ExpandEnvironmentVariables(ToStringFromBuffer(buffer));
                }
                if (registryValueType == Windows.RegistryValueType.MultiString)
                {
                    bufferSize = NormalizeStringBufferSize(bufferSize);
                    var buffer = new char[bufferSize / 2];
                    errorCode = Windows.RegQueryValueExW(
                            _handle,
                            valueName,
                            IntPtr.Zero,
                            ref registryValueType,
                            buffer,
                            ref bufferSize
                    );

                    if (buffer.Length > 0 && buffer[buffer.Length - 1] != (char)0)
                    {
                        Array.Resize(ref buffer, buffer.Length + 1);
                    }

                    var stringList = new List<string>();

                    var beginIndex = 0;
                    var bufferLength = buffer.Length;

                    while (errorCode == Windows.Error.Success && beginIndex < bufferLength)
                    {
                        var endIndex = beginIndex;
                        while (endIndex < bufferLength && buffer[endIndex] != (char)0)
                        {
                            endIndex++;
                        }

                        string subString = null;
                        if (endIndex < bufferLength)
                        {
                            if (endIndex - beginIndex > 0)
                            {
                                subString = new string(buffer, beginIndex, endIndex - beginIndex);
                            }
                            else if (endIndex != bufferLength - 1)
                            {
                                subString = string.Empty;
                            }
                        }
                        else
                        {
                            subString = new string(buffer, beginIndex, bufferLength - beginIndex);
                        }
                        beginIndex = endIndex + 1;

                        if (subString == null)
                        {
                            continue;
                        }

                        stringList.Add(subString);
                    }

                    data = stringList.ToArray();
                }

                return data;
            }

            private ValueKind DoGetValueKind(string valueName)
            {
                if (_handle == null)
                {
                    throw new ObjectDisposedException(_name, "Cannot access a closed registry key.");
                }

                var registryValueType = Windows.RegistryValueType.None;
                uint bufferSize = 0;
                var errorCode = Windows.RegQueryValueExW(
                        _handle,
                        valueName,
                        IntPtr.Zero,
                        ref registryValueType,
                        (byte[])null,
                        ref bufferSize
                );
                if (errorCode != Windows.Error.Success)
                {
                    ThrowException(
                            errorCode,
                            null
                    );
                }

                if (!Enum.IsDefined(typeof(Windows.RegistryValueType), registryValueType))
                {
                    return ValueKind.None;
                }
                return (ValueKind)registryValueType;
            }

            private Key DoOpenSubKey(
                    string subKeyName,
                    KeyPermissionCheck keyPermissionCheck,
                    bool writable,
                    bool throwOnAccessDenied)
            {
                if (_handle == null)
                {
                    throw new ObjectDisposedException(_name, "Cannot access a closed registry key.");
                }

                var normalizedSubKeyName = NormalizeSubKeyName(subKeyName);

                Windows.SafeRegistryHandle handle;
                var errorCode = Windows.RegOpenKeyExW(
                        _handle,
                        normalizedSubKeyName,
                        0,
                        ToRegistryKeyAccessRight(writable) | ToRegistryKeyAccessRight(_view),
                        out handle
                );
                if (errorCode == Windows.Error.Success && !handle.IsInvalid)
                {
                    return new Key(
                            handle,
                            _view,
                            writable,
                            false
                    )
                    {
                        _name = $"{_name}\\{normalizedSubKeyName}",
                        _keyPermissionCheck = keyPermissionCheck
                    };
                }
                if (throwOnAccessDenied
                        && (errorCode == Windows.Error.AccessDenied || errorCode == Windows.Error.BadImpersonationLevel))
                {
                    throw new SecurityException("Requested registry access is not allowed.");
                }

                return null;
            }

            private void DoSetValue(
                    string valueName,
                    object value,
                    ValueKind valueKind)
            {
                if (_handle == null)
                {
                    throw new ObjectDisposedException(_name, "Cannot access a closed registry key.");
                }
                if (!IsWritable())
                {
                    throw new UnauthorizedAccessException("Cannot write to the registry key.");
                }

                var errorCode = Windows.Error.Success;
                try
                {
                    if (valueKind == ValueKind.DWord)
                    {
                        var data = System.Convert.ToInt32(
                                value,
                                System.Globalization.CultureInfo.InvariantCulture
                        );
                        errorCode = Windows.RegSetValueExW(
                                _handle,
                                valueName,
                                0,
                                Windows.RegistryValueType.DWord,
                                ref data,
                                4
                        );
                    }
                    if (valueKind == ValueKind.QWord)
                    {
                        var data = System.Convert.ToInt64(
                                value,
                                System.Globalization.CultureInfo.InvariantCulture
                        );
                        errorCode = Windows.RegSetValueExW(
                                _handle,
                                valueName,
                                0,
                                Windows.RegistryValueType.QWord,
                                ref data,
                                8
                        );
                    }
                    if (valueKind == ValueKind.None
                            || valueKind == ValueKind.Binary)
                    {
                        var data = (byte[])value;
                        errorCode = Windows.RegSetValueExW(
                                _handle,
                                valueName,
                                0,
                                ToRegistryValueType(valueKind),
                                data,
                                data.Length
                        );
                    }
                    if (valueKind == ValueKind.ExpandString
                            || valueKind == ValueKind.String)
                    {
                        var data = value.ToString();
                        errorCode = Windows.RegSetValueExW(
                                _handle,
                                valueName,
                                0,
                                ToRegistryValueType(valueKind),
                                data,
                                checked(data.Length * 2 + 2)
                        );
                    }
                    if (valueKind == ValueKind.MultiString)
                    {
                        var clonedStringArray = (string[])((string[])value).Clone();

                        // Format: str1\0str2\0str3\0\0
                        var sizeInChars = 1;
                        foreach (var item in clonedStringArray)
                        {
                            if (item == null)
                            {
                                throw new ArgumentException("Win32Registry.Key.SetValue does not allow a string[] that contains a null string reference.");
                            }
                            sizeInChars = checked(sizeInChars + (item.Length + 1));
                        }
                        var sizeInBytes = checked(sizeInChars * sizeof(char));

                        var data = new char[sizeInChars];
                        var destinationIndex = 0;
                        foreach (var item in clonedStringArray)
                        {
                            var itemLength = item.Length;
                            item.CopyTo(
                                    0,
                                    data,
                                    destinationIndex,
                                    itemLength
                            );
                            destinationIndex += (itemLength + 1);
                        }

                        errorCode = Windows.RegSetValueExW(
                                _handle,
                                valueName,
                                0,
                                Windows.RegistryValueType.MultiString,
                                data,
                                sizeInBytes
                        );
                    }
                }
                catch (Exception exc) when (exc is OverflowException || exc is InvalidOperationException || exc is FormatException || exc is InvalidCastException)
                {
                    throw new ArgumentException("The type of the value object did not match the specified Win32Registry.ValueKind or the object could not be properly converted.");
                }

                if (errorCode != Windows.Error.Success)
                {
                    ThrowException(
                            errorCode,
                            null
                    );
                }
            }

            /// <summary>
            /// Gets the subkey names.
            /// </summary>
            /// <returns>System.String[].</returns>
            public string[] GetSubKeyNames()
            {
                var subKeyCount = SubKeyCount;
                if (subKeyCount <= 0)
                {
                    return new string[0];
                }
                return DoGetSubKeyNames(subKeyCount);
            }

            /// <summary>
            /// Gets the value.
            /// </summary>
            /// <param name="valueName">The value name.</param>
            /// <returns>System.Object.</returns>
            public object GetValue(string valueName)
            {
                return DoGetValue(
                        valueName,
                        null
                );
            }

            /// <summary>
            /// Gets the value kind.
            /// </summary>
            /// <param name="valueName">The value name.</param>
            /// <returns>ValueKind.</returns>
            public ValueKind GetValueKind(string valueName)
            {
                return DoGetValueKind(valueName);
            }

            private bool IsSystemKey()
            {
                return (_keyStateFlag & KeyStateFlag.SystemKey) != 0;
            }

            private bool IsWritable()
            {
                return (_keyStateFlag & KeyStateFlag.WriteAccess) != 0;
            }

            private static void NormalizePath(StringBuilder path)
            {
                var pathLength = path.Length;
                var needToBeNormalized = false;
                const char markerChar = (char)0xFFFF;

                var i = 1;
                while (i < pathLength - 1)
                {
                    if (path[i] == '\\')
                    {
                        i++;
                        while (i < pathLength && path[i] == '\\')
                        {
                            path[i] = markerChar;
                            i++;
                            needToBeNormalized = true;
                        }
                    }
                    i++;
                }

                if (!needToBeNormalized)
                {
                    return;
                }

                i = 0;
                var j = 0;
                while (i < pathLength)
                {
                    if (path[i] == markerChar)
                    {
                        i++;
                        continue;
                    }
                    path[j] = path[i];
                    i++;
                    j++;
                }
                path.Length += j - i;
            }

            private static uint NormalizeStringBufferSize(uint bufferSize)
            {
                if (bufferSize % 2 != 1)
                {
                    return bufferSize;
                }

                try
                {
                    bufferSize = checked(bufferSize + 1);
                }
                catch (OverflowException e)
                {
                    throw new IOException("Win32Registry.Key.GetValue does not allow a String that has a length greater than Int32.MaxValue.", e);
                }
                return bufferSize;
            }

            private static string NormalizeSubKeyName(string name)
            {
                if (name.IndexOf('\\') == -1)
                {
                    return name;
                }

                var stringBuilder = new StringBuilder(name);
                NormalizePath(stringBuilder);
                var index = stringBuilder.Length - 1;
                if (index >= 0 && stringBuilder[index] == '\\') // Remove trailing slash
                {
                    stringBuilder.Length = index;
                }

                return stringBuilder.ToString();
            }

            /// <summary>
            /// Opens the base key.
            /// </summary>
            /// <param name="hive">The hive.</param>
            /// <returns>Key.</returns>
            public static Key OpenBaseKey(Hive hive)
            {
                return OpenBaseKey(
                        hive,
                        View.Default
                );
            }

            /// <summary>
            /// Opens the base key.
            /// </summary>
            /// <param name="hive">The hive.</param>
            /// <param name="view">The view.</param>
            /// <returns>Key.</returns>
            public static Key OpenBaseKey(
                    Hive hive,
                    View view)
            {
                return new Key(
                        new Windows.SafeRegistryHandle(
                                (IntPtr)hive,
                                false
                        ),
                        view,
                        true,
                        true
                )
                {
                    _keyPermissionCheck = KeyPermissionCheck.Default,
                    _name = RegistryHiveWithName[hive]
                };
            }

            /// <summary>
            /// Opens the subkey.
            /// </summary>
            /// <param name="subKeyName">The subkey name.</param>
            /// <param name="keyPermissionCheck">The key permission check.</param>
            /// <returns>Key.</returns>
            public Key OpenSubKey(
                    string subKeyName,
                    KeyPermissionCheck keyPermissionCheck)
            {
                return DoOpenSubKey(
                        subKeyName,
                        keyPermissionCheck,
                        keyPermissionCheck == KeyPermissionCheck.ReadWriteSubTree,
                        true
                );
            }

            /// <summary>
            /// Sets the value.
            /// </summary>
            /// <param name="valueName">The value name.</param>
            /// <param name="value">The value.</param>
            /// <param name="valueKind">The value kind.</param>
            /// <exception cref="ArgumentNullException">value</exception>
            /// <exception cref="ArgumentException">Registry value names should not be greater than 16,383 characters. - valueName</exception>
            /// <exception cref="ArgumentException">The specified RegistryValueKind is an invalid value - valueKind</exception>
            public void SetValue(
                    string valueName,
                    object value,
                    ValueKind valueKind)
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                if (valueName != null && valueName.Length > MaxValueNameLength)
                {
                    throw new ArgumentException("Registry value names should not be greater than 16,383 characters.", nameof(valueName));
                }

                if (!Enum.IsDefined(typeof(ValueKind), valueKind))
                {
                    throw new ArgumentException("The specified RegistryValueKind is an invalid value", nameof(valueKind));
                }

                DoSetValue(valueName, value, valueKind);
            }

            private void ThrowException(
                    Windows.Error errorCode,
                    string keyName)
            {
                if (errorCode == Windows.Error.AccessDenied)
                {
                    if (keyName != null)
                    {
                        throw new UnauthorizedAccessException($"Access to the registry key '{keyName}' is denied.");
                    }
                    throw new UnauthorizedAccessException();
                }
                if (errorCode == Windows.Error.InvalidHandle)
                {
                    _handle.SetHandleAsInvalid();
                    _handle = null;
                    throw new IOException("Unexpected error: " + errorCode, (int)errorCode);
                }
                if (errorCode == Windows.Error.FileNotFound)
                {
                    throw new IOException("The specified registry key does not exist.", (int)errorCode);
                }
                throw new IOException("Unexpected error: " + errorCode, (int)errorCode);
            }

            private static Windows.RegistryKeyAccessRight ToRegistryKeyAccessRight(bool isWritable)
            {
                if (!isWritable)
                {
                    return Windows.RegistryKeyAccessRight.Read;
                }

                return Windows.RegistryKeyAccessRight.Read | Windows.RegistryKeyAccessRight.Write;
            }

            private static Windows.RegistryKeyAccessRight ToRegistryKeyAccessRight(KeyPermissionCheck keyPermissionCheck)
            {
                if (keyPermissionCheck == KeyPermissionCheck.Default
                        || keyPermissionCheck == KeyPermissionCheck.ReadSubTree)
                {
                    return Windows.RegistryKeyAccessRight.Read;
                }
                if (keyPermissionCheck == KeyPermissionCheck.ReadWriteSubTree)
                {
                    return Windows.RegistryKeyAccessRight.Read | Windows.RegistryKeyAccessRight.Write;
                }

                return Windows.RegistryKeyAccessRight.None;
            }

            private static Windows.RegistryKeyAccessRight ToRegistryKeyAccessRight(View view)
            {
                return (Windows.RegistryKeyAccessRight)view;
            }

            private static Windows.RegistryValueType ToRegistryValueType(ValueKind valueKind)
            {
                return (Windows.RegistryValueType)valueKind;
            }

            private static string ToStringFromBuffer(char[] buffer)
            {
                if (buffer == null)
                {
                    return null;
                }

                if (buffer.Length <= 0 || buffer[buffer.Length - 1] != (char)0)
                {
                    return new string(buffer);
                }

                return new string(buffer, 0, buffer.Length - 1);
            }
        }
    }
}
