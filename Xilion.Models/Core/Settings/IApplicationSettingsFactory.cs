namespace Xilion.Models.Core.Settings
{
    public interface IApplicationSettingsFactory
    {
        T GetSettings<T>(SettingsScope scope) where T : ApplicationSettings;
    }
}