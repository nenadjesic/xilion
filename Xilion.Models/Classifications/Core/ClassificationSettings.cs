using System.Collections.Generic;
using Xilion.Models.Core.Data.Repositories;
using Xilion.Models.Core.Settings;

namespace Xilion.Models.Classifications.Core
{
    public class ClassificationSettings : ApplicationSettings
    {
        private readonly IList<Classification> _flatSystemClassifications = new List<Classification>()
                                                                                 {
                                                                                     new Classification()
                                                                                         {
                                                                                             Alias = "tag",
                                                                                             Name = "Tags",
                                                                                             ItemName = "Tag",
                                                                                             ClassificationType = ClassificationType.Flat,
                                                                                             IsSystem = true
                                                                                         }
                                                                                 };

        private readonly IList<Classification> _hierarchySystemClassifications = new List<Classification>()
                                                                                 {
                                                                                     new Classification()
                                                                                         {
                                                                                             Alias = "category",
                                                                                             Name = "Categories",
                                                                                             ItemName = "Categoriy",
                                                                                             ClassificationType = ClassificationType.Hierarchy,
                                                                                             IsSystem = true
                                                                                         },
                                                                                         new Classification()
                                                                                         {
                                                                                             Alias = "team",
                                                                                             Name = "Teams",
                                                                                             ItemName = "Team",
                                                                                             ClassificationType = ClassificationType.Hierarchy,
                                                                                             IsSystem = true
                                                                                         }

                                                                                 };

        public ClassificationSettings(ISettingsRepository repository, string owner)
            : base(repository, ClassificationApplication.ApplicationName, owner)
        {
        }

        public IList<Classification> FlatSystemClassifications
        {
            get { return _flatSystemClassifications; }
        }


        public IList<Classification> HierarchySystemClassifications
        {
            get { return _hierarchySystemClassifications; }
        }
    }
}
