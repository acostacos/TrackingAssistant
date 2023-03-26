namespace TrackingAssistantAPI.Shared
{
    internal class ConfigurationSettings : IConfigurationSettings
    {
        private readonly string _connString;
        public string ConnectionString { get { return _connString; } }

        public ConfigurationSettings(IConfiguration configuration)
        {
            _connString = configuration.GetSection("TRACKASSIST_CONNSTRING").Value;
        }
    }
}
