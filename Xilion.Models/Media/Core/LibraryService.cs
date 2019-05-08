using System;
using System.IO;
using System.Linq;
using System.Web;
using Xilion.Models.Core.Services;
using Xilion.Models.Media.Data;
using Xilion.Models.Media.Extensions;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Http;

namespace Xilion.Models.Media.Core
{
    public class LibraryService : CmsService<Library>
    {
        private readonly ILibraryRepository _libraryRepository;
        private static IHttpContextAccessor _httpContextAccessor;
        public LibraryService(ILibraryRepository libraryRepository, IHttpContextAccessor httpContextAccessor) : base(libraryRepository)
        {
            _libraryRepository = libraryRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        
        public IQueryable<Library> GetAllLibraryItems()
        {
            return _libraryRepository.GetAll().OrderBy(x => x.CreatedOn).Distinct(); 
        }

        public Library GetByTitleForUsers(string title)
        {
            return
                _libraryRepository.Query().SingleOrDefault(
                    x => x.Title == title && x.CreatedBy == _httpContextAccessor.HttpContext.User.Identity.Name);
        }

        /*Pregled po tipu dokumenata*/
        public List<Library> GetByLibraryType(long librarytype)
        {
            return _libraryRepository.Query().Where(x => x.LibraryType.Id == librarytype).ToList(); 
        }

        /* Pregled po vrsti dokumenta */

        public IQueryable<Library> GetByType(MediaType type)
        {
            return _libraryRepository.Query().Where(x => x.Type == type); 
        }
        /* Pregled po vrsti dokumenta PAGING */

        public IList<Library> GetByTypePaging(MediaType type)
        {
            return _libraryRepository.GetAll().Where(x => x.Type == type).ToList();
        }

        /// <summary>
        /// Deletes document from file system and clears cache.
        /// </summary>
        public void DeleteDirectory(Library item)
        {
            string path = String.Format("{0}", item.Directory());
            Directory.Delete(path);
        }

    }
}