using System.Threading.Tasks;

namespace Htc.Vita.Core.Preference
{
    public abstract partial class PreferenceFactory
    {
        public Task<Preferences> LoadPreferencesAsync()
        {
            return LoadPreferencesAsync("");
        }

        public Task<Preferences> LoadPreferencesAsync(string label)
        {
            if (string.IsNullOrWhiteSpace(label))
            {
                return OnLoadPreferencesAsync("default");
            }
            return OnLoadPreferencesAsync(label);
        }

        protected abstract Task<Preferences> OnLoadPreferencesAsync(string label);
    }
}
