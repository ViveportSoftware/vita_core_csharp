using Htc.Vita.Core.Util;

namespace Htc.Vita.Core.Preference
{
    /// <summary>
    /// Class PreferenceFactory.
    /// </summary>
    public abstract partial class PreferenceFactory
    {
        static PreferenceFactory()
        {
            TypeRegistry.RegisterDefault<PreferenceFactory, DefaultPreferenceFactory>();
        }

        /// <summary>
        /// Registers the instance type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void Register<T>()
                where T : PreferenceFactory, new()
        {
            TypeRegistry.Register<PreferenceFactory, T>();
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <returns>PreferenceFactory.</returns>
        public static PreferenceFactory GetInstance()
        {
            return TypeRegistry.GetInstance<PreferenceFactory>();
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>PreferenceFactory.</returns>
        public static PreferenceFactory GetInstance<T>()
                where T : PreferenceFactory, new()
        {
            return TypeRegistry.GetInstance<PreferenceFactory, T>();
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
