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
            new Medium("twitter", "Twitter"),
            new Medium("facebook", "Facebook"),
            new Medium("linkedin", "LinkedIn"),
            new Medium("google-plus", "Google+"),
            new Medium("meetup", "MeetUp"),
            new Medium("goodreads", "GoodReads"),
            new Medium("github", "GitHub"),
            new Medium("gitlab", "GitLab"),
            new Medium("bitbucket", "BitBucket"),            
            new Medium("personal-website", "Personal Website"),
            new Medium("blog", "Personal Blog"),
            new Medium("stackoverflow", "Stack Overflow"),
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
