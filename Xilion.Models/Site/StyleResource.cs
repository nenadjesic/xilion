namespace Xilion.Models.Site
{
    public class StyleResource : PageResource
    {
        public StyleResource()
        {
            ResourceType = PageResourceType.Style;
            Tag = "link";
        }

        public virtual string Charset
        {
            get { return ResourceData.GetValue("Charset"); }
            set { ResourceData.SetValue("Charset", value); }
        }

        public virtual string Href
        {
            get { return ResourceData.GetValue("Href"); }
            set { ResourceData.SetValue("Href", value); }
        }

        public virtual string HrefLang
        {
            get { return ResourceData.GetValue("HrefLang"); }
            set { ResourceData.SetValue("HrefLang", value); }
        }

        public virtual string Rel
        {
            get { return ResourceData.GetValue("Rel"); }
            set { ResourceData.SetValue("Rel", value); }
        }

        public virtual string Target
        {
            get { return ResourceData.GetValue("Target"); }
            set { ResourceData.SetValue("Target", value); }
        }

        public virtual string Type
        {
            get { return ResourceData.GetValue("Type"); }
            set { ResourceData.SetValue("Type", value); }
        }

        public virtual string Sizes
        {
            get { return ResourceData.GetValue("Sizes"); }
            set { ResourceData.SetValue("Sizes", value); }
        }
    }
}