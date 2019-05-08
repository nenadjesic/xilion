using System;

namespace Xilion.Models.Web.Mvc
{
    public class CmsPageHelper
    {
        public static string CmsBasePath = "~/qusion.net";
        public static string CmsPagePath(Guid id)
        {
            return String.Format("{0}/page/{1}.cshtml", CmsBasePath, id);
        }
        
        public static string CmsPageHeaderSection = "Xilion:Head";
        public static string CmsPageFooterSection = "Xilion:Footer";

        public static bool IsCmsSection(string section)
        {
            if(String.IsNullOrWhiteSpace(section)) return false;
            return IsHeadSection(section) || IsFooterSection(section);
        }

        public static bool IsHeadSection(string section)
        {
            if(String.IsNullOrWhiteSpace(section)) return false;
            return section.ToLower() == CmsPageHeaderSection.ToLower();
        }

        public static bool IsFooterSection(string section)
        {
            if(String.IsNullOrWhiteSpace(section)) return false;
            return section.ToLower() == CmsPageFooterSection.ToLower();
        }
    }
}