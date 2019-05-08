using System.Collections.Generic;
using System.Globalization;

namespace Xilion.Models.Web.Http
{
    public class PageResult<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int Count { get; set; }
    }
}