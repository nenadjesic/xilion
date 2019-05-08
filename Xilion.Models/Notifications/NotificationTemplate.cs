using System;
using System.Globalization;
using System.IO;
using System.Web.Mvc;
using Xilion.Models.Notifications.Definitions;
using Xilion.Models.Notifications.Domain;

namespace Xilion.Models.Notifications
{
    public class NotificationTemplate
    {
        public static string ExecuteTemplate(ControllerContext context, INotificationDefinition definition,
                                             NotificationState state)
        {
            string name = definition.GetType().Name;
            string fullViewName = "NotificationTemplate/" + name;
            Type modelType = typeof (NotificationModel<>);
            Type[] typeArgs = {definition.GetType()};
            Type g = modelType.MakeGenericType(typeArgs);
            object o = Activator.CreateInstance(g);
            g.GetProperty("Definition").SetValue(o, definition, null);
            g.GetProperty("State").SetValue(o, state, null);

            context.Controller.ViewData.Model = o;
            string html = string.Empty;
            ViewEngineResult viewEngineResult = ViewEngines.Engines.FindPartialView(context, fullViewName);
            if (viewEngineResult.View != null)
            {
                using (var writer = new StringWriter(CultureInfo.InvariantCulture))
                {
                    viewEngineResult.View.Render(
                        new ViewContext(context, viewEngineResult.View, context.Controller.ViewData,
                                        context.Controller.TempData, writer), writer);
                    html = writer.ToString();
                }
            }
            return html;
        }
    }
}
