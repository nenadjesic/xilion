using Xilion.Models.Core;
using Xilion.Models.Core.Queries;
using Xilion.Framework.Data;
using Xilion.Framework.Queries;

namespace Xilion.Models.User.Core
{
    public class UserQuery : MetaDataQuery<Users>
    {
        private readonly ICmsContext _cmsContext;

        public UserQuery(ICmsContext cmsContext)
            : base(cmsContext)
        {
            _cmsContext = cmsContext;
            Sorting = new SortingInfo(Properties.FirstName, SortOrder.Descending);
            Paging = new PagerInfo(1, Settings.General.PageSize);
            AddProperty(Properties.FirstName).SetIsLocalized(false).SetOperator(QueryOperator.StartsWith).SetList();
            AddProperty(Properties.LastName).SetIsLocalized(false).SetOperator(QueryOperator.StartsWith).SetList();
            AddProperty(Properties.Country).SetIsLocalized(false);
            AddProperty(Properties.FullName).SetIsLocalized(false);
            AddProperty(Properties.Locked);
            AddProperty(Properties.Deactived).SetDefaultValue("false");
            AddProperty(Properties.Roles).SetList();
            AddProperty(Properties.Email).SetIsLocalized(false);
            AddProperty(Properties.LabelAlias);
        }

        private UserSettings Settings
        {
            get { return (UserSettings) _cmsContext.GetApplication<UserApplication>().GetSettings(); }
        }

        public static UserQuery Default
        {
            get { return new UserQuery(CmsContext.Current); }
        }

        public string FirstName
        {
            get { return GetValue<string>(Properties.FirstName); }
            set { SetValue(Properties.FirstName, value); }
        }

        public string Locked
        {
            get { return GetValue<string>(Properties.Locked); }
            set { SetValue(Properties.Locked, value); }
        }

        public string LastName
        {
            get { return GetValue<string>(Properties.LastName); }
            set { SetValue(Properties.LastName, value); }
        }

        public string[] Roles
        {
            get { return GetValue<string[]>(Properties.Roles); }
            set { SetValue(Properties.Roles, value); }
        }

        public string Country
        {
            get { return GetValue<string>(Properties.Country); }
            set { SetValue(Properties.Country, value); }
        }

        public string Email
        {
            get { return GetValue<string>(Properties.Email); }
            set { SetValue(Properties.Email, value); }
        }

        public string Deactived
        {
            get { return GetValue<string>(Properties.Deactived); }
            set { SetValue(Properties.Deactived, value); }
        }

        public string LabelAlias
        {
            get { return GetValue<string>(Properties.LabelAlias); }
            set { SetValue(Properties.LabelAlias, value); }
        }

        #region Nested type: Properties

        private static class Properties
        {
            public const string FirstName = "FirstName";
            public const string LastName = "LastName";
            public const string Country = "Country";
            public const string FullName = "FullName";
            public const string Email = "Email";
            public const string Locked = "Locked";
            public const string Roles = "Roles";
            public const string Labels = "Labels";
            public const string Deactived = "Deactived";
            public const string LabelAlias = "label_alias";
        }

        #endregion
    }
}