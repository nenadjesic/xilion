using Xilion.Framework.Data.Repositories;

namespace Xilion.Models.Core.Data.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISettingsRepository : IRepository<Settings.Settings>
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicationName"></param>
        /// <param name="owner"></param>
        /// <returns></returns>
        Settings.Settings GetByApplicationNameAndOwner(string applicationName, string owner);
    }
}