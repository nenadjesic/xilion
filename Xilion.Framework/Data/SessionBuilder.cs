using System.Reflection;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Bytecode;
using NHibernate.Cfg;
using NHibernate.Envers.Configuration.Attributes;
using NHibernate.Envers.Event;
using NHibernate.Event;
using Xilion.Framework.Configuration;
using Xilion.Framework.Logging;
using NHibernate.Caches.SysCache;
using Microsoft.AspNetCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Xilion.Framework.Web;
using ISession = NHibernate.ISession;
using HttpContext = Xilion.Framework.Web.HttpContext;
using Xilon.Framework.Data.Search;
using System.Xml.Schema;
using System.Xml;
using NHibernate.Dialect;

namespace Xilion.Framework.Data
{
    public class SessionBuilder : ISessionBuilder
    {
        private const string SessionKey = "CurrentNHibernateSession";

        private static readonly ILogger _logger = LogManager.GetLogger<SessionBuilder>();

        private static NHibernate.Cfg.Configuration _configuration;
        private static ISessionFactory _sessionFactory;
        private static ISession _currentSession;
 

        #region ISessionBuilder Members

        /// <summary>
        /// Closes the current session object from http context. This should be called on end request.
        /// </summary>
        public void CloseSession()
        {

            if (HttpContext.Current != null)
            {
                _logger.Debug("Closing NHibernate session from the HttpContext.");

                var session = HttpContext.Current.Items[SessionKey] as ISession;

                if (session != null)
                {
                    if (session.IsOpen)
                    {
                        session.Close();
                        session.Dispose();
                    }
                    HttpContext.Current.Items.Remove(SessionKey);
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
        [System.Obsolete]
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
        [System.Obsolete]
        public ISession GetSession()
        {
            ISessionFactory factory =  GetSessionFactory();
            if (HttpContext.Current != null)
            {
                _logger.Debug("Getting session from HttpContext.");

                var session = HttpContext.Current.Items[SessionKey] as ISession;
                if (session == null || !session.IsOpen)
                {
                    session =  factory.OpenSession();
                    HttpContext.Current.Items.Remove(SessionKey);
                    HttpContext.Current.Items.Add(SessionKey, session);
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
        [System.Obsolete]
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
                         .Database(MsSqlConfiguration.MsSql2012
                                       .ConnectionString(ConnectionStringProvider.GetConnectionString())
                                       .Dialect<MsSql2012Dialect>()
                                       .FormatSql().ShowSql()
                                       .AdoNetBatchSize(100))
                        .Cache(c => c.UseQueryCache().ProviderClass(typeof(NHibernate.Caches.RtMemoryCache.RtMemoryCacheProvider).AssemblyQualifiedName))
                         .Mappings(m => AddAssemblies(m.FluentMappings))
                         .ExposeConfiguration(x => x.SetProperty("hbm2ddl.auto", "update"))
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
            configuration.SetProperty("nhibernate.envers.store_data_at_delete", "true");
            configuration.IntegrateWithEnvers(new AttributeConfiguration());
        }

        private static void InitializeSearch(NHibernate.Cfg.Configuration configuration)
        {
            configuration.SetListener(ListenerType.PostUpdate, new AuditEventListener());
            configuration.SetListener(ListenerType.PostInsert, new AuditEventListener());
            configuration.SetListener(ListenerType.PostDelete, new AuditEventListener());

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