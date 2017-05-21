using EmpGrid.Domain;
using EmpGrid.Domain.Core;
using System.Collections.Generic;
using System.Linq;

namespace EmpGrid.DataAccess.Core
{
    public class MediumRepository : ISingularRepository<Medium>
    {
        private static readonly IList<Medium> InMemoryEntities = new []
        {
            new Medium { Id = "twitter", Name = "Twitter" },
            new Medium { Id = "facebook", Name = "Facebook" },
            new Medium { Id = "linkedin", Name = "LinkedIn" },
            new Medium { Id = "google-plus", Name = "Google+" },
            new Medium { Id = "meetup", Name = "MeetUp" },
            new Medium { Id = "goodreads", Name = "GoodReads" },
            new Medium { Id = "github", Name = "GitHub" },
            new Medium { Id = "gitlab", Name = "GitLab" },
            new Medium { Id = "bitbucket", Name = "BitBucket" },            
            new Medium { Id = "personal-website", Name = "Personal Website" },
            new Medium { Id = "blog", Name = "Personal Blog" },
        };

        public Medium FindById(IEntityIdentity<string> identity)
        {
            return FindById(identity.Id);
        }

        public Medium FindById(string id)
        {
            return InMemoryEntities.SingleOrDefault(e => e.Id == id);
        }

        public Medium GetById(IEntityIdentity<string> identity)
        {
            return FindById(identity.Id) ?? throw new EntityNotFoundException(identity);
        }

        public Medium GetById(string id)
        {
            return GetById((StringEntityIdentity)id);
        }

        public IEnumerable<Medium> List()
        {
            return InMemoryEntities.ToList();
        }
    }
}
