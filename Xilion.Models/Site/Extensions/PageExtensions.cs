using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xilion.Models.Site.Extensions
{
    public static class PageExtensions
    {
        public static IList<Widget> AllWidgets(this Page page)
        {
            var all = new List<Widget>();
            if (page.Template != null)
                all.AddRange(page.Template.Widgets);
            all.AddRange(page.Widgets);
            return all;
        }

        public static IList<Widget> SectionWidgets(this Page page, string section)
        {
            return page.AllWidgets()
                .Where(x => x.Section.Equals(section, StringComparison.InvariantCultureIgnoreCase))
                .OrderBy(x => x.Ordinal).ToList();
        }

        public static IList<ScriptResource> Scripts(this Page page)
        {
            return page.Resources<ScriptResource>(PageResourceType.Script);
        }

        public static IList<StyleResource> Styles(this Page page)
        {
            return page.Resources<StyleResource>(PageResourceType.Style);
        }

        public static IList<MetaTag> Meta(this Page page)
        {
            return page.Resources<MetaTag>(PageResourceType.Meta);
        }

        public static IList<T> Resources<T>(this Page page, PageResourceType type) where T : PageResource
        {
            return page.Resources.Where(x => x.ResourceType == type).OrderBy(x => x.Ordinal).Cast<T>().ToList();
        }

        public static string Url(this Page page)
        {
            if (Equals(page.PageType, PageType.External))
                return page.ExternalUrl;

            if (Equals(page.PageType, PageType.InternalRedirect) && page.InternalRedirect != null)
                return GetUrl(page.InternalRedirect);

            return GetUrl(page);
        }

        private static string GetUrl(Page page)
        {
            var sb = new StringBuilder();
            const string separator = "/";
            sb.AppendFormat("{0}{1}", separator, page.Alias);
            Page parent = page.Parent;
            while (parent != null)
            {
                sb.Insert(0, String.Format("{0}{1}", separator, parent.Alias));
                parent = parent.Parent;
            }
            string url = sb.ToString();
            return url.EndsWith("/") ? url : string.Concat(url, "/");
        }
    }
}