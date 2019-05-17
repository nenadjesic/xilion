using Xilion.Models.Core.Data;
using Xilion.Framework.Domain;

namespace Xilion.Models.Core.Domain
{
    /// <summary>
    /// A <see cref="TrackableEntity"/> that contains metadata definition.
    /// </summary>
    public abstract class MetaDataEntity : TrackableEntity, IHaveMetaData
    {
        private MetaData _metaData;

        /// <summary>
        /// Creates a new instance of MetaDataEntity class.
        /// </summary>
        protected MetaDataEntity()
        {
            _metaData = new MetaData(GetType());
        }

        #region IHaveMetaData Members

        /// <summary>
        /// Gets or sets the entity metadata - a collection of dynamic properties that can be localized.
        /// </summary>
        public virtual MetaData MetaData
        {
            get { return _metaData; }
            protected set { _metaData = value; }
        }

        #endregion
    }
}