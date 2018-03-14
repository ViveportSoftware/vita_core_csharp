using System.Threading.Tasks;

namespace Htc.Vita.Core.Preference
{
    public abstract partial class PreferenceFactory
    {
        public async Task<Preferences> LoadPreferencesAsync()
        {
            return await LoadPreferencesAsync("");
        }

        public async Task<Preferences> LoadPreferencesAsync(string label)
        {
            if (string.IsNullOrWhiteSpace(label))
            {
                return await OnLoadPreferencesAsync("default");
            }
            return await OnLoadPreferencesAsync(label);
        }

        protected abstract Task<Preferences> OnLoadPreferencesAsync(string label);
    }
}
