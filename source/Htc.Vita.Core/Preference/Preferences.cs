using System;
using System.Collections.Generic;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Preference
{
    /// <summary>
    /// Class Preferences.
    /// </summary>
    public abstract partial class Preferences
    {
        /// <summary>
        /// Gets the label.
        /// </summary>
        /// <value>The label.</value>
        public string Label { get; } = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="Preferences"/> class.
        /// </summary>
        /// <param name="label">The label.</param>
        protected Preferences(string label)
        {
            if (!string.IsNullOrWhiteSpace(label))
            {
                Label = label;
            }
        }

        /// <summary>
        /// Gets all keys.
        /// </summary>
        /// <returns>ICollection&lt;System.String&gt;.</returns>
        public ICollection<string> AllKeys()
        {
            ICollection<string> result = null;
            try
            {
                result = OnAllKeys();
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Preferences)).Error(e.ToString());
            }
            return result ?? new List<string>();
        }

        /// <summary>
        /// Clears this preferences.
        /// </summary>
        /// <returns>Preferences.</returns>
        public Preferences Clear()
        {
            try
            {
                return OnClear();
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Preferences)).Error(e.ToString());
            }
            return this;
        }

        /// <summary>
        /// Commits this preferences.
        /// </summary>
        /// <returns><c>true</c> if success, <c>false</c> otherwise.</returns>
        public bool Commit()
        {
            return OnSave();
        }

        /// <summary>
        /// Determines whether the preferences has the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns><c>true</c> if the preferences has the specified key; otherwise, <c>false</c>.</returns>
        public bool HasKey(string key)
        {
            var result = false;
            try
            {
                result = OnHasKey(key);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Preferences)).Error(e.ToString());
            }
            return result;
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <returns>Preferences.</returns>
        public Preferences Initialize()
        {
            return OnInitialize();
        }

        /// <summary>
        /// Parses bool value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>The value.</returns>
        public bool ParseBool(string key)
        {
            return ParseBool(
                    key,
                    false
            );
        }

        /// <summary>
        /// Parses bool value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The value.</returns>
        public bool ParseBool(
                string key,
                bool defaultValue)
        {
            var result = defaultValue;
            try
            {
                result = OnParseBool(
                        key,
                        defaultValue
                );
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Preferences)).Error(e.ToString());
            }
            return result;
        }

        /// <summary>
        /// Parses double value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>System.Double.</returns>
        public double ParseDouble(string key)
        {
            return ParseDouble(
                    key,
                    0.0D
            );
        }

        /// <summary>
        /// Parses double value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.Double.</returns>
        public double ParseDouble(
                string key,
                double defaultValue)
        {
            var result = defaultValue;
            try
            {
                result = OnParseDouble(
                        key,
                        defaultValue
                );
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Preferences)).Error(e.ToString());
            }
            return result;
        }

        /// <summary>
        /// Parses float value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>System.Single.</returns>
        public float ParseFloat(string key)
        {
            return ParseFloat(key, 0.0F);
        }

        /// <summary>
        /// Parses float value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.Single.</returns>
        public float ParseFloat(
                string key,
                float defaultValue)
        {
            var result = defaultValue;
            try
            {
                result = OnParseFloat(
                        key,
                        defaultValue
                );
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Preferences)).Error(e.ToString());
            }
            return result;
        }

        /// <summary>
        /// Parses int value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>System.Int32.</returns>
        public int ParseInt(string key)
        {
            return ParseInt(
                    key,
                    0
            );
        }

        /// <summary>
        /// Parses int value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.Int32.</returns>
        public int ParseInt(
                string key,
                int defaultValue)
        {
            var result = defaultValue;
            try
            {
                result = OnParseInt(
                        key,
                        defaultValue
                );
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Preferences)).Error(e.ToString());
            }
            return result;
        }

        /// <summary>
        /// Parses long value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>System.Int64.</returns>
        public long ParseLong(string key)
        {
            return ParseLong(
                    key,
                    0L
            );
        }

        /// <summary>
        /// Parses long value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.Int64.</returns>
        public long ParseLong(
                string key,
                long defaultValue)
        {
            var result = defaultValue;
            try
            {
                result = OnParseLong(
                        key,
                        defaultValue
                );
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Preferences)).Error(e.ToString());
            }
            return result;
        }

        /// <summary>
        /// Parses string value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>System.String.</returns>
        public string ParseString(string key)
        {
            return ParseString(
                    key,
                    null
            );
        }

        /// <summary>
        /// Parses string value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.String.</returns>
        public string ParseString(
                string key,
                string defaultValue)
        {
            var result = defaultValue;
            try
            {
                result = OnParseString(
                        key,
                        defaultValue
                );
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Preferences)).Error(e.ToString());
            }
            return result;
        }

        /// <summary>
        /// Puts the value with the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>Preferences.</returns>
        public Preferences Put(
                string key,
                bool value)
        {
            var result = this;
            try
            {
                result = OnPutBool(
                        key,
                        value
                );
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Preferences)).Error(e.ToString());
            }
            return result;
        }

        /// <summary>
        /// Puts the value with the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>Preferences.</returns>
        public Preferences Put(
                string key,
                double value)
        {
            var result = this;
            try
            {
                result = OnPutDouble(
                        key,
                        value
                );
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Preferences)).Error(e.ToString());
            }
            return result;
        }

        /// <summary>
        /// Puts the value with the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>Preferences.</returns>
        public Preferences Put(
                string key,
                float value)
        {
            var result = this;
            try
            {
                result = OnPutFloat(
                        key,
                        value
                );
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Preferences)).Error(e.ToString());
            }
            return result;
        }

        /// <summary>
        /// Puts the value with the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>Preferences.</returns>
        public Preferences Put(
                string key,
                int value)
        {
            var result = this;
            try
            {
                result = OnPutInt(
                        key,
                        value
                );
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Preferences)).Error(e.ToString());
            }
            return result;
        }

        /// <summary>
        /// Puts the value with the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>Preferences.</returns>
        public Preferences Put(
                string key,
                long value)
        {
            var result = this;
            try
            {
                result = OnPutLong(
                        key,
                        value
                );
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Preferences)).Error(e.ToString());
            }
            return result;
        }

        /// <summary>
        /// Puts the value with the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>Preferences.</returns>
        public Preferences Put(
                string key,
                string value)
        {
            var result = this;
            try
            {
                result = OnPutString(
                        key,
                        value
                );
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Preferences)).Error(e.ToString());
            }
            return result;
        }

        /// <summary>
        /// Called when getting all keys.
        /// </summary>
        /// <returns>ICollection&lt;System.String&gt;.</returns>
        protected abstract ICollection<string> OnAllKeys();
        /// <summary>
        /// Called when clearing this preferences.
        /// </summary>
        /// <returns>Preferences.</returns>
        protected abstract Preferences OnClear();
        /// <summary>
        /// Called when determining whether the preferences has the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns><c>true</c> if having the key, <c>false</c> otherwise.</returns>
        protected abstract bool OnHasKey(string key);
        /// <summary>
        /// Called when initializing.
        /// </summary>
        /// <returns>Preferences.</returns>
        protected abstract Preferences OnInitialize();
        /// <summary>
        /// Called when parsing bool value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.Boolean.</returns>
        protected abstract bool OnParseBool(
                string key,
                bool defaultValue
        );
        /// <summary>
        /// Called when parsing double value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.Double.</returns>
        protected abstract double OnParseDouble(
                string key,
                double defaultValue
        );
        /// <summary>
        /// Called when parsing float value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.Single.</returns>
        protected abstract float OnParseFloat(
                string key,
                float defaultValue
        );
        /// <summary>
        /// Called when parsing int value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.Int32.</returns>
        protected abstract int OnParseInt(
                string key,
                int defaultValue
        );
        /// <summary>
        /// Called when parsing long value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.Int64.</returns>
        protected abstract long OnParseLong(
                string key,
                long defaultValue
        );
        /// <summary>
        /// Called when parsing string value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.String.</returns>
        protected abstract string OnParseString(
                string key,
                string defaultValue
        );
        /// <summary>
        /// Called when putting the value with the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <returns>Preferences.</returns>
        protected abstract Preferences OnPutBool(
                string key,
                bool value
        );
        /// <summary>
        /// Called when putting the value with the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>Preferences.</returns>
        protected abstract Preferences OnPutDouble(
                string key,
                double value
        );
        /// <summary>
        /// Called when putting the value with the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>Preferences.</returns>
        protected abstract Preferences OnPutFloat(
                string key,
                float value
        );
        /// <summary>
        /// Called when putting the value with the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>Preferences.</returns>
        protected abstract Preferences OnPutInt(
                string key,
                int value
        );
        /// <summary>
        /// Called when putting the value with the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>Preferences.</returns>
        protected abstract Preferences OnPutLong(
                string key,
                long value
        );
        /// <summary>
        /// Called when putting the value with the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>Preferences.</returns>
        protected abstract Preferences OnPutString(
                string key,
                string value
        );
        /// <summary>
        /// Called when saving.
        /// </summary>
        /// <returns><c>true</c> if saving successfully, <c>false</c> otherwise.</returns>
        protected abstract bool OnSave();
    }
}
