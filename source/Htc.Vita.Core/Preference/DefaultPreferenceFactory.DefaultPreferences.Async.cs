using System.Threading.Tasks;

namespace Htc.Vita.Core.Preference
{
    public partial class DefaultPreferenceFactory
    {
        public partial class DefaultPreferences
        {
            /// <inheritdoc />
            protected override async Task<Preferences> OnInitializeAsync()
            {
                _properties = await _storage.LoadFromFileAsync().ConfigureAwait(false);
                return this;
            }

            /// <inheritdoc />
            protected override Task<bool> OnSaveAsync()
            {
                return _storage.SaveToFileAsync(_properties);
            }
        }
    }
}
