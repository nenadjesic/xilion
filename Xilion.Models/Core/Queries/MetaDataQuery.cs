using System;
using System.Linq;
using Xilion.Models.Core.Domain;
using Xilion.Framework.Domain;
using Xilion.Framework.Extensions;
using Xilion.Framework.Queries;
using System.Collections.Generic;
using Xilion.Models.Core.Applications;

namespace Xilion.Models.Core.Queries
{
    public class MetaDataQuery<T> : TrackableQuery where T : Entity
    {
        private readonly ICmsContext _cmsContext;

        public MetaDataQuery(ICmsContext cmsContext)
        {
            _cmsContext = cmsContext;
            AddAliasProperties();
        }

        private void AddAliasProperties()
        {
            if (!typeof(T).Implements<IAliased>()) return;
            AddProperty(Properties.Alias);
        }


        public string Alias
        {
            get { return GetValue<string>(Properties.Alias); }
            set { SetValue(Properties.Alias, value); }
        }

        private static class Properties
        {
            public const string Alias = "Alias";
        }
    }
}