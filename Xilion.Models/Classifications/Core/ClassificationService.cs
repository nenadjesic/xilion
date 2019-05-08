using System.Collections.Generic;
using System.Linq;
using Xilion.Models.Classifications.Data;
using Xilion.Models.Core;
using Xilion.Models.Core.Services;

namespace Xilion.Models.Classifications.Core
{
    public class ClassificationService : AliasedEntityService<Classification>
    {
        private static bool _initialized;
        private readonly IClassificationRepository _repository;

        public ClassificationService(IClassificationRepository repository)
            : base(repository)
        {
            _repository = repository;

            Initialize();
        }

        public ClassificationSettings Settings
        {
            get
            {
                return
                    (ClassificationSettings)
                    CmsContext.Current.GetApplication<ClassificationApplication>().GetSettings();
            }
        }

        public IList<Classification> Hierarchies()
        {
            return _repository.Query().Where(x => x.ClassificationType == ClassificationType.Hierarchy).ToList();
        }

        public IList<Classification> Flat()
        {
            return _repository.Query().Where(x => x.ClassificationType == ClassificationType.Flat).ToList();
        }

        protected void Initialize()
        {
            if (!_initialized)
            {
                foreach (var flat in Settings.FlatSystemClassifications)
                {
                    var classification = _repository.GetByAlias(flat.Alias);
                    if (classification == null)
                        _repository.Save(flat);
                }

                foreach (var hierarchy in Settings.HierarchySystemClassifications)
                {
                    var classification = _repository.GetByAlias(hierarchy.Alias);
                    if (classification == null)
                        _repository.Save(hierarchy);
                }

                _initialized = true;
            }
        }


        public override void Delete(Classification entity)
        {
            if (entity.IsSystem)
                throw new CmsException("Cannot delete system classification");

            base.Delete(entity);
        }

        public void DeleteSystem(Classification entity)
        {
            base.Delete(entity);
        }

        public override void Save(Classification entity)
        {
            if (string.IsNullOrWhiteSpace(entity.Alias))
                entity.Alias = GenerateAlias(entity.Name);
            base.Save(entity);
        }
    }
}