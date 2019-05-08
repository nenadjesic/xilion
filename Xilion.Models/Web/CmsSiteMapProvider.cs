using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xilion.Models.Localization;
using Xilion.Models.Site;
using Xilion.Models.Site.Core;
using Xilion.Framework.Data;

namespace Xilion.Models.Web
{
    public class CmsSiteMapProvider : SiteMapProvider
    {
        internal readonly object Lock;
        private readonly IDictionary<string, SiteMapNodeCollection> _mapChildren;
        private readonly IDictionary<string, SiteMapNode> _mapParents;
        private readonly IDictionary<string, SiteMapNode> _mapUrls;
        private readonly SiteService _siteService;
        private SiteMapNode _rootNode;

        public CmsSiteMapProvider()
            : this(DependencyResolver.Current.GetService<SiteService>())
        {
        }

        public CmsSiteMapProvider(SiteService siteService)
        {
            _siteService = siteService;
            Lock = new object();

            _mapUrls = new Dictionary<string, SiteMapNode>();
            _mapChildren = new Dictionary<string, SiteMapNodeCollection>();
            _mapParents = new Dictionary<string, SiteMapNode>();

            EnableLocalization = true;
        }

        public override void Initialize(string name, NameValueCollection attributes)
        {
            base.Initialize(name, attributes);
            CacheDependency.Current.Subscribe(typeof (Page), OnPageChanged);
        }

        public override SiteMapNode FindSiteMapNode(string rawUrl)
        {
            if (rawUrl == null)
                throw new ArgumentNullException("rawUrl");

            CreateSiteMap();

            return _mapUrls.SingleOrDefault(x => x.Value.Url == PrepareUrl(rawUrl)).Value;
        }

        private string PrepareUrl(string url)
        {
            return url.Replace(LocalizationManager.CurrentContentCulture.Name, "").Trim('/');
        }

        public override SiteMapNodeCollection GetChildNodes(SiteMapNode node)
        {
            CreateSiteMap();
            return _mapChildren.ContainsKey(node.Key) ? _mapChildren[node.Key] : null;
        }

        public override SiteMapNode GetParentNode(SiteMapNode node)
        {
            CreateSiteMap();
            return _mapParents.ContainsKey(node.Key) ? _mapParents[node.Key] : null;
        }

        protected override SiteMapNode GetRootNodeCore()
        {
            CreateSiteMap();
            return _rootNode;
        }

        public override bool IsAccessibleToUser(HttpContext context, SiteMapNode node)
        {
            var mapNode = node as CmsSiteMapNode;
            if (mapNode != null)
            {
                Page page = mapNode.Page;
                if (!page.AllowAnonymous)
                {
                }
            }
            return base.IsAccessibleToUser(context, node);
        }

        /// <summary>
        ///   Clears the cache and reloads the SiteInfo map.
        /// </summary>
        public void Refresh()
        {
            Clear();
        }

        /// <summary>
        ///   Checks the page visibility regarding to the current user and page properties
        /// </summary>
        /// <param name = "page">SiteInfo page</param>
        /// <returns>Boolean value indicates whether this page is accessible or not</returns>
        protected virtual bool CheckPageVisibility(Page page)
        {
            //if (!Equals(page.Status, WorkflowStatus.Live))
            //    return false;
            if (!page.Navigable)
                return false;
            if (!page.AllowAnonymous && !HttpContext.Current.User.Identity.IsAuthenticated)
                return false;

            return true;
        }

        /// <summary>
        ///   This will clear all cached SiteInfo map values so next time requested SiteInfo map will be rebuilt
        /// </summary>
        protected virtual void Clear()
        {
            lock (Lock)
            {
                if (_mapChildren != null)
                    _mapChildren.Clear();
                if (_mapUrls != null)
                    _mapUrls.Clear();
                if (_mapParents != null)
                    _mapParents.Clear();

                _rootNode = null;
            }
        }

        private void OnPageChanged(object sender, EventArgs e)
        {
            Clear();
        }

        private void CreateSiteMap()
        {
            if (_rootNode != null)
                return;
            lock (Lock)
            {
                SiteInfo site = _siteService.GetCurrent();
                _rootNode = new CmsSiteMapNode(this, site.Root);

                AddPageNode(_rootNode, null);

                foreach (Page page in site.Root.Children)
                {
                    if (!CheckPageVisibility(page))
                        continue;

                    var node = new CmsSiteMapNode(this, page);
                    AddPageNode(node, _rootNode);
                    CreateSiteMap(node, page);
                }
            }
        }

        private void CreateSiteMap(CmsSiteMapNode parentNode, Page parentPage)
        {
            foreach (Page page in parentPage.Children.OrderBy(p => p.Ordinal))
            {
                if (!page.Navigable)
                    continue;
                var node = new CmsSiteMapNode(this, page);
                AddPageNode(node, parentNode);
                CreateSiteMap(node, page);
            }
        }

        private void AddPageNode(SiteMapNode node, SiteMapNode parent)
        {
            if (node == null)
                throw new ArgumentNullException("node");

            lock (Lock)
            {
                _mapUrls[node.Key] = node;

                if (parent == null)
                    return;

                _mapParents[node.Key] = parent;
                if (!_mapChildren.ContainsKey(parent.Key))
                    _mapChildren[parent.Key] = new SiteMapNodeCollection();

                _mapChildren[parent.Key].Add(node);
            }
        }
    }
}