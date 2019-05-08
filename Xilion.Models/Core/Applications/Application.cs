using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Xilion.Models.Classifications;
using Xilion.Models.Core.Configuration;
using Xilion.Models.Core.Settings;

namespace Xilion.Models.Core.Applications
{
    /// <summary>
    /// Represents a basic part of CMS.
    /// </summary>
    public abstract class Application : IApplication
    {
        private IList<ApplicationEntityType> _types = new List<ApplicationEntityType>();

        /// <summary>
        /// Gets the name of application configuration section (a section in config file used to define additional
        /// application options).
        /// </summary>
        protected virtual string ConfigurationSectionName
        {
            get { return Name.ToLower(); }
        }

        /// <summary>
        /// Gets or sets the value indicating whether the application is already initialized.
        /// </summary>
        protected bool Initialized { get; set; }

        #region IApplication Members

        /// <summary>
        ///   Gets the application name.
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        ///   Gets the list of application entity types.
        /// </summary>
        public virtual IList<ApplicationEntityType> Types
        {
            get
            {
                if (!Initialized)
                    throw new CmsException(String.Format(
                        "You need to call Initialize method on '{0}' application before accessing its properties.",
                        Name));

                return _types;
            }
        }

        /// <summary>
        /// Initializes the application. Creates all types and their respective metadata properties and label groups, overrides them
        /// from config settings, etc. Needs to be called only once from the <see cref = "ICmsContext" />.
        /// </summary>
        public virtual void Initialize()
        {
            if (Initialized)
                throw new CmsException("You only need to call Initialize() method once.");

            BeforeInitialize();
            // types
            _types = GetTypes();
            OverridePropertiesWithConfigSectionDefinitions();
            // set flag indicates application is initialized
            Initialized = true;

            AfterInitialize();
        }

        /// <summary>
        /// Gets the application settings object.
        /// </summary>
        /// <returns>An instance of application settings object.</returns>
        public virtual ApplicationSettings GetSettings()
        {
            return GetSettings(SettingsScope.AllUsers);
        }

        /// <summary>
        /// Gets the application settings object for the given owner / Users name.
        /// </summary>
        /// <returns>An instance of application settings object.</returns>
        public virtual ApplicationSettings GetUsersettings()
        {
            return GetSettings(SettingsScope.CurrentUsers);
        }

        #endregion

        #region Implementation of IClassificated

        public virtual IList<Classification> Classifications { get; set; }

        #endregion

        protected virtual void BeforeInitialize()
        {
        }

        protected virtual void AfterInitialize()
        {
        }

        /// <summary>
        /// Gets the application settings object for the given scope.
        /// </summary>
        /// <param name="scope">Scope to get the settings for.</param>
        /// <returns>An instance of application settings object.</returns>
        protected virtual ApplicationSettings GetSettings(SettingsScope scope)
        {
            return null;
        }

        /// <summary>
        /// Get a list of all application entity types.
        /// </summary>
        /// <returns>A list of all application entity types.</returns>
        protected abstract IList<ApplicationEntityType> GetTypes();

        /// <summary>
        /// Gets the application configuration section with the name defined in <see cref = "ConfigurationSectionName" />
        /// property. If you use this method from inherited class, cast the result to section type inherited from
        /// <see cref = "ApplicationConfigurationSection" />.
        /// </summary>
        /// <returns>An <see cref = "ApplicationConfigurationSection" /> instance.</returns>
        protected ApplicationConfigurationSection GetConfigurationSection()
        {
            object section = ConfigurationManager.GetSection(ConfigurationSectionName);
            return section != null
                       ? ConfigurationManager.GetSection(ConfigurationSectionName) as ApplicationConfigurationSection
                       : null;
        }

        private IEnumerable<MetaDataConfigurationElement> GetEntityConfigurationMeta(
            Type type, ApplicationConfigurationSection section)
        {
            if (section != null)
            {
                var meta = (IEnumerable<MetaDataConfigurationElement>) section.MetaDefinition;
                var entities = (IEnumerable<EntityConfigurationElement>) section.Entities;
                EntityConfigurationElement definition = entities.SingleOrDefault(x => x.Type == type);
                if (definition != null)
                    meta = meta.Union(definition.MetaDefinition, new MetaDataConfigurationElementComparer());
                return meta;
            }
            return null;
        }

        private void MergeProperties(
            ICollection<MetaDataPropertyDefinition> definitions,
            IEnumerable<MetaDataConfigurationElement> configuration)
        {
            if (configuration != null)
            {
                foreach (MetaDataConfigurationElement element in configuration)
                {
                    MetaDataPropertyDefinition definition = definitions.SingleOrDefault(x => x.Name == element.Name);
                    if (definition == null)
                    {
                        definition = new MetaDataPropertyDefinition(element.Name);
                        definitions.Add(definition);
                    }

                    definition.IsAnalyzed = element.Analyzed;
                    definition.DefaultValue = element.DefaultValue;
                    definition.Indexed = element.Indexed;
                    definition.IsLocalized = element.Localized;
                    definition.IsStored = element.Stored;
                    definition.Type = element.Type;
                }
            }
        }

        private void OverridePropertiesWithConfigSectionDefinitions()
        {
            ApplicationConfigurationSection configurationSection = GetConfigurationSection();
            if (configurationSection == null) return;

            foreach (ApplicationEntityType entityType in _types)
            {
                IEnumerable<MetaDataConfigurationElement> configDefinitions = GetEntityConfigurationMeta(
                    entityType.Type, configurationSection);
                MergeProperties(entityType.Properties, configDefinitions);
            }
        }
    }
}