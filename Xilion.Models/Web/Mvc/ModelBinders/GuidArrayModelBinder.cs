using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Xilion.Framework.Extensions;

namespace Xilion.Models.Web.Mvc.ModelBinders
{
    public class GuidArrayModelBinder : IModelBinder
    {
        #region IModelBinder Members

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            ValueProviderResult result = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            string value = result == null ? String.Empty : result.AttemptedValue;

            if (String.IsNullOrWhiteSpace(value))
                return new Guid[0];

            string[] guidValues = value.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
            IEnumerable<Guid> guids = guidValues.Where(x => x.IsGuid()).Select(x => new Guid(x));

            return guids.ToArray();
        }

        #endregion
    }
}