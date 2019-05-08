using System;
using System.Collections.Generic;
using System.Linq;
using Xilion.Models.Core;
using Xilion.Models.Core.Services;
using Xilion.Models.Site.Data;
using Xilion.Models.Site.Extensions;

namespace Xilion.Models.Site.Core
{
    public class PageService : CmsService<Page>
    {
        private readonly IPageRepository _pageRepository;

        public PageService(IPageRepository pageRepository) : base(pageRepository)
        {
            _pageRepository = pageRepository;
        }

        /// <summary>
        ///   Gets root page for site.
        /// </summary>
        /// <param name="siteInfo"> SiteInfo to find root page for. </param>
        /// <returns> </returns>
        public Page GetRoot(SiteInfo siteInfo)
        {
            return siteInfo.Root;
        }

        /// <summary>
        ///   Get list of top pages - pages without Parent.
        /// </summary>
        /// <returns> </returns>
        public IEnumerable<Page> GetTopPages()
        {
            return _pageRepository.Query().Where(x => x.Parent == null).OrderBy(x => x.Ordinal);
        }

        public IEnumerable<Page> SortByOrdinal(IEnumerable<Page> tree)
        {
            foreach (var page in tree)
            {
                if(page.HasChildren)
                {
                    page.Children = page.Children.OrderBy(x => x.Ordinal).ToList();

                    SortByOrdinal(page.Children);
                }
            }
            return tree;
        } 

        /// <summary>
        ///   Gets page by its url. It searches page by its alias. If it finds more pages with same alias
        ///   (different parent) than it compares full url to get the right one.
        /// </summary>
        /// <param name="url"> Current url. </param>
        /// <param name="siteInfo"> Site info this page belongs to. </param>
        /// <returns> </returns>
        public Page GetByUrl(string url, SiteInfo siteInfo)
        {
            if (String.IsNullOrEmpty(url))
                url = "";

            if (url.Equals("/", StringComparison.InvariantCultureIgnoreCase))
                return GetRoot(siteInfo);

            var newUrl = PrepareUrl(url);
            var chunks = newUrl.Split('/');
            var query = _pageRepository.Query().Where(x => x.Alias.ToLower() == chunks.Last());

            if (query.Count() > 1)
                query = query.Where(x => x.Url().ToLower() == newUrl);

            return query.FirstOrDefault();
        }

        public PageSettings CurrentUserSettings
        {
            get { return (PageSettings) CmsContext.Current.GetApplication<SiteApplication>().GetUsersettings(); }
        }

        #region Private methods

        private string PrepareUrl(string url)
        {
            var newUrl = url;
            if (newUrl.Contains('?'))
                newUrl = newUrl.Substring(0, newUrl.IndexOf('?'));

            return newUrl.TrimEnd('/').ToLower();
        }

        /// <summary>
        ///   Gets Page by id
        /// </summary>
        public Page GetPageByID(long id)
        {
            return _pageRepository.GetById(id);
        }

        #endregion
    }
}