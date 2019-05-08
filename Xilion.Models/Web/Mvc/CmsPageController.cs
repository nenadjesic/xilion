using System;
using System.Web.Mvc;
using Xilion.Models.Site.Core;
using Xilion.Models.Web.Extensions;

namespace Xilion.Models.Web.Mvc
{
    public class CmsPageController : Controller
    {
        private readonly PageService _pageService;
        private readonly SiteService _siteService;

        public CmsPageController(PageService pageService, SiteService siteService)
        {
            _pageService = pageService;
            _siteService = siteService;
        }

        public ActionResult Page()
        {
            if (!ControllerContext.IsChildAction)
            {
                var page = _pageService.GetByUrl(Request.Path, _siteService.GetCurrent());
                if (page == null)
                    return View("404");

                var model = new CmsPageContext(CmsPageContextMode.View).Build(page);

                ViewBag.Title = page.Title;

                return View(CmsPageHelper.CmsPagePath(page.GuidID), model);
            }

            throw new CmsWidgetRenderException();
        }
    }
}