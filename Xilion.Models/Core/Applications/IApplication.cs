using System.Collections.Generic;
using Xilion.Models.Core.Settings;

namespace Xilion.Models.Core.Applications
{
    /// <summary>
    /// Represents main application.
    /// </summary>
    public interface IApplication
    {
        /// <summary>
        /// Gets unique application name.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Gets or sets <see cref="ApplicationEntityType"/> for the application.
        /// </summary>
        IList<ApplicationEntityType> Types { get; }
        /// <summary>
        /// 
        /// </summary>
        void Initialize();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ApplicationSettings GetSettings();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ApplicationSettings GetUsersettings();
    }
}