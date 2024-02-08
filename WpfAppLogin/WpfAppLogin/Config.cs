using System.Configuration;

namespace WpfAppLogin
{
    class Config
    {
        public static string GetCurrentConfiguration()
        {
            string url = ConfigurationManager.AppSettings["url"];
            return url;
        }

        public static void ReadConfiguration()
        {
            string url = ConfigurationManager.AppSettings["url"];
            TokenManager.UrlHost = url;
        }

        public static void WriteConfiguration(string url)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["url"].Value = url;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
            ReadConfiguration();
        }
    }
}
