using System.Reflection;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Bytecode;
using NHibernate.Cfg;
using NHibernate.Envers.Configuration.Attributes;
using NHibernate.Event;
using NHibernate.Search.Event;
using NHibernate.Search.Store;
using Xilion.Framework.Configuration;
using Xilion.Framework.Data.Search;
using Xilion.Framework.Logging;
using Xilon.Framework.Data.Search;

namespace Xilion.Framework.Data
{
    public class SessionBuilder : ISessionBuilder
    {
        private const string SessionKey = "CurrentNHibernateSession";

        private static readonly ILogger _logger = LogManager.GetLogger<SessionBuilder>();

        private static NHibernate.Cfg.Configuration _configuration;
        private static ISessionFactory _sessionFactory;
        private static ISession _currentSession;
        private static Microsoft.AspNetCore.Http.IHttpContextAccessor _httpContextAccessor;

        #region ISessionBuilder Members

        /// <summary>
        /// Closes the current session object from http context. This should be called on end request.
        /// </summary>
        public void CloseSession()
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext.Session != null)
            {
                _logger.Debug("Closing NHibernate session from the HttpContext.");

                var session = httpContext.Items[SessionKey] as ISession;

                if (session != null)
                {
                    if (session.IsOpen)
                    {
                        session.Close();
                        session.Dispose();
                    }
                    httpContext.Items.Remove(SessionKey);
                }
            }

            if (_currentSession == null) return;
            if (_currentSession.IsOpen)
            {
                _logger.Debug("Closing NHibernate session from the current thread.");

                _currentSession.Close();
                _currentSession.Dispose();
            }
            _currentSession = null;
        }

        /// <summary>
        /// Gets the new session object avoiding the one from HTTP context.
        /// </summary>
        /// <returns><c>NHibernate</c> session object.</returns>
        public ISession GetNewSession()
        {
            _logger.Debug("Getting new session object avoiding the one from HTTP");

            ISessionFactory factory = GetSessionFactory();
            return factory.OpenSession();
        }

        /// <summary>
        /// Gets the session object from current HTTP context or static variable. Creates a new one if none exists.
        /// </summary>
        /// <returns><c>NHibernate</c> session object.</returns>
        public ISession GetSession()
        {
            ISessionFactory factory = GetSessionFactory();
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext.Session != null)
            {
                _logger.Debug("Getting session from HttpContext.");

                var session = httpContext.Items[SessionKey] as ISession;
                if (session == null || !session.IsOpen)
                {
                    session = factory.OpenSession();
                    httpContext.Items.Remove(SessionKey);
                    httpContext.Items.Add(SessionKey, session);
                }

                return session;
            }

            if (_currentSession == null || !_currentSession.IsOpen)
                _currentSession = factory.OpenSession();

            return _currentSession;
        }

        /// <summary>
        /// Gets the stateless session useful for batch inserts or updates.
        /// </summary>
        /// <returns><see cref="NHibernate"/> stateless session object.</returns>
        public IStatelessSession GetStatelessSession()
        {
            ISessionFactory factory = GetSessionFactory();
            return factory.OpenStatelessSession();
        }

        #endregion

        private NHibernate.Cfg.Configuration GetConfiguration()
        {
            if (_configuration == null)
            {
                lock (this)
                {
                    _configuration = Fluently.Configure()
                        .Database(MsSqlConfiguration.MsSql2008
                                      .ConnectionString(ConnectionStringProvider.GetConnectionString())
                                      .AdoNetBatchSize(100))
                        .Cache(c =>
                        {
                            c.UseSecondLevelCache();
                            c.ProviderClass(
                                "NHibernate.Caches.SysCache.SysCacheProvider, NHibernate.Caches.SysCache");
                            c.UseQueryCache();
                        })
                        //.Mappings(m => AddAssemblies(m.FluentMappings))
                        //// TODO: There's a bug in current version of Fluent NHibernate
                        //// When using ExportTo, conventions aren't applied
                        //// https://groups.google.com/forum/#!topic/fluent-nhibernate/8lXr5jlgW-8
                        ////m.FluentMappings
                        ////.Conventions.AddFromAssemblyOf<TableNameConvention>()
                        ////.AddFromAssemblyOf<TableNameConvention>()
                        ////.ExportTo(@"c:\Temp")
                        //.ExposeConfiguration(x => x.SetProperty("hbm2ddl.auto", "update"))
                        //.BuildConfiguration();
                    .Mappings(m =>
                     {
                         var assemblies = new[] { Assembly };
                         var cfg = new AutomappingConfiguration();
                         var persistenceModel = new CustomAutoPersistenceModel(container, cfg);
                         persistenceModel.AddTypeSource(new CombinedAssemblyTypeSource(assemblies.Select(a => new AssemblyTypeSource(a))));

                         m.UsePersistenceModel(persistenceModel);

                         m.HbmMappings.AddFromAssembly(m.FluentMappings);

                         m.AutoMappings.Add(persistenceModel
                             .Conventions.AddFromAssemblyOf<NotNullConvention>()
                             .Conventions.AddFromAssemblyOf<CacheConvention>()
                             .UseOverridesFromAssemblyOf<PCSUserOverrides>());

#if DEBUG
                    var mappingsPath = @"C:\Temp\Mappings";
                    if (Directory.Exists(mappingsPath))
                    {
                        Directory.Delete(mappingsPath, true);
                        Directory.CreateDirectory(mappingsPath);

                        m.AutoMappings.ExportTo(mappingsPath);
                    }
#endif
                     })
                .BuildConfiguration();
                }
            }
            return _configuration;
        }

        [System.Obsolete]
        private ISessionFactory GetSessionFactory()
        {
            if (_sessionFactory == null)
            {
                lock (this)
                {
                    FluentConfiguration configuration = Fluently.Configure(GetConfiguration());
                    configuration.ProxyFactoryFactory<DefaultProxyFactoryFactory>();
                    configuration
                        .ExposeConfiguration(InitializeSearch)
                        .ExposeConfiguration(InitializeEnvers)
                        .ExposeConfiguration(InitializeTrackableListener);

                    ISessionFactory sessionFactory = configuration.BuildSessionFactory();
                    _sessionFactory = new SessionFactorySearchWrapper(sessionFactory);
                }
            }

            return _sessionFactory;
        }

        private static void InitializeEnvers(NHibernate.Cfg.Configuration configuration)
        {
            configuration.SetProperty("nhibernate.envers.audit_table_suffix", "_Audit");
            configuration.SetProperty("nhibernate.envers.revision_field_name", "Revision");
            configuration.SetProperty("nhibernate.envers.revision_type_field_name", "RevisionType");
            // TODO: The following property is set to true, since otherwise Envers tries to save null to all 
            // audit property values when doing delete. Alternative is to allow nulls for all audited columns in db.
            configuration.SetProperty("nhibernate.envers.store_data_at_delete", "true");
            configuration.IntegrateWithEnvers(new AttributeConfiguration());
        }

        private static void InitializeSearch(NHibernate.Cfg.Configuration configuration)
        {
            configuration.SetProperty("hibernate.search.default.directory_provider",
                                      typeof (FSDirectoryProvider).AssemblyQualifiedName);
            // TODO: Make the index folder configurable
            configuration.SetProperty("hibernate.search.default.indexBase", LuceneIndex.IndexLocation);

            configuration.SetListener(ListenerType.PostUpdate, new FullTextIndexEventListener());
            configuration.SetListener(ListenerType.PostInsert, new FullTextIndexEventListener());
            configuration.SetListener(ListenerType.PostDelete, new FullTextIndexEventListener());
            configuration.SetListener(ListenerType.PostCollectionRecreate, new FullTextIndexCollectionEventListener());
            configuration.SetListener(ListenerType.PostCollectionRemove, new FullTextIndexCollectionEventListener());
            configuration.SetListener(ListenerType.PostCollectionUpdate, new FullTextIndexCollectionEventListener());
        }

        private void InitializeTrackableListener(NHibernate.Cfg.Configuration configuration)
        {
            // ReSharper disable CoVariantArrayConversion
            configuration.EventListeners.SaveOrUpdateEventListeners = new[] {new TrackableEntityEventListener()};
            // ReSharper restore CoVariantArrayConversion
        }

        private static void AddAssemblies(FluentMappingsContainer container)
        {
            foreach (Assembly assembly in AssemblyScanner.GetAllReferencingFrameCore())
            {
                _logger.DebugFormat(
                    "Adding persistent assembly '{0}' to Fluent NHibernate mapping scanner.", assembly.FullName);

                container.AddFromAssembly(assembly);
                container.Conventions.AddAssembly(assembly);
            }
        }

        public void CreateDatabase()
        {
            var cfg = GetConfiguration();
            cfg.Configure();
            foreach (Assembly assembly in AssemblyScanner.GetAllReferencingFrameCore())
            {
                _logger.DebugFormat(
                    "Adding persistent assembly '{0}' to Fluent NHibernate mapping scanner.", assembly.FullName);

                cfg.AddAssembly(assembly);
            }
            new NHibernate.Tool.hbm2ddl.SchemaExport(cfg).Execute(false, true, false);
        }
    }
}