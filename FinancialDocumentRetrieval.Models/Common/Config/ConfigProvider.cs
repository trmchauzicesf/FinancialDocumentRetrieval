using Microsoft.Extensions.Configuration;

namespace FinancialDocumentRetrieval.Models.Common.Config
{
    public static class ConfigProvider
    {
        private static IConfiguration configuration;

        #region ConnectionStrings

        public static string ConnectionString { get; set; }

        #endregion

        #region JwtConfiguration

        public static string JwtKey { get; set; }
        public static int DurationInMinutes { get; set; }

        #endregion

        //TODO Adding rest of config properties from 'appsettings.cs' file
        public static void Setup(IConfiguration config)
        {
            configuration = config;

            #region ConnectionStrings

            IConfigurationSection DatabaseSection = GetSection("Database");
            if (DatabaseSection.Exists())
            {
                ConnectionString = DatabaseSection["ConnectionString"];
            }

            #endregion

            #region JwtConfiguration

            IConfigurationSection JwtConfiguration = GetSection("JwtConfiguration");
            if (JwtConfiguration.Exists())
            {
                JwtKey = JwtConfiguration["JwtKey"];
                DurationInMinutes = Int32.Parse(JwtConfiguration["DurationInMinutes"]);
            }

            #endregion
        }

        public static IConfigurationSection GetSection(string key)
        {
            IConfigurationSection section = configuration.GetSection(key);
            return section;
        }
    }
}