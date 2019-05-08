using System.Collections.Generic;
using System.Linq;
using Xilion.Models.Classifications.Data;
using Xilion.Models.Core.Services;

namespace Xilion.Models.Classifications.Core
{
    public class LabelService : CmsService<Label>
    {
        private readonly ILabelRepository _labelRepository;

        public LabelService(ILabelRepository labelRepository) : base(labelRepository)
        {
            _labelRepository = labelRepository;
        }

        public IEnumerable<Label> GetTopLabels(string alias)
        {
            return
                _labelRepository.Query().Where(x => x.Parent == null && x.Classification.Alias == alias).OrderBy(
                    x => x.Ordinal);
        }

        public IEnumerable<Label> SortByOrdinal(IEnumerable<Label> tree)
        {
            foreach (Label label in tree)
            {
                if (label.HasChildren)
                {
                    label.Children = label.Children.OrderBy(x => x.Ordinal).ToList();
                    SortByOrdinal(label.Children);
                }
            }
            return tree;
        }

        public override void Save(Label entity)
        {
            if (string.IsNullOrWhiteSpace(entity.Alias))
                entity.Alias = GenerateAlias(entity.Name);
            if (entity.Children != null)
                foreach (Label child in entity.Children)
                {
                    child.Classification = entity.Classification;
                    child.Parent = entity;
                    if (string.IsNullOrWhiteSpace(child.Alias))
                        child.Alias = GenerateAlias(child.Name);
                    base.Save(child);
                }
            base.Save(entity);
        }
    }
}