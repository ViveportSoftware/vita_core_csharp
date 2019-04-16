using System;
using System.Collections.Generic;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Preference
{
    public abstract partial class PreferenceFactory
    {
        private static Dictionary<string, PreferenceFactory> Instances { get; } = new Dictionary<string, PreferenceFactory>();

        private static readonly object InstancesLock = new object();

        private static Type defaultType = typeof(DefaultPreferenceFactory);

        public static void Register<T>() where T : PreferenceFactory
        {
            defaultType = typeof(T);
            Logger.GetInstance(typeof(PreferenceFactory)).Info("Registered default " + typeof(PreferenceFactory).Name + " type to " + defaultType);
        }

        public static PreferenceFactory GetInstance()
        {
            PreferenceFactory instance;
            try
            {
                instance = DoGetInstance(defaultType);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(PreferenceFactory)).Fatal("Instance initialization error: " + e);
                Logger.GetInstance(typeof(PreferenceFactory)).Info("Initializing " + typeof(DefaultPreferenceFactory).FullName + "...");
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
                Logger.GetInstance(typeof(PreferenceFactory)).Fatal("Instance initialization error: " + e);
                Logger.GetInstance(typeof(PreferenceFactory)).Info("Initializing " + typeof(DefaultPreferenceFactory).FullName + "...");
                instance = new DefaultPreferenceFactory();
            }
            return instance;
        }

        private static PreferenceFactory DoGetInstance(Type type)
        {
            if (type == null)
            {
                throw new ArgumentException("Invalid arguments to get " + typeof(PreferenceFactory).Name + " instance");
            }

            var key = type.FullName + "_";
            PreferenceFactory instance = null;
            if (Instances.ContainsKey(key))
            {
                instance = Instances[key];
            }
            if (instance == null)
            {
                Logger.GetInstance(typeof(PreferenceFactory)).Info("Initializing " + key + "...");
                var constructor = type.GetConstructor(new Type[] { });
                if (constructor != null)
                {
                    instance = (PreferenceFactory)constructor.Invoke(new object[] { });
                }
            }
            if (instance == null)
            {
                Logger.GetInstance(typeof(PreferenceFactory)).Info("Initializing " + typeof(DefaultPreferenceFactory).FullName + "...");
                instance = new DefaultPreferenceFactory();
            }
            lock (InstancesLock)
            {
                if (!Instances.ContainsKey(key))
                {
                    Instances.Add(key, instance);
                }
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
                return OnLoadPreferences("default");
            }
            return OnLoadPreferences(label);
        }

        protected abstract Preferences OnLoadPreferences(string label);
    }
}
