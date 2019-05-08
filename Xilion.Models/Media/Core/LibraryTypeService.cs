using System;
using System.IO;
using System.Linq;
using System.Web;
using Xilion.Models.Core.Services;
using Xilion.Models.Media.Data;
using Xilion.Models.Media.Extensions;


namespace Xilion.Models.Media.Core
{
    public class LibraryTypeService : CmsService<LibraryType>
    {
        private readonly ILibraryTypeRepository _librarytypeRepository;
        private readonly ILibraryRepository _libraryRepository;

        public LibraryTypeService(ILibraryTypeRepository librarytypeRepository,ILibraryRepository libraryRepository) : base(librarytypeRepository)
        {
            _librarytypeRepository = librarytypeRepository;
             _libraryRepository = libraryRepository;
        }
        
        public IQueryable<LibraryType> GetAllLibraryTypeItems()
        {
            return _librarytypeRepository.GetAll();
        }
       
        public LibraryType GetLibraryTypeByID(long id)
        {
            return _librarytypeRepository.GetById(id);
        }

        public IQueryable<Library> GetLibrary(long libtypeId)
        {
            return _libraryRepository.Query().Where(x => x.LibraryType.Id == libtypeId);
        }

        public override void Save(LibraryType entity)
        {
            if (string.IsNullOrWhiteSpace(entity.Alias))
                entity.Alias = GenerateAlias(entity.Title);
            base.Save(entity);
        }
    }
}