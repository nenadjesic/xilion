using System;
using System.Linq;
using NHibernate;
using NHibernate.Linq;
using Xilion.Models.Core.Domain;
using Xilion.Models.Karte;
using Xilion.Framework.Data;
using Xilion.Framework.Data.Repositories;
using ISessionBuilder = Xilion.Framework.Data.ISessionBuilder;

namespace Xilion.Models.Karte.Data.Default
{
    public class KartaRepository : Repository<Karta>, IKartaRepository
    {
        /// <summary>
        /// Creates a new instance of repository initialized with session builder object.
        /// </summary>
        /// <param name = "sessionBuilder"></param>
        public KartaRepository(ISessionBuilder sessionBuilder)
            : base(sessionBuilder)
        {
        }
    }
}