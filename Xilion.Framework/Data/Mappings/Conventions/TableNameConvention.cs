using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace Xilion.Framework.Data.Mappings.Conventions
{
    public class TableNameConvention : IClassConvention
    {
        static TableNameConvention()
        {
            Prefix = "Xilion_";
        }

        public static string Prefix { get; set; }

        #region IClassConvention Members

        /// <summary>
        /// Apply changes to the target
        /// </summary>
        public void Apply(IClassInstance instance)
        {
            instance.Table(Prefix + instance.EntityType.Name);
        }

        #endregion
    }
}