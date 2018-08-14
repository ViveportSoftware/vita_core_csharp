using System.Threading.Tasks;

namespace Htc.Vita.Core.Preference
{
    public abstract partial class Preferences
    {
        public Task<bool> CommitAsync()
        {
            return OnSaveAsync();
        }

        public Task<Preferences> InitializeAsync()
        {
            return OnInitializeAsync();
        }

        protected abstract Task<Preferences> OnInitializeAsync();
        protected abstract Task<bool> OnSaveAsync();
    }
}
