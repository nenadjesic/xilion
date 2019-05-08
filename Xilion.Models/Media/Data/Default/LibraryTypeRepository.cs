using System;
using System.Text.RegularExpressions;
using Xilion.Models.Core.Domain;
using Xilion.Framework.Data;
using Xilion.Framework.Data.Repositories;

namespace Xilion.Models.Media.Data.Default
{
    public class LibraryTypeRepository : Repository<LibraryType>, ILibraryTypeRepository
    {
        /// <summary>
        /// Creates a new instance of repository initialized with session builder object.
        /// </summary>
        /// <param name = "sessionBuilder"></param>
        public LibraryTypeRepository(ISessionBuilder sessionBuilder)
            : base(sessionBuilder)
        {
        }

        public string GenerateAlias(string input)
        {
            throw new NotImplementedException();
        }

        public LibraryType GetByAlias(string alias)
        {
            throw new NotImplementedException();
        }
    }
}