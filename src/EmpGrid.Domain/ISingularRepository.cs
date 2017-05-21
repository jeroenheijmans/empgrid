using System.Collections.Generic;

namespace EmpGrid.Domain
{
    public interface ISingularRepository<T> 
        : IEntityRepository<T, string, IEntityIdentity<string>>
        where T : SingularEntity, IAggregate
    {
        IEnumerable<T> List();
    }
}
