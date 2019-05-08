using System;
using System.Collections.Generic;

namespace Xilion.Models.Filters
{
    public class FilteredDefault : IFiltered
    {
        private IEnumerable<Filter> _filters = new ArraySegment<Filter>();

        #region Implementation of IFiltered

        public IEnumerable<Filter> Filters
        {
            get { return _filters; }
            set { _filters = value; }
        }

        #endregion
    }
}