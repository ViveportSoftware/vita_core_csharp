using System.Threading.Tasks;

namespace Htc.Vita.Core.Preference
{
    public abstract partial class Preferences
    {
        public async Task<bool> CommitAsync()
        {
            return await OnSaveAsync().ConfigureAwait(false);
        }

        public async Task<Preferences> InitializeAsync()
        {
            return await OnInitializeAsync().ConfigureAwait(false);
        }

        protected abstract Task<Preferences> OnInitializeAsync();
        protected abstract Task<bool> OnSaveAsync();
    }
}
