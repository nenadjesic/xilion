using System.Collections.Generic;
using Xilion.Models.Core.Domain;

namespace Xilion.Models.Filters
{
    public class Filter : MetaDataEntity
    {
        private IList<FilterItem> _items = new List<FilterItem>();
        private int _levelLimit = 100;

        public virtual bool Hierarchy { get; set; }

        public virtual int LevelLimit
        {
            get { return _levelLimit; }
            set { _levelLimit = value; }
        }

        public virtual string Title
        {
            get { return MetaData.GetValue<string>("Title"); }
            set { MetaData.SetValueNull("Title", value); }
        }

        public IList<FilterItem> Items
        {
            get { return _items; }
            protected set { _items = value; }
        }
    }
}