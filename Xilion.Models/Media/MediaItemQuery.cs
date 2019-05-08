using System;
using System.Globalization;
using Xilion.Models.Core;
using Xilion.Models.Core.Queries;
using Xilion.Framework;
using Xilion.Framework.Extensions;

namespace Xilion.Models.Media
{
    public abstract class MediaItemQuery : WorkflowQuery<MediaItem>
    {
        protected MediaItemQuery(ICmsContext cmsContext)
            : base(cmsContext)
        {
            AddProperty(Properties.LibraryID);
            AddProperty(Properties.MediaType);
            AddProperty(Properties.Extensions).SetList();
        }

        public virtual string[] Extensions
        {
            get { return GetValue<string[]>(Properties.Extensions); }
            set { SetValue(Properties.Extensions, value); }
        }

        public virtual long? LibraryID
        {
            get
            {
                var value = GetValue<string>(Properties.LibraryID);
                return value.Islong() ? long.Parse(value) : (long?) null;
            }
            set { SetValue(Properties.LibraryID, value == null ? String.Empty : value.Value.ToString()); }
        }

        public virtual MediaType MediaType
        {
            get
            {
                var value = GetValue<string>(Properties.MediaType);
                return String.IsNullOrWhiteSpace(value)
                           ? null
                           : Enumeration.FromValue<MediaType>(Int32.Parse(value));
            }
            set
            {
                SetValue(Properties.MediaType,
                         value == null ? String.Empty : value.Value.ToString(CultureInfo.InvariantCulture));
            }
        }

        public virtual void SetExtensions(string extensions, char separator = ';')
        {
            Extensions = extensions.Replace("*.", String.Empty).Replace(".", String.Empty).Split(separator);
        }

        #region Nested type: Properties

        private static class Properties
        {
            public const string Extensions = "Extension";
            public const string LibraryID = "Library";
            public const string MediaType = "MediaType";
        }

        #endregion
    }
}