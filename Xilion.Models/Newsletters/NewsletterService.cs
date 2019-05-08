using System;
using Xilion.Models.Core.Domain;
using Xilion.Models.Core.Services;
using Xilion.Models.Newsletters;
using System.Linq;

namespace Xilion.Models.Newsletters.Core
{
    public class NewsletterService : CmsService<Newsletter>
    {
        private readonly INewsletterRepository _newletterRepository;

        public NewsletterService(INewsletterRepository newletterRepository): base(newletterRepository)
        {
            _newletterRepository = newletterRepository;
        }

        /// <summary>
        ///   Gets Odjel po id.
        /// </summary>
        public IQueryable<Newsletter> GetNewsletterByEmail(string email)
        {
            return _newletterRepository.Query().Where(x => x.Email == email);
        }

        public override void Save(Newsletter entity)
        { 
            base.Save(entity);
        }
    }
}