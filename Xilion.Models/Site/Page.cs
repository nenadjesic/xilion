﻿using System;
using System.Collections.Generic;
using NHibernate.Envers.Configuration.Attributes;
using Xilion.Models.Core.Domain;


namespace Xilion.Models.Site
{

    [Audited]
    public class Page : SiteEntity, IHierarchy, IAliased, IHaveWorkflow
    {
        private bool _allowAnonymous = true;
        private bool _navigable = true;
        private IList<Page> _children = new List<Page>();
        private PageType _pageType;
        private DateTime _publishedOn = DateTime.Now;
        private WorkflowStatus _status = WorkflowStatus.Draft;

        public Page()
        {
            _pageType = PageType.Normal;
        }

        public virtual string MenuName
        {
            get { return MetaData.GetValue<string>("MenuName"); }
            set { MetaData.SetValueNull("MenuName", value); }
        }

        [NotAudited]
        public virtual SiteInfo SiteInfo { get; set; }

        [NotAudited]
        public virtual PageTemplate Template { get; set; }

        [NotAudited]
        public virtual Page Parent { get; set; }

        [NotAudited]
        public virtual string ExternalUrl { get; set; }

        [NotAudited]
        public virtual Page InternalRedirect { get; set; }

        public virtual PageType PageType
        {
            get { return _pageType; }
            set { _pageType = value; }
        }

        public virtual bool Navigable
        {
            get { return _navigable; }
            set { _navigable = value; }
        }

        public virtual bool RequireSSL { get; set; }


        public virtual bool AllowAnonymous
        {
            get { return _allowAnonymous; }
            set { _allowAnonymous = value; }
        }

        [NotAudited]
        public virtual IList<Page> Children
        {
            get { return _children; }
            set { _children = value; }
        }

        public virtual bool HasChildren
        {
            get { return Children.Count > 0; }
        }



        #region IHierarchy Members

        public virtual int Ordinal { get; set; }

        IEnumerable<IHierarchy> IHierarchy.Children
        {
            get { return Children; }
            set { Children = (IList<Page>) value; }
        }

        IHierarchy IHierarchy.Parent
        {
            get { return Parent; }
            set { Parent = (Page) value; }
        }

        #endregion

        #region Implementation of IAliased

        public virtual string Alias { get; set; }

        #endregion

        #region IHaveWorkflow Members

        /// <summary>
        /// Gets or sets the date and time this entity is published.
        /// </summary>
        public virtual DateTime PublishedOn
        {
            get { return _publishedOn; }
            set { _publishedOn = value; }
        }

        /// <summary>
        /// Gets or sets the date and time this entity is expired, or will be expired. If it's null, entity will never 
        /// expire.
        /// </summary>
        public virtual DateTime? ExpiresOn { get; set; }

        /// <summary>
        /// Gets or sets the workflow status of this entity.
        /// </summary>

        public virtual WorkflowStatus Status
        {
            get { return _status; }
            set { _status = value; }
        }

        #endregion
    }
}