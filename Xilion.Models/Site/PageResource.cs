using Xilion.Models.Core.Domain;
using Xilion.Framework.Domain;

namespace Xilion.Models.Site
{
    /// <summary>
    /// Represents page resources.
    /// </summary>
    public class PageResource : Entity, IOrdered
    {
        private DynamicData _resourceData;
        private string _tag;

        public PageResource()
        {
            _resourceData = new DynamicData();
            _tag = "meta";
        }

        public virtual PageResourceType ResourceType { get; set; }

        public virtual DynamicData ResourceData
        {
            get { return _resourceData; }
            set { _resourceData = value; }
        }

        public virtual string Tag
        {
            get { return _tag; }
            set { _tag = value; }
        }

        public virtual PageResourceScope Scope { get; set; }

        #region Implementation of IOrdered

        public virtual int Ordinal { get; set; }

        #endregion
    }
}