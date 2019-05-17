using System.Collections.Generic;
using NHibernate.Envers.Configuration.Attributes;
using Xilion.Models.Core.Domain;

namespace Xilion.Models.Site
{
    public abstract class SiteEntity : MetaDataEntity
    {
        private IList<PageResource> _resources = new List<PageResource>();
        private IList<Widget> _widgets = new List<Widget>();

        [NotAudited]
        public virtual IList<Widget> Widgets
        {
            get { return _widgets; }
            set { _widgets = value; }
        }

        public virtual string Title
        {
            get { return MetaData.GetValue<string>("Title"); }
            set { MetaData.SetValueNull("Title", value); }
        }

        public virtual string Description
        {
            get { return MetaData.GetValue<string>("Description"); }
            set { MetaData.SetValueNull("Description", value); }
        }

        [NotAudited]
        public virtual IList<PageResource> Resources
        {
            get { return _resources; }
            set { _resources = value; }
        }
    }
}