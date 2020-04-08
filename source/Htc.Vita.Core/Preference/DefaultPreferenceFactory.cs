namespace Htc.Vita.Core.Preference
{
    /// <summary>
    /// Class DefaultPreferenceFactory.
    /// Implements the <see cref="PreferenceFactory" />
    /// </summary>
    /// <seealso cref="PreferenceFactory" />
    public partial class DefaultPreferenceFactory : PreferenceFactory
    {
        /// <inheritdoc />
        protected override Preferences OnLoadPreferences(string label)
        {
            return new DefaultPreferences("", label).Initialize();
        }
    }
}
