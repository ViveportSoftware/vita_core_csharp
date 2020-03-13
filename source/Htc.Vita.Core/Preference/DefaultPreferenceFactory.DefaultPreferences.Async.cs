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

            protected override Task<bool> OnSaveAsync()
            {
                return _storage.SaveToFileAsync(_properties);
            }
        }
    }
}
