using NHibernate.Envers.Configuration.Attributes;
using Xilion;
using Xilion.Framework.Domain;
using System;
using Xilion.Models.Core.Domain;
using Xilion.Models.Core.Data;
using Xilion.Models.Articles;
using Xilion.Models.Core;


namespace Xilion.Models.Media
{
    /// <summary>
    /// Represents media item collection.
    /// </summary>
    public partial class Library : MetaDataEntity
    {
        private WorkflowStatus _status = WorkflowStatus.Draft;
        /// <summary>
        /// Gets or sets library scope.
        /// </summary>
        public virtual LibraryScope LibraryScope { get; set; }
        /// <summary>
        //[NotNull]
        public virtual Article Article { get; set; }
        /// <summary>
        /// Gets or sets item title.
        /// </summary>
        public virtual string Title { get; set; }

        /// <summary>
        ///   Gets or sets  content.
        /// </summary>
        public virtual string Content { get; set; }
    
        /// <summary>
        /// Gets or sets library application name. 
        /// This property is used only if LibraryScope is Application.
        /// </summary>
        public virtual string ApplicationName { get; set; }

        /// <summary>
        /// Gets or sets library media type. 
        /// </summary>
        public virtual MediaType Type { get; set; }

        /// <summary>
        ///   Gets or sets tipovi dokuemenata.
        /// </summary>

        public virtual LibraryType LibraryType { get; set; }

        /// <summary>
        /// Gets or sets the workflow status of this entity.
        /// </summary>

        public virtual WorkflowStatus Status
        {
            get { return _status; }
            set { _status = value; }
        }

        /// <summary>
        ///   Gets or sets date and time when article is published.
        /// </summary>

        public virtual DateTime ArDatum { get; set; }
    }
}