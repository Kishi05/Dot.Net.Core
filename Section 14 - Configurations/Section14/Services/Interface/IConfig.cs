using Section14.ViewModels;

namespace Section14.Services.Interface
{
    public interface IConfig
    {
        public AppSettings GetValue();
    }
}
