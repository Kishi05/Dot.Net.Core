using Microsoft.Extensions.Options;
using Section14.Services.Interface;
using Section14.ViewModels;

namespace Section14.Services
{
    public class Config : IConfig
    {
        private AppSettings appSettings;
        //Consume Injected Config
        public Config(IOptions<AppSettings> options) {
            appSettings = options.Value;
        }
        public AppSettings GetValue()
        {
            return appSettings;
        }
    }
}
