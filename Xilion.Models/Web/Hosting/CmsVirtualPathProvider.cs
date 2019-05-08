using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.Caching;
using System.Web.Hosting;
using Xilion.Models.Site;
using Xilion.Models.Site.Core;
using Xilion.Models.Web.Mvc;

namespace Xilion.Models.Web.Hosting
{
    public class CmsVirtualPathProvider : VirtualPathProvider
    {
        private readonly PageService _pageService;

        public CmsVirtualPathProvider(PageService pageService)
        {
            _pageService = pageService;
        }

        public override bool FileExists(string virtualPath)
        {
            if (IsPathVirtual(virtualPath))
            {
                bool exists = ((CmsPageVirtualFile) GetFile(virtualPath)).Exists;
                if (exists)
                    return true;
                return Previous.FileExists(virtualPath);
            }
            return Previous.FileExists(virtualPath);
        }

        public override bool DirectoryExists(string virtualDir)
        {
            return IsPathVirtual(virtualDir) || Previous.DirectoryExists(virtualDir);
        }

        public override VirtualFile GetFile(string virtualPath)
        {
            if (IsPathVirtual(virtualPath))
                return new CmsPageVirtualFile(virtualPath, _pageService);
            return Previous.GetFile(virtualPath);
        }

        public override VirtualDirectory GetDirectory(string virtualDir)
        {
            if (IsPathVirtual(virtualDir))
                return new CmsPageVirtualDirectory(virtualDir, _pageService);
            return Previous.GetDirectory(virtualDir);
        }

        public override CacheDependency GetCacheDependency(string virtualPath,
                                                           IEnumerable virtualPathDependencies,
                                                           DateTime utcStart)
        {
            return IsPathVirtual(virtualPath)
                       ? null
                       : Previous.GetCacheDependency(virtualPath, virtualPathDependencies, utcStart);
        }

        private bool IsPathVirtual(string virtualPath)
        {
            String checkPath = VirtualPathUtility.ToAppRelative(virtualPath);
            var isVirtual = checkPath.StartsWith(CmsPageHelper.CmsBasePath.ToLower(), StringComparison.InvariantCultureIgnoreCase);
            return isVirtual;
        }

        #region Nested type: CmsPageVirtualDirectory

        private class CmsPageVirtualDirectory : VirtualDirectory
        {
            private readonly PageService _pageService;

            private readonly ArrayList _children = new ArrayList();
            private readonly ArrayList _directories = new ArrayList();
            private readonly ArrayList _files = new ArrayList();

            public CmsPageVirtualDirectory(string virtualDirectory, PageService pageService)
                : base(virtualDirectory)
            {
                _pageService = pageService;
            }

            public override IEnumerable Children
            {
                get { return _children; }
            }

            public override IEnumerable Directories
            {
                get { return _directories; }
            }

            public override IEnumerable Files
            {
                get { return _files; }
            }
        }

        #endregion

        #region Nested type: CmsPageVirtualFile

        private class CmsPageVirtualFile : VirtualFile
        {
            private string _content = String.Empty;
            private readonly PageService _pageService;
            private readonly string _virtualPath;

            public CmsPageVirtualFile(string virtualPath, PageService pageService) : base(virtualPath)
            {
                _virtualPath = virtualPath;
                _pageService = pageService;
                BuildContent();
            }

            #region Overrides of VirtualFile

            public override Stream Open()
            {
                Stream stream = new MemoryStream();
                if (_content != null && !_content.Equals(String.Empty))
                {
                    // Put the page content on the stream.
                    var writer = new StreamWriter(stream);
                    writer.Write(_content);
                    writer.Flush();
                    stream.Seek(0, SeekOrigin.Begin);
                }
                return stream;
            }

            #endregion

            public bool Exists
            {
                get { return !String.IsNullOrEmpty(_content); }
            }

            private void BuildContent()
            {
                Guid id = ParseForGuid(_virtualPath);
                if (id != Guid.Empty)
                {
                    Page page = _pageService.GetById(id);
                    _content += "@using Xilion.Models.Web.Mvc.Html \r\n";
                    _content += "@model Xilion.Models.Web.Mvc.CmsPageContext \r\n";
                    _content += page.Template != null ? page.Template.Content : DefaultContent();
                }
                else
                    _content = string.Empty;

                // prepare content by adding head and foot scripts.
                ParepareContent();
            }

            private string DefaultContent()
            {
                return "<!DOCTYPE html>"+
                        "<html>"+
                        "<head>"+
                            "<meta charset=\"utf-8\" />"+
                            "<meta name=\"viewport\" content=\"width=device-width\" />"+
                            "<title>@ViewBag.Title</title>"+
                        "</head>"+
                        "<body>"+
                            "<div id=\"body\">"+
	                        "@Html.RenderCmsWidgets(\"Body\", ViewContext)"+
                            "</div>"+
                        "</body>"+
                        "</html>";
            }

            // TODO: it doesn't work for RenderSection that doesn't contain required part
            private void ParepareContent()
            {
                _content = TemplateService.ParseRazorLayout(_content);
                //_content = _content.Replace("@RenderBody()", "@RenderSection(\"Body\", required: true)");
                //var regex =
                //    new Regex(
                //        "@(\\{[\\s\\r\\n]*)?\\(?RenderSection\\s*\\(\\s*\"(?<name>[^\\\"]*)\"\\s*(,\\s*(required\\s*:?)?\\s*(?<required>true|false)\\s*)\\)\\)?([;\\r\\n\\s]*\\})?",
                //        RegexOptions.IgnoreCase);
                //foreach (Match match in regex.Matches(_content))
                //{
                //    string section = match.Groups["name"].Value;
                //    _content = _content.Replace(match.Value,
                //                              "@Html.RenderCmsWidgets(\"" + section + "\", ViewContext)");
                //}

                var regexHead = new Regex(CmsPageHelper.CmsPageHeaderSection, RegexOptions.IgnoreCase);
                if (!regexHead.IsMatch(_content))
                    _content = _content.Replace("</head>",
                                          String.Format("@Html.RenderCmsWidgets(\"{0}\", ViewContext)\n</head>", CmsPageHelper.CmsPageHeaderSection));

                var regexFoot = new Regex(CmsPageHelper.CmsPageFooterSection, RegexOptions.IgnoreCase);
                if (!regexFoot.IsMatch(_content))
                    _content = _content.Replace("</body>",
                                          String.Format("@Html.RenderCmsWidgets(\"{0}\", ViewContext)\n</body>", CmsPageHelper.CmsPageFooterSection));
            }

            private Guid ParseForGuid(string input)
            {
                string str = Path.GetFileNameWithoutExtension(input);
                if(str != null && str.Contains("."))
                {
                    str = str.Split('.')[0];
                }
                Guid id;
                Guid.TryParse(str, out id);
                return id;
            }
        }

        #endregion
    }
}