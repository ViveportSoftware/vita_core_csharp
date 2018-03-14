using System.Threading.Tasks;

namespace Htc.Vita.Core.Preference
{
    public abstract partial class PreferenceFactory
    {
        public async Task<Preferences> LoadPreferencesAsync()
        {
            return await LoadPreferencesAsync("").ConfigureAwait(false);
        }

        public async Task<Preferences> LoadPreferencesAsync(string label)
        {
            if (string.IsNullOrWhiteSpace(label))
            {
                return await OnLoadPreferencesAsync("default").ConfigureAwait(false);
            }
            return await OnLoadPreferencesAsync(label).ConfigureAwait(false);
        }

        protected abstract Task<Preferences> OnLoadPreferencesAsync(string label);
    }
}
