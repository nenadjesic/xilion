using System;
using System.Web.Mvc;
using Xilion.Models.Core;
using Xilion.Framework;

namespace Xilion.Models.Web.Mvc.ModelBinders
{
    public class EnumerationModelBinder : IModelBinder
    {
        #region IModelBinder Members

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            try
            {
                ValueProviderResult value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
                string enumerationValue = value == null ? String.Empty : value.AttemptedValue;

                if (enumerationValue == String.Empty)
                    return null;

                Enumeration enumeration = GetEnumeration(bindingContext.ModelType, enumerationValue);
                return enumeration;
            }
            catch (Exception ex)
            {
                throw new CmsException(
                    String.Format("Unable to locate a valid value for enumeration query string parameter '{0}'",
                                  bindingContext.ModelName), ex);
            }
        }

        #endregion

        private static Enumeration GetEnumeration(Type enumerationType, string value)
        {
            int enumValue;
            bool isEnumerationValueBinding = int.TryParse(value, out enumValue);

            return isEnumerationValueBinding
                       ? Enumeration.FromValue(enumerationType, enumValue)
                       : Enumeration.FromDisplayName(enumerationType, value);
        }
    }
}