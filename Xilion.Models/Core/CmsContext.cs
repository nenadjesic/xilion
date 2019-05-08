using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web.Mvc;
using Xilion.Framework.Logging;
using Xilion.Models.Core.Applications;
using Xilion.Models.Core.Domain;

namespace Xilion.Models.Core
{
    /// <summary>
    ///   Basic entry point for all cms applications.
    /// </summary>
    public class CmsContext : ICmsContext
    {
        public const string DefaultLabelGroup = "Xilion/*";
        private static readonly ILogger _logger = LogManager.GetLogger<CmsContext>();
        private static string[] _unrestrictedRoles = new[] { UsersRoles.Administrator };
        private readonly object _syncLock = new Object();
        private bool _initialized;

        public static ICmsContext Current
        {
            get { return DependencyResolver.Current.GetService<ICmsContext>(); }
        }

        public static string[] UnrestrictedRoles
        {
            get { return _unrestrictedRoles; }
            set { _unrestrictedRoles = value; }
        }

        #region ICmsContext Members

        public IEnumerable<IApplication> Applications { get; private set; }

        public void Initialize(IApplication[] applications)
        {
            _logger.DebugFormat("Initializing default CmsContext with {0} applications...", applications.Length);

            if (_initialized) return;

            lock (_syncLock)
            {
                if (_initialized) return;

                Applications = applications;

                foreach (IApplication application in Applications)
                    application.Initialize();

                _initialized = true;
            }

            _logger.DebugFormat("CmsContext initialization completed");
        }

        #endregion
    }
}