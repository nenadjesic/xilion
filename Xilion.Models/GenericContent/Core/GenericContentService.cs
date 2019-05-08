using Xilion.Models.Core.Services;
using Xilion.Models.GenericContent.Data;
using System.Linq;
using System;

namespace Xilion.Models.GenericContent.Core
{
    public class GenericContentService : CmsService<GenericContent>
    {
        private readonly IGenericContentRepository _genericContentRepository;

        public GenericContentService(IGenericContentRepository genericContentRepository): base(genericContentRepository)
        {
             _genericContentRepository = genericContentRepository;
        }


        
       

        public GenericContent GetByPageID(long pageid)
        {
             return
                _genericContentRepository.Query().SingleOrDefault(x => x.Page.Id == pageid );
        }

  
    
    }
}