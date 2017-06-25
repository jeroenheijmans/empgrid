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
            new Medium("twitter", "Twitter", "twitter-square"),
            new Medium("facebook", "Facebook", "facebook-square"),
            new Medium("linkedin", "LinkedIn", "linkedin-square"),
            new Medium("google-plus", "Google+", "google-plus-square"),
            new Medium("meetup", "MeetUp", "meetup"),
            new Medium("goodreads", "GoodReads", "book"), // https://github.com/FortAwesome/Font-Awesome/issues/1715
            new Medium("github", "GitHub", "github-square"),
            new Medium("gitlab", "GitLab", "gitlab"),
            new Medium("bitbucket", "BitBucket", "bitbucket-square"),            
            new Medium("personal-website", "Personal Website", "home"),
            new Medium("blog", "Personal Blog", "rss-square"),
            new Medium("stackoverflow", "Stack Overflow", "stack-overflow"),
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
