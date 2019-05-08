using System.Linq;
using Xilion.Framework.Data;
using Xilion.Framework.Data.Repositories;

namespace Xilion.Models.Core.Data.Repositories.Default
{
    /// <summary>
    /// 
    /// </summary>
    public class SettingsRepository : Repository<Settings.Settings>, ISettingsRepository
    {
        public SettingsRepository(ISessionBuilder sessionBuilder) : base(sessionBuilder)
        {
        }

        #region ISettingsRepository Members

        public Settings.Settings GetByApplicationNameAndOwner(string applicationName, string owner)
        {
            return Query<Settings.Settings>()
                .SingleOrDefault(x => x.ApplicationName == applicationName && x.Owner == owner);
        }

        #endregion
    }
}