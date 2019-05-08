using FluentNHibernate.Mapping;
using Xilion.Models.Core.Data.Mappings.Conventions;

namespace Xilion.Models.Core.Data.Mappings
{
    /// <summary>
    /// Mappings for <see cref="Settings.Settings"/> entity.
    /// </summary>
    public class SettingsMap : ClassMap<Settings.Settings>
    {
        /// <summary>
        /// 
        /// </summary>
        public SettingsMap()
        {
            // ReSharper disable DoNotCallOverridableMethodsInConstructor
            Id(x => x.Id);
            // ReSharper restore DoNotCallOverridableMethodsInConstructor
            Map(x => x.ApplicationName);
            Map(x => x.Owner).Nullable();
            Map(x => x.Properties).CustomType(typeof (SettingsPropertiesType));
        }
    }
}