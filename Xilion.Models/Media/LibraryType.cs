using Xilion.Models.Core.Data.Mappings;
using Xilion.Models.Core.Domain;
using Xilion.Framework.Domain;
using System.Collections.Generic; 

namespace Xilion.Models.Media
{


    public class LibraryType : MetaDataEntity, IAliased
    {

        private IList<Library> _library = new List<Library>();
        /// <summary>
        /// Gets or sets list of Radnika odjeljenja.
        /// </summary>
        public virtual IList<Library> Library
        {
            get { return _library; }
            protected set { _library = value; }
        }
        /// <summary>
        /// Gets or sets item title.
        /// </summary>
        public virtual string Title { get; set; }

        /// <summary>
        /// Gets or sets blog owner.
        /// </summary>
        public virtual Users Owner { get; set; }

        public virtual string Alias { get; set; }

    }
}