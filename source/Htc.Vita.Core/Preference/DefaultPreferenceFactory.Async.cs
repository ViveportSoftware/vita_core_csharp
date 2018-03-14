using System.Threading.Tasks;

namespace Htc.Vita.Core.Preference
{
    public partial class DefaultPreferenceFactory
    {
        protected override async Task<Preferences> OnLoadPreferencesAsync(string label)
        {
            return await new DefaultPreferences("", label).InitializeAsync().ConfigureAwait(false);
        }
    }
}
