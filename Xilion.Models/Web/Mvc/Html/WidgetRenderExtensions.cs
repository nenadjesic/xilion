using System;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Xilion.Models.Core;
using Xilion.Models.Core.Configuration;
using Xilion.Models.Site;
using Xilion.Models.Web.Extensions;

namespace Xilion.Models.Web.Mvc.Html
{
    public static class WidgetRenderExtensions
    {
        private static readonly CmsConfigurationSection _config = CmsConfig.CmsConfigurationSection;

        public static MvcHtmlString RenderCmsWidgets(this HtmlHelper helper, string section, ViewContext viewContext)
        {
            var pageContext = viewContext.ViewData.Model as CmsPageContext;
            if (pageContext == null)
                throw new CmsException();

            var output = new StringBuilder();

            if (CmsPageHelper.IsFooterSection(section))
            {
                RenderFooter(helper, pageContext, output);
            }
            else if (CmsPageHelper.IsHeadSection(section))
            {
                RenderHead(helper, pageContext, output);
            }
            else
            {
                var widgets = pageContext.SectionWidgets(section);

                // Render qusion cms section containers only if edit mode (Page or Template)
                if (pageContext.Mode != CmsPageContextMode.View)
                {
                    output.AppendFormat(
                        widgets.Count > 0
                            ? "\r<div class=\"qusion-cms-section\" id=\"{0}\">"
                            : "\r<div class=\"qusion-cms-section qusion-cms-section-empty\" id=\"{0}\">", section);
                }

                // Render every page widget
                foreach (var widget in widgets)
                    helper.RenderCmsWidget(widget, output);

                // Close section container tag
                if (pageContext.Mode != CmsPageContextMode.View)
                    output.AppendLine("</div>");
            }

            return MvcHtmlString.Create(output.ToString());
        }

        public static MvcHtmlString RenderCmsWidget(this HtmlHelper helper, CmsWidgetContext context,
                                                    StringBuilder output)
        {
            if (context == null)
                throw new CmsException();

            // Render qusion cms widget containers only if edit mode (Page or Template)
            if (context.PageContext.Mode != CmsPageContextMode.View)
                output.AppendFormat(
                    "\r<div class=\"qusion-cms-widget\" data-key=\"{0}\" data-scope=\"{1}\" data-title=\"{2}\">",
                    context.ID,
                    (context.IsTemplateWidget ? "template" : "page"), context.Title);

            // Get action result string
            bool error;
            var html = Action(helper, context, out error);

            // if error occures we display error only if not view
            if (error)
            {
                if (context.PageContext.Mode != CmsPageContextMode.View)
                    output.Append(html);
            }
            else
                output.Append(html);

            // Close widget container tag
            if (context.PageContext.Mode != CmsPageContextMode.View)
                output.AppendLine("</div>");

            return MvcHtmlString.Create(output.ToString());
        }

        private static void RenderHead(HtmlHelper helper, CmsPageContext context, StringBuilder output)
        {
            RenderMeta(helper, context, output);
            RenderStyles(helper, context, output);
            RenderScripts(helper, context, output, PageResourceScope.Head);
        }

        private static void RenderFooter(HtmlHelper helper, CmsPageContext context, StringBuilder output)
        {
            RenderScripts(helper, context, output, PageResourceScope.Body);
        }

        private static void RenderScripts(HtmlHelper helper, CmsPageContext context, StringBuilder output,
                                          PageResourceScope scope)
        {
            foreach (var scripts in context.Scripts().Where(x => x.Scope == scope))
            {
                var tag = new TagBuilder("script");
                tag.MergeAttributes(scripts.Attributes);
                output.AppendLine(tag.ToString());
            }

            // render require script reference with data for page layout editing
            if (scope == PageResourceScope.Body && context.Mode != CmsPageContextMode.View)
            {
                var editScript = new TagBuilder("script");
                editScript.MergeAttribute("type", "text/javascript");
                editScript.MergeAttribute("src", string.Format("{0}/bundles/page", _config.XilionPath));
                output.AppendLine(editScript.ToString());
            }
        }

        private static void RenderStyles(HtmlHelper helper, CmsPageContext context, StringBuilder output)
        {
            foreach (var scripts in context.Styles())
            {
                var tag = new TagBuilder("link");
                tag.MergeAttributes(scripts.Attributes);
                output.AppendLine(tag.ToString(TagRenderMode.SelfClosing));
            }

            // render style reference css page layout editing
            if (context.Mode != CmsPageContextMode.View)
            {
                var editStyle = new TagBuilder("link");
                editStyle.MergeAttribute("rel", "stylesheet");
                editStyle.MergeAttribute("href", String.Format("/areas{0}/style/page.css", _config.XilionPath));
                output.AppendLine(editStyle.ToString(TagRenderMode.SelfClosing));
            }
        }

        private static void RenderMeta(HtmlHelper helper, CmsPageContext context, StringBuilder output)
        {
            foreach (var scripts in context.Meta())
            {
                var tag = new TagBuilder("meta");
                tag.MergeAttributes(scripts.Attributes);
                output.AppendLine(tag.ToString(TagRenderMode.SelfClosing));
            }
        }

        private static MvcHtmlString Action(HtmlHelper helper, CmsWidgetContext context, out bool error)
        {
            error = false;
            var controller = context.RouteData["controller"] as string;
            var action = context.RouteData["action"] as string;

            MvcHtmlString result;
            try
            {
                result = helper.Action(action, controller, context.RouteData);
            }
            catch (CmsWidgetRenderException e)
            {
                error = true;
                result = new MvcHtmlString(String.Format("<div class=\"qusion-cms-widget-error \">{0}</div>", e.Message));
            }
            catch (Exception e)
            {
                error = true;
                result = new MvcHtmlString(String.Format("<div class=\"qusion-cms-widget-error\">{0}</div>", e.Message));
            }

            return result;
        }
    }
}