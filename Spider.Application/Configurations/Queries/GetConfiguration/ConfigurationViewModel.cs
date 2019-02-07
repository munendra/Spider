using Spider.Application.Configurations.Queries.Models;

namespace Spider.Application.Configurations.Queries.GetConfiguration
{
    public class ConfigurationViewModel
    {
        public ConfigurationViewModel()
        {
            Configuration = new ConfigurationLookUp();
        }

        public ConfigurationLookUp Configuration { get; set; }
    }
}