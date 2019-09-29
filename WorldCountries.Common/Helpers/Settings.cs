using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace WorldCountries.Common.Helpers
{
    public class Settings
    {
        private const string _countries = "Countries";
        private static readonly string _stringDefault = string.Empty;

        private static ISettings AppSettings => CrossSettings.Current;

        public static string Countries
        {
            get => AppSettings.GetValueOrDefault(_countries, _stringDefault);
            set => AppSettings.AddOrUpdateValue(_countries, value);
        }
    }
}
