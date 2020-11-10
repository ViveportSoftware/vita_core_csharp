using System.Collections.Generic;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Config
{
    /// <summary>
    /// Class DummyConfigV2.
    /// Implements the <see cref="ConfigV2" />
    /// </summary>
    /// <seealso cref="ConfigV2" />
    public class DummyConfigV2 : ConfigV2
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DummyConfigV2"/> class.
        /// </summary>
        public DummyConfigV2()
        {
            Logger.GetInstance(typeof(ConfigV2)).Error($"You are using dummy {typeof(ConfigV2)} instance!!");
        }

        /// <inheritdoc />
        protected override ISet<string> OnAllKeys()
        {
            return null;
        }

        /// <inheritdoc />
        protected override bool OnHasKey(string key)
        {
            return false;
        }

        /// <inheritdoc />
        protected override string OnGet(string key)
        {
            return null;
        }
    }
}
