using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xilion.Models.Core.Domain;
using Xilion.Models.Site;
using Xilion.Models.Web.Mvc;

namespace Xilion.Models.Web.Extensions
{
    public static class CmsPageContextExtensions
    {
        public static CmsPageContext Build(this CmsPageContext context, Page page)
        {
            if(page == null)
                throw new ArgumentNullException("page");

            context.Alias = page.Alias;
            context.Title = page.Title;

            // Build widgets
            foreach (Widget widget in page.Widgets)
                context.Widgets.Add(new CmsWidgetContext
                                        {
                                            ID = widget.ID.ToString(),
                                            Ordinal = widget.Ordinal,
                                            PageContext = context,
                                            Section = widget.Section,
                                            Title = widget.Title,
                                            RouteData = GetValues(widget.WidgetData)
                                        });

            // Build resources
            foreach (var resource in page.Resources)
                context.Resources.Add(new PageResourceContext(resource.Tag).Build(resource));

            if (page.Parent != null)
                context.Parent = context.Build(page.Parent);

            if (page.Template != null)
            {
                context.Template = new CmsPageTemplateContext { Title = page.Template.Title };
                foreach (Widget widget in page.Template.Widgets)
                    context.Widgets.Add(new CmsWidgetContext
                                            {
                                                ID = widget.ID.ToString(),
                                                Ordinal = widget.Ordinal,
                                                Section = widget.Section,
                                                Title = widget.Title,
                                                PageContext = context,
                                                IsTemplateWidget = true,
                                                RouteData = GetValues(widget.WidgetData)
                                            });
            }

            return context;
        }

        private static RouteValueDictionary GetValues(WidgetData data)
        {
            var values = new RouteValueDictionary();
            foreach (MetaDataProperty property in data.Properties)
            {
                values.Add(property.Name, data.GetValue<string>(property.Name));
            }
            return values;
        }

        public static IList<CmsWidgetContext> AllWidgets(this CmsPageContext context)
        {
            var all = new List<CmsWidgetContext>();
            if (context.Template != null)
                all.AddRange(context.Template.Widgets);
            all.AddRange(context.Widgets);
            return all;
        }

        public static IList<CmsWidgetContext> SectionWidgets(this CmsPageContext context, string section)
        {
            return context.AllWidgets()
                .Where(x => x.Section.Equals(section, StringComparison.InvariantCultureIgnoreCase))
                .OrderBy(x => x.Ordinal).ToList();
        }

        public static IList<PageResourceContext> Scripts(this CmsPageContext context)
        {
            return context.Resources.Where(x => x.ResourceType == PageResourceType.Script).ToList();
        }

        public static IList<PageResourceContext> Styles(this CmsPageContext context)
        {
            return context.Resources.Where(x => x.ResourceType == PageResourceType.Style).ToList();
        }

        public static IList<PageResourceContext> Meta(this CmsPageContext context)
        {
            return context.Resources.Where(x => x.ResourceType == PageResourceType.Meta).ToList();
        }

        public static string Url(this CmsPageContext context)
        {
            return GetUrl(context);
        }

        private static string GetUrl(CmsPageContext context)
        {
            var sb = new StringBuilder();
            const string separator = "/";
            sb.AppendFormat("{0}{1}", separator, context.Alias);
            CmsPageContext parent = context.Parent;
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