namespace Xilion.Framework.Data
{
    /// <summary>
    /// Used to store sorting info for the lists
    /// </summary>
    public class SortingInfo
    {
        public static readonly SortingInfo Empty = new SortingInfo(null, SortOrder.Ascending);

        public SortingInfo(string orderByProperty) : this(orderByProperty, SortOrder.Ascending)
        {
        }

        public SortingInfo(string orderByProperty, SortOrder sortOrder)
        {
            OrderByProperty = orderByProperty;
            SortOrder = sortOrder;
        }

        /// <summary>
        /// Property name to be sorted on
        /// </summary>
        public string OrderByProperty { get; set; }

        /// <summary>
        /// Sort order 
        /// </summary>
        public SortOrder SortOrder { get; set; }

        public bool IsAscending
        {
            get { return SortOrder == SortOrder.Ascending; }
        }
    }
}