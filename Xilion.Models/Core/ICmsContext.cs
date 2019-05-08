using FluentNHibernate.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using Xilion.Models.Core.Applications;
using Xilion.Models.Core.Security;
using Xilion.Models.Core.Settings;

namespace Xilion.Models.Core
{
    /// <summary>
    /// </summary>
    public static class CmsContextExtensions
    {
        /// <summary>
        /// </summary>
        /// <param name="context"> </param>
        /// <typeparam name="TApplication"> </typeparam>
        /// <returns> </returns>
        public static IApplication GetApplication<TApplication>(this ICmsContext context)
            where TApplication : IApplication
        {
            return context.Applications.SingleOrDefault(x => x.GetType() == typeof (TApplication));
        }

        public static IApplication GetApplication(this ICmsContext context, Type applicationType)
        {
            return context.Applications.SingleOrDefault(x => x.GetType() == applicationType);
        }

        public static IApplication GetApplicationFor<TEntity>(this ICmsContext context) where TEntity : Entity
        {
            return GetApplicationFor(context, typeof (TEntity));
        }

        public static IApplication GetApplicationFor(this ICmsContext context, Type entityType)
        {
            var c = context.Applications.SingleOrDefault(x => x.Types.Any(t => t.Type == entityType));
            return c;
        }

       
        public static ISecured GetSecuredApplicationFor<TEntity>(this ICmsContext context) where TEntity : Entity
        {
            return GetApplicationFor<TEntity>(context) as ISecured;
        }

        public static ISecured GetSecuredApplicationFor(this ICmsContext context, Type entityType)
        {
            return GetApplicationFor(context, entityType) as ISecured;
        }

        public static string GetLabelGroupFor<TEntity>(this ICmsContext context) where TEntity : Entity
        {
            return GetLabelGroupFor(context, typeof (TEntity));
        }

        public static string GetLabelGroupFor(this ICmsContext context, Type entityType)
        {
            var application = context.Applications.SingleOrDefault(a => a.Types.Any(x => x.Type == entityType));
            var res = application == null
                          ? String.Empty
                          : application.Types.Single(x => x.Type == entityType).LabelGroup;
            return res;
        }

        public static IList<MetaDataPropertyDefinition> GetMetaDataFor<TEntity>(this ICmsContext context)
            where TEntity : Entity
        {
            return GetMetaDataFor(context, typeof (TEntity));
        }

        public static IList<MetaDataPropertyDefinition> GetMetaDataFor(this ICmsContext context, Type entityType)
        {
            var application =
                context.Applications.SingleOrDefault(a => a.Types.Any(x => x.Type == entityType)) ??
                context.Applications.SingleOrDefault(a => a.Types.Any(x => x.Type.IsAssignableFrom(entityType)));

            return application == null
                       ? new List<MetaDataPropertyDefinition>()
                       : application.Types.Where(x => x.Type == entityType || x.Type.IsAssignableFrom(entityType))
                             .SelectMany(x => x.Properties).ToList();
        }


        public static Permissions GetPermissionsFor<TApplication>(this ICmsContext context)
            where TApplication : IApplication
        {
            var application = GetApplication<TApplication>(context) as ISecured;

            if (application == null)
                throw new CmsException(String.Format(
                    "Application {0} doesn't implement ISecured or is not registered.", typeof(TApplication).Name));

            return application.Permissions;
        }

        public static void SetPermissionsFor<TApplication>(this ICmsContext context, Permissions permissions)
            where TApplication : IApplication
        {
            var application = GetApplication<TApplication>(context) as ISecured;

            if (application == null)
                throw new CmsException(String.Format(
                    "Application {0} doesn't implement ISecured or is not registered.", typeof(TApplication).Name));

            application.Permissions = permissions;
        }

        public static TSettings GetSettings<TSettings, TApplication>(this ICmsContext context)
            where TSettings : ApplicationSettings
            where TApplication : IApplication
        {
            var application = context.Applications.Single(x => x.GetType() == typeof (TApplication));
            return (TSettings) application.GetSettings();
        }
    }
}