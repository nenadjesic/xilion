using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;

namespace Xilion.Models.Web.Http
{
    /// <summary>
    ///   Custom controller selector uses DependencyResolver to create web api controllers.
    ///   In example bellow custom controller selector will create instance of IArticleRepository implementation.
    ///   <example>
    ///     <code>public class ArticleController {
    ///       private IArticleRepository _repository;
    ///     
    ///       public ArticleController(IArticleRepository repository) {
    ///       _repository = repository.
    ///       }
    ///       }</code>
    ///   </example>
    /// </summary>
    public class CmsControllerActivator : IHttpControllerActivator
    {
        #region Implementation of IHttpControllerActivator

        public IHttpController Create(HttpRequestMessage request,
                                      HttpControllerDescriptor controllerDescriptor,
                                      Type controllerType)
        {
            return DependencyResolver.Current.GetService(controllerType) as IHttpController;
        }

        #endregion
    }
}