using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xilion.Models.Notifications.Definitions;
using Xilion.Framework;
using Xilion.Framework.Extensions;
using StructureMap;
using StructureMap.Configuration.DSL;
using StructureMap.Pipeline;

namespace Xilion.Models.Notifications
{
    public class NotificationRegistry : Registry
    {
        public NotificationRegistry()
        {
            For<INotificationDefinition>().AddInstances(x =>
                                                            {
                                                                foreach (Type notificationDefinition in GetNotificationDefinitions())
                                                                {
                                                                    x.Type(notificationDefinition).Named(
                                                                        notificationDefinition.Name);
                                                                }
                                                            });
        }

        private static IEnumerable<Type> GetNotificationDefinitions()
        {
            var definitions = new List<Type>();
            foreach (Assembly assembly in AssemblyScanner.GetAllReferencingFrameCore())
                definitions.AddRange(
                    assembly.GetTypes().Where(x => x.Implements<INotificationDefinition>() && !x.IsAbstract && !x.IsInterface));
            return definitions;
        }
    }
}
