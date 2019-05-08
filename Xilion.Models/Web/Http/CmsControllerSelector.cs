using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;

namespace Xilion.Models.Web.Http
{
    /// <summary>
    /// Custom controller selector uses DependencyResolver to select web api controllers.
    /// </summary>
    public class CmsControllerSelector : DefaultHttpControllerSelector
    {
        private readonly HttpConfiguration _configuration;

        public CmsControllerSelector(HttpConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            HttpControllerDescriptor controller = base.SelectController(request);
            var descriptor = new HttpControllerDescriptor(_configuration, GetControllerName(request),
                                                          controller.ControllerType);
            return DependencyResolver.Current.GetService(descriptor.GetType()) as HttpControllerDescriptor;
        }
    }
}