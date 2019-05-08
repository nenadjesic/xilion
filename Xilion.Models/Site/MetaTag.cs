namespace Xilion.Models.Site
{
    public class MetaTag : PageResource
    {
        public MetaTag()
        {
            ResourceType = PageResourceType.Meta;
            Tag = "meta";
        }

        public virtual string Charset
        {
            get { return ResourceData.GetValue("Charset"); }
            set { ResourceData.SetValue("Charset", value); }
        }

        public virtual string Content
        {
            get { return ResourceData.GetValue("Content"); }
            set { ResourceData.SetValue("Content", value); }
        }

        public virtual string HttpEquiv
        {
            get { return ResourceData.GetValue("HttpEquiv"); }
            set { ResourceData.SetValue("HttpEquiv", value); }
        }

        public virtual string Name
        {
            get { return ResourceData.GetValue("Name"); }
            set { ResourceData.SetValue("Name", value); }
        }

        public virtual string Scheme
        {
            get { return ResourceData.GetValue("Scheme"); }
            set { ResourceData.SetValue("Scheme", value); }
        }
    }
}