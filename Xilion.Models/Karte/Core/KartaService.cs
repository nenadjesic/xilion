using System.Collections.Generic;
using System.Linq;
using Xilion.Models.Core;
using Xilion.Models.Core.Services;
using Xilion.Models.Karte.Data;
using Xilion.Models.Karte;

namespace Xilion.Models.Karte.Core
{
    public class KartaService : CmsService<Karta>
    {
        private readonly IKartaRepository _kartaRepository;

        public KartaService(IKartaRepository kartaRepository) : base(kartaRepository)
        {
            _kartaRepository = kartaRepository;
        }

        /// <summary>
        ///   Gets list of all articles.
        /// </summary>
        public IList<Karta> GetKarte()
        {
            return _kartaRepository.GetAll().ToList();
        }

        public override void Save(Karta entity)
        {
            base.Save(entity);
        }
    }
}