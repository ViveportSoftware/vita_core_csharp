namespace Htc.Vita.Core.Preference
{
    public partial class DefaultPreferenceFactory : PreferenceFactory
    {
        protected override Preferences OnLoadPreferences(string label)
        {
            return new DefaultPreferences("", label).Initialize();
        }
    }
}
