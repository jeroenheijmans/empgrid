using System;

namespace EmpGrid.Domain
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(IEntityIdentity identity)
            : base($"Entity with id '{identity.HumanReadableId}' could not be found.")
        {            
        }

        public EntityNotFoundException(IEntityIdentity identity, string message)
            : base($"Entity with id '{identity.HumanReadableId}' could not be found. {message}")
        { }

        public EntityNotFoundException(IEntityIdentity identity, string message, Exception inner) 
            : base($"Entity with id '{identity.HumanReadableId}' could not be found. {message}", inner)
        { }
    }
}
