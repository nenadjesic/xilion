using System;

namespace Xilion.Framework.Data
{
    /// <summary>
    /// Encapsulates information about the pageable source.
    /// </summary>
    public class PagerInfo
    {
        private const int DefaultPageNumber = 1;

        /// <summary>
        /// Default page size.
        /// </summary>
        public const int DefaultPageSize = 10;

        /// <summary>
        /// Unpaged 
        /// </summary>
        public static readonly PagerInfo Unpaged = new PagerInfo(DefaultPageNumber, Int32.MaxValue);

        /// <summary>
        /// Initializes a new instance of the <see cref="PagerInfo"/> class for the first page and 
        /// <see cref="DefaultPageSize"/> items.
        /// </summary>
        public PagerInfo() : this(DefaultPageNumber, DefaultPageSize)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PagerInfo"/> class for defined page and default page size.
        /// </summary>
        /// <param name="pageNumber">Curent page number.</param>
        public PagerInfo(int pageNumber)
            : this(pageNumber, DefaultPageSize)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PagerInfo"/> class.
        /// </summary>
        /// <param name="pageNumber">Page number. Page numbers are starting from 1.</param>
        /// <param name="pageSize">Size of the page.</param>
        public PagerInfo(int pageNumber, int pageSize)
        {
            TotalCount = 0;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        /// <summary>
        /// Gets or sets the page number. Page numbers are starting from 1.
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// Gets or sets a single page size.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Gets or sets the total count of items without paging applied.
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Returns total pages.
        /// </summary>
        public int TotalPages
        {
            get { return (int) Math.Ceiling((double) TotalCount / PageSize); }
        }

        /// <summary>
        /// Gets the index of the first item in result set.
        /// </summary>
        internal int StartIndex
        {
            get { return (PageNumber - 1) * PageSize; }
        }
    }
}