using System;
using Xilion.Framework.Extensions;
using Xilion.Framework.Queries;

namespace Xilion.Models.Karte.Queries
{
    public class KartaQuery : Query
    {
        public KartaQuery()
        {
            SetQ(Properties.Title).SetIsLocalized(true);
            AddProperty(Properties.Category);

        }

        public string Title
        {
            get { return GetValue<string>(Properties.Title); }
            set { SetValue(Properties.Title, value); }
        }


        public string Category
        {
            get { return GetValue<string>(Properties.Category); }
            set { SetValue(Properties.Category, value); }
        }

        #region Nested type: Properties

        public static class Properties
        {
            public const string Title = "MetaData.Title";
            public const string Labels = "Labels";
            public const string Category = "label_alias";
        }

        #endregion
    }
}