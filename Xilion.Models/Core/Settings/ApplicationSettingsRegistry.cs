using Xilion.Models.Media.Documents;
using Xilion.Models.Media.Images;
using Xilion.Models.Media.Video;
using StructureMap;
using StructureMap.Configuration.DSL.Expressions;
using StructureMap.Pipeline;

namespace Xilion.Models.Core.Settings
{
    public class ApplicationSettingsRegistry : Registry
    {
        public ApplicationSettingsRegistry()
        {
            // TODO: See how to make this auto-register all types inherited from ApplicationSettings

            For<ImageSettings>()
               .LifecycleIs(Lifecycles.Container)
               .Use<ImageSettings>()
               .Named(SettingsScope.CurrentUsers.ToString());
            For<ImageSettings>()
                .Use<ImageSettings>()
                .Named(SettingsScope.AllUsers.ToString());

            For<VideoSettings>()
               .LifecycleIs(Lifecycles.Container)
               .Use<VideoSettings>()
               .Named(SettingsScope.CurrentUsers.ToString());
            For<VideoSettings>()
                .Use<VideoSettings>()
                .Named(SettingsScope.AllUsers.ToString());

            For<DocumentsSettings>()
               .LifecycleIs(Lifecycles.Container)
               .Use<DocumentsSettings>()
               .Named(SettingsScope.CurrentUsers.ToString());
            For<DocumentsSettings>()
                .Use<DocumentsSettings>()
                .Named(SettingsScope.AllUsers.ToString());         
        }
    }
}