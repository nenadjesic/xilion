using Xilion.Models.Core;
using Xilion.Models.Core.Queries;
using Xilion.Framework.Data;
using Xilion.Framework.Queries;

namespace Xilion.Models.Roles.Core
{
    public class RoleQuery : MetaDataQuery<Role>
    {
        private readonly ICmsContext _cmsContext;

        public RoleQuery(ICmsContext cmsContext)
            : base(cmsContext)
        {
            _cmsContext = cmsContext;
            Sorting = new SortingInfo(Properties.RoleName, SortOrder.Descending);
            Paging = new PagerInfo(1, Settings.General.PageSize);
            AddProperty(Properties.RoleName).SetIsLocalized(false).SetOperator(QueryOperator.StartsWith).SetList();
        }

        private RoleSettings Settings
        {
            get { return (RoleSettings) _cmsContext.GetApplication<RoleApplication>().GetSettings(); }
        }

        public static RoleQuery Default
        {
            get { return new RoleQuery(CmsContext.Current); }
        }

        public string RoleName
        {
            get { return GetValue<string>(Properties.RoleName); }
            set { SetValue(Properties.RoleName, value); }
        }


        #region Nested type: Properties

        private static class Properties
        {
            public const string RoleName = "RoleName";           
        }

        #endregion
    }
}