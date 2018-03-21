using System;
using System.Collections.Generic;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Preference
{
    public abstract partial class Preferences
    {
        public string Label { get; } = string.Empty;

        protected Preferences(string label)
        {
            if (!string.IsNullOrWhiteSpace(label))
            {
                Label = label;
            }
        }

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

        public bool Commit()
        {
            return OnSave();
        }

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

        public Preferences Initialize()
        {
            return OnInitialize();
        }

        public bool ParseBool(string key)
        {
            return ParseBool(key, false);
        }

        public bool ParseBool(string key, bool defaultValue)
        {
            var result = defaultValue;
            try
            {
                result = OnParseBool(key, defaultValue);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Preferences)).Error(e.ToString());
            }
            return result;
        }

        public double ParseDouble(string key)
        {
            return ParseDouble(key, 0.0D);
        }

        public double ParseDouble(string key, double defaultValue)
        {
            var result = defaultValue;
            try
            {
                result = OnParseDouble(key, defaultValue);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Preferences)).Error(e.ToString());
            }
            return result;
        }

        public float ParseFloat(string key)
        {
            return ParseFloat(key, 0.0F);
        }

        public float ParseFloat(string key, float defaultValue)
        {
            var result = defaultValue;
            try
            {
                result = OnParseFloat(key, defaultValue);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Preferences)).Error(e.ToString());
            }
            return result;
        }

        public int ParseInt(string key)
        {
            return ParseInt(key, 0);
        }

        public int ParseInt(string key, int defaultValue)
        {
            var result = defaultValue;
            try
            {
                result = OnParseInt(key, defaultValue);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Preferences)).Error(e.ToString());
            }
            return result;
        }

        public long ParseLong(string key)
        {
            return ParseLong(key, 0L);
        }

        public long ParseLong(string key, long defaultValue)
        {
            var result = defaultValue;
            try
            {
                result = OnParseLong(key, defaultValue);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Preferences)).Error(e.ToString());
            }
            return result;
        }

        public string ParseString(string key)
        {
            return ParseString(key, null);
        }

        public string ParseString(string key, string defaultValue)
        {
            var result = defaultValue;
            try
            {
                result = OnParseString(key, defaultValue);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Preferences)).Error(e.ToString());
            }
            return result;
        }

        public Preferences Put(string key, bool value)
        {
            var result = this;
            try
            {
                result = OnPutBool(key, value);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Preferences)).Error(e.ToString());
            }
            return result;
        }

        public Preferences Put(string key, double value)
        {
            var result = this;
            try
            {
                result = OnPutDouble(key, value);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Preferences)).Error(e.ToString());
            }
            return result;
        }

        public Preferences Put(string key, float value)
        {
            var result = this;
            try
            {
                result = OnPutFloat(key, value);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Preferences)).Error(e.ToString());
            }
            return result;
        }

        public Preferences Put(string key, int value)
        {
            var result = this;
            try
            {
                result = OnPutInt(key, value);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Preferences)).Error(e.ToString());
            }
            return result;
        }

        public Preferences Put(string key, long value)
        {
            var result = this;
            try
            {
                result = OnPutLong(key, value);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Preferences)).Error(e.ToString());
            }
            return result;
        }

        public Preferences Put(string key, string value)
        {
            var result = this;
            try
            {
                result = OnPutString(key, value);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Preferences)).Error(e.ToString());
            }
            return result;
        }

        protected abstract ICollection<string> OnAllKeys();
        protected abstract Preferences OnClear();
        protected abstract bool OnHasKey(string key);
        protected abstract Preferences OnInitialize();
        protected abstract bool OnParseBool(string key, bool defaultValue);
        protected abstract double OnParseDouble(string key, double defaultValue);
        protected abstract float OnParseFloat(string key, float defaultValue);
        protected abstract int OnParseInt(string key, int defaultValue);
        protected abstract long OnParseLong(string key, long defaultValue);
        protected abstract string OnParseString(string key, string defaultValue);
        protected abstract Preferences OnPutBool(string key, bool value);
        protected abstract Preferences OnPutDouble(string key, double value);
        protected abstract Preferences OnPutFloat(string key, float value);
        protected abstract Preferences OnPutInt(string key, int value);
        protected abstract Preferences OnPutLong(string key, long value);
        protected abstract Preferences OnPutString(string key, string value);
        protected abstract bool OnSave();
    }
}
