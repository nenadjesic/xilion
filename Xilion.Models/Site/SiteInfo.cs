using Xilion.Models.Core.Domain;

namespace Xilion.Models.Site
{
    public class SiteInfo : MetaDataEntity, IAliased
    {
        public virtual Page Root { get; set; }
        public virtual string Title { get; set; }

        #region Implementation of IAliased

        public virtual string Alias { get; set; }

        #endregion
    }
}