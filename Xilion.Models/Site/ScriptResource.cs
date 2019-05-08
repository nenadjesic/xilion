namespace Xilion.Models.Site
{
    public class ScriptResource : PageResource
    {
        public ScriptResource()
        {
            ResourceType = PageResourceType.Script;
            Tag = "script";
        }

        public virtual string Async
        {
            get { return ResourceData.GetValue("Async"); }
            set { ResourceData.SetValue("Async", value); }
        }

        public virtual string Type
        {
            get { return ResourceData.GetValue("Type"); }
            set { ResourceData.SetValue("Type", value); }
        }

        public virtual string Src
        {
            get { return ResourceData.GetValue("Src"); }
            set { ResourceData.SetValue("Src", value); }
        }

        public virtual string Charset
        {
            get { return ResourceData.GetValue("Charset"); }
            set { ResourceData.SetValue("Charset", value); }
        }

        public virtual string Defer
        {
            get { return ResourceData.GetValue("Defer"); }
            set { ResourceData.SetValue("Defer", value); }
        }
    }
}