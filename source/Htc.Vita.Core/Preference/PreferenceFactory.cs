using System;
using System.Collections.Generic;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Preference
{
    public abstract class PreferenceFactory
    {
        private static Dictionary<string, PreferenceFactory> Instances { get; } = new Dictionary<string, PreferenceFactory>();
        private static Type _defaultType = typeof(DefaultPreferenceFactory);

        public static void Register<T>() where T : PreferenceFactory
        {
            _defaultType = typeof(T);
            Logger.GetInstance().Info("Registered default preference factory type to " + _defaultType);
        }

        public static PreferenceFactory GetInstance()
        {
            PreferenceFactory instance;
            try
            {
                instance = DoGetInstance(_defaultType);
            }
            catch (Exception e)
            {
                Logger.GetInstance().Fatal("Instance initialization error " + e);
                Logger.GetInstance().Info("Initializing " + typeof(DefaultPreferenceFactory).FullName + "...");
                instance = new DefaultPreferenceFactory();
            }
            return instance;
        }

        public static PreferenceFactory GetInstance<T>() where T : PreferenceFactory
        {
            PreferenceFactory instance;
            try
            {
                instance = DoGetInstance(typeof(T));
            }
            catch (Exception e)
            {
                Logger.GetInstance().Fatal("Instance initialization error " + e);
                Logger.GetInstance().Info("Initializing " + typeof(DefaultPreferenceFactory).FullName + "...");
                instance = new DefaultPreferenceFactory();
            }
            return instance;
        }

        private static PreferenceFactory DoGetInstance(Type type)
        {
            if (type == null)
            {
                throw new ArgumentException("Invalid arguments to get preference factory instance");
            }

            var key = type.FullName;
            PreferenceFactory instance = null;
            if (Instances.ContainsKey(key))
            {
                instance = Instances[key];
            }
            if (instance == null)
            {
                Logger.GetInstance().Info("Initializing " + key + "...");
                var constructor = type.GetConstructor(new Type[] { });
                if (constructor != null)
                {
                    instance = (PreferenceFactory)constructor.Invoke(new object[] { });
                }
            }
            if (instance == null)
            {
                Logger.GetInstance().Info("Initializing " + typeof(DefaultPreferenceFactory).FullName + "...");
                instance = new DefaultPreferenceFactory();
            }
            if (!Instances.ContainsKey(key))
            {
                Instances.Add(key, instance);
            }
            return instance;
        }

        public Preferences LoadPreferences()
        {
            return LoadPreferences("");
        }

        public Preferences LoadPreferences(string label)
        {
            if (string.IsNullOrWhiteSpace(label))
            {
                OnLoadPreferences("default");
            }
            return OnLoadPreferences(label);
        }

        protected abstract Preferences OnLoadPreferences(string label);
    }
}
