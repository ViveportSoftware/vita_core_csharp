using System.Threading.Tasks;

namespace Htc.Vita.Core.Preference
{
    public partial class DefaultPreferenceFactory
    {
        public partial class DefaultPreferences
        {
            protected override async Task<Preferences> OnInitializeAsync()
            {
                _properties = await _storage.LoadFromFileAsync().ConfigureAwait(false);
                return this;
            }

            protected override async Task<bool> OnSaveAsync()
            {
                return await _storage.SaveToFileAsync(_properties).ConfigureAwait(false);
            }
        }
    }
}
