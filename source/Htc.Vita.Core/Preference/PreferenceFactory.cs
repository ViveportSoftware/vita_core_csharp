using System;
using System.Collections.Generic;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Preference
{
    /// <summary>
    /// Class PreferenceFactory.
    /// </summary>
    public abstract partial class PreferenceFactory
    {
        private static Dictionary<string, PreferenceFactory> Instances { get; } = new Dictionary<string, PreferenceFactory>();

        private static readonly object InstancesLock = new object();

        private static Type _defaultType = typeof(DefaultPreferenceFactory);

        /// <summary>
        /// Registers the instance type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void Register<T>() where T : PreferenceFactory
        {
            _defaultType = typeof(T);
            Logger.GetInstance(typeof(PreferenceFactory)).Info($"Registered default {typeof(PreferenceFactory).Name} type to {_defaultType}");
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <returns>PreferenceFactory.</returns>
        public static PreferenceFactory GetInstance()
        {
            PreferenceFactory instance;
            try
            {
                instance = DoGetInstance(_defaultType);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(PreferenceFactory)).Fatal($"Instance initialization error: {e}");
                Logger.GetInstance(typeof(PreferenceFactory)).Info($"Initializing {typeof(DefaultPreferenceFactory).FullName}...");
                instance = new DefaultPreferenceFactory();
            }
            return instance;
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>PreferenceFactory.</returns>
        public static PreferenceFactory GetInstance<T>() where T : PreferenceFactory
        {
            PreferenceFactory instance;
            try
            {
                instance = DoGetInstance(typeof(T));
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(PreferenceFactory)).Fatal($"Instance initialization error: {e}");
                Logger.GetInstance(typeof(PreferenceFactory)).Info($"Initializing {typeof(DefaultPreferenceFactory).FullName}...");
                instance = new DefaultPreferenceFactory();
            }
            return instance;
        }

        private static PreferenceFactory DoGetInstance(Type type)
        {
            if (type == null)
            {
                throw new ArgumentException($"Invalid arguments to get {typeof(PreferenceFactory).Name} instance");
            }

            var key = $"{type.FullName}_";
            PreferenceFactory instance = null;
            if (Instances.ContainsKey(key))
            {
                instance = Instances[key];
            }
            if (instance == null)
            {
                Logger.GetInstance(typeof(PreferenceFactory)).Info($"Initializing {key}...");
                var constructor = type.GetConstructor(new Type[] { });
                if (constructor != null)
                {
                    instance = (PreferenceFactory)constructor.Invoke(new object[] { });
                }
            }
            if (instance == null)
            {
                Logger.GetInstance(typeof(PreferenceFactory)).Info($"Initializing {typeof(DefaultPreferenceFactory).FullName}...");
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

        /// <summary>
        /// Loads the preferences.
        /// </summary>
        /// <returns>Preferences.</returns>
        public Preferences LoadPreferences()
        {
            return LoadPreferences("");
        }

        /// <summary>
        /// Loads the preferences.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <returns>Preferences.</returns>
        public Preferences LoadPreferences(string label)
        {
            if (string.IsNullOrWhiteSpace(label))
            {
                return OnLoadPreferences("default");
            }
            return OnLoadPreferences(label);
        }

        /// <summary>
        /// Called when loading preferences.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <returns>Preferences.</returns>
        protected abstract Preferences OnLoadPreferences(string label);
    }
}
