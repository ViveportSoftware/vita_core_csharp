using System.Threading.Tasks;

namespace Htc.Vita.Core.Preference
{
    public abstract partial class Preferences
    {
        public async Task<bool> CommitAsync()
        {
            return await OnSaveAsync();
        }

        public async Task<Preferences> InitializeAsync()
        {
            return await OnInitializeAsync();
        }

        protected abstract Task<Preferences> OnInitializeAsync();
        protected abstract Task<bool> OnSaveAsync();
    }
}
