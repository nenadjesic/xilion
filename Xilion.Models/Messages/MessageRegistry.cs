using System;
using System.Collections.Generic;
using System.Linq;
using Xilion.Models.Messages.Domain;
using Xilion.Framework;
using Xilion.Framework.Extensions;
using StructureMap.Configuration.DSL;
using StructureMap;

namespace Xilion.Models.Messages
{
    public class MessageRegistry : Registry
    {
        public MessageRegistry()
        {
            For<IAttachmentProvider>().AddInstances(x =>
                                                        {
                                                            foreach (var attachmentProvider in GetAttachmentProviders())
                                                            {
                                                                x.Type(attachmentProvider).Named(attachmentProvider.Name);
                                                            }
                                                        });
        }

        private static IEnumerable<Type> GetAttachmentProviders()
        {
            var definitions = new List<Type>();
            foreach (var assembly in AssemblyScanner.GetAllReferencingFrameCore())
                definitions.AddRange(
                    assembly.GetTypes().Where(
                        x => x.Implements<IAttachmentProvider>() && !x.IsAbstract && !x.IsInterface));
            return definitions;
        }
    }
}