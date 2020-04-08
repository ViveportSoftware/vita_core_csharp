using System.Threading.Tasks;

namespace Htc.Vita.Core.Preference
{
    public abstract partial class Preferences
    {
        /// <summary>
        /// Commits this preferences asynchronously.
        /// </summary>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        public Task<bool> CommitAsync()
        {
            return OnSaveAsync();
        }

        /// <summary>
        /// Initializes this instance asynchronously.
        /// </summary>
        /// <returns>Task&lt;Preferences&gt;.</returns>
        public Task<Preferences> InitializeAsync()
        {
            return OnInitializeAsync();
        }

        /// <summary>
        /// Called when initializing asynchronously.
        /// </summary>
        /// <returns>Task&lt;Preferences&gt;.</returns>
        protected abstract Task<Preferences> OnInitializeAsync();
        /// <summary>
        /// Called when saving asynchronously.
        /// </summary>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        protected abstract Task<bool> OnSaveAsync();
    }
}
