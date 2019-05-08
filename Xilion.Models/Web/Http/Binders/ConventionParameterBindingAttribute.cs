using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;

namespace Xilion.Models.Web.Http.Binders
{
    public class ConventionParameterBindingAttribute : ModelBinderAttribute
    {
        public override HttpParameterBinding GetBinding(HttpParameterDescriptor parameter)
        {
            return base.GetBinding(parameter);
        }
    }
}