using Xilion.Models.Site;
using Xilion.Models.Web.Mvc;

namespace Xilion.Models.Web.Extensions
{
    public static class PageResourceContextExtensions
    {
         public static PageResourceContext Build(this PageResourceContext context, PageResource resource)
         {
             context.ResourceType = resource.ResourceType;
             context.Scope = resource.Scope;
             foreach (var property in resource.ResourceData.Properties)
             {
                 context.Attributes.Add(property.Name.ToLower(), property.Value);
             }
             return context;
         }
    }
}