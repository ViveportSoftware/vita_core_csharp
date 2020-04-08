using System.Threading.Tasks;

namespace Htc.Vita.Core.Preference
{
    public abstract partial class PreferenceFactory
    {
        /// <summary>
        /// Loads the preferences asynchronously.
        /// </summary>
        /// <returns>Task&lt;Preferences&gt;.</returns>
        public Task<Preferences> LoadPreferencesAsync()
        {
            return LoadPreferencesAsync("");
        }

        /// <summary>
        /// Loads the preferences asynchronously.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <returns>Task&lt;Preferences&gt;.</returns>
        public Task<Preferences> LoadPreferencesAsync(string label)
        {
            if (string.IsNullOrWhiteSpace(label))
            {
                return OnLoadPreferencesAsync("default");
            }
            return OnLoadPreferencesAsync(label);
        }

        /// <summary>
        /// Called when loading preferences asynchronously.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <returns>Task&lt;Preferences&gt;.</returns>
        protected abstract Task<Preferences> OnLoadPreferencesAsync(string label);
    }
}
