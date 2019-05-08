using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Xilion.Models.Core;
using Xilion.Models.Media.Data;
using Xilion.Models.Media.Documents;
using Xilion.Models.Media.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace Xilion.Models.Media.Core
{
    public class DocumentService : MediaService<DocumentItem>
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly ILibraryRepository _libraryRepository;
        private static IHttpContextAccessor _httpContextAccessor;
        private readonly IHostingEnvironment _hosting;
        public DocumentService(IDocumentRepository documentRepository, ILibraryRepository libraryRepository ,
            IHttpContextAccessor httpContextAccessor, IHostingEnvironment hosting)
            : base(documentRepository)
        {
            _documentRepository = documentRepository;
            _libraryRepository = libraryRepository;
            _httpContextAccessor = httpContextAccessor;
            _hosting = hosting;
        }

        public ILibraryRepository CollectionRepository
        {
            get { return _libraryRepository; }
        }

        public DocumentsSettings Settings
        {
            get { return (DocumentsSettings) CmsContext.Current.GetApplication<DocumentsApplication>().GetSettings(); }
        }

        /// <summary>
        /// Gets all documents by their library.
        /// </summary>
        public IList<DocumentItem> GetDocumentsByLibrary(long id)
        {
            return _documentRepository.Query().Where(x => x.Library.Id == id).ToList();
        }

        /// <summary>
        ///PREGLED DOKUMENATA U TRENUTKU IZBORA LIBTIPA
        /// </summary>
        public IList<DocumentItem> GetDocumentsByLibraryType(string type)
        {
            return _documentRepository.GetAll().Where(x => x.Library.LibraryType.Alias == type).OrderByDescending(x => x.CreatedOn).Distinct().ToList();
        }

        /// <summary>
        /// Deletes document file by file path
        /// </summary>
        /// <param name="documentItem"></param>
        public void DeleteDocument(DocumentItem documentItem)
        {
            string path = String.Format("{0}/{1}", MediaService.GetCollectionPath(documentItem.Library.Id),
                                        documentItem.FilePath);
            File.Delete(path);
            Delete(documentItem);
        }

        /// <summary>
        /// Deletes document from file system and clears cache.
        /// </summary>
        public void DeleteFile(DocumentItem item)
        {
            string path = String.Format("{0}/{1}", item.Library.Directory(),
                                        item.FilePath);
            File.Delete(path);
            ClearCache(item);
        }

        public void ClearCache(DocumentItem item)
        {
            string directory = item.Library.Directory();
            string cfgCachePath = Settings.CachePath;
            string[] cachedFiles = Directory.GetFiles(_hosting.WebRootPath + item.Id + "*");

            foreach (string cached in cachedFiles)
            {
                File.Delete(Path.Combine(directory, cached));
            }
        }

        /// <summary>
        /// Organizes documents by their ids
        /// </summary>
        /// <param name="memberIds"></param>
        public void Organize(long[] memberIds)
        {
            int index = 0;
            foreach (long memberId in memberIds)
            {
                DocumentItem document = GetById(memberId);
                if (document != null)
                {
                    document.Ordinal = index;
                    index++;
                    Save(document);
                }
            }
        }
    }
}