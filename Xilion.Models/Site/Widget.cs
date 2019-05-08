using Xilion.Models.Core.Domain;
using Xilion.Framework.Domain;

namespace Xilion.Models.Site
{
    public class Widget : Entity, IOrdered
    {
        private WidgetData _widgetData;

        public Widget()
        {
            _widgetData = new WidgetData();
        }
        public virtual string Section { get; set; }
        public virtual string Title { get; set; }

        public virtual WidgetData WidgetData
        {
            get { return _widgetData; }
            set { _widgetData = value; }
        }

        #region Implementation of IOrdered

        public virtual int Ordinal { get; set; }
        public virtual Page Page { get; set; }
        public virtual PageTemplate Template { get; set; }

        #endregion
    }
}