using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Xilion.Framework;
using StructureMap;
using System.Web.Mvc;

namespace Xilion.Models.Core.Configuration
{
    /// <summary>
    /// Configures the AutoMapper library.
    /// </summary>
    public static class AutoMapperConfiguration
    {
        private static Container _container;
        /// <summary>
        /// Configure AutoMapper.
        /// </summary>
        public static void Configure()
        {
            Mapper.Initialize(
                x =>
                    {
                        x.ConstructServicesUsing(DependencyResolver.Current.GetService);
                        foreach (Type profile in GetProfiles())
                            x.AddProfile((Profile) _container.GetInstance(profile));
                    });
        }

        private static IEnumerable<Type> GetProfiles()
        {
            var profiles = new List<Type>();
            foreach (Assembly assembly in AssemblyScanner.GetAllReferencingFrameCore())
                profiles.AddRange(assembly.GetTypes().Where(x => !x.IsAbstract && typeof (Profile).IsAssignableFrom(x)));
            return profiles;
        }
    }
}