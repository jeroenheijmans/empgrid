using EmpGrid.Domain;
using EmpGrid.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmpGrid.DataAccess.Core
{
    public class EmpRepository : IBulkEntityRepository<Emp>
    {
        // For now...
        private static readonly List<Emp> FakeDatabase = new List<Emp>
        {
            new Emp
            {
                Id = Guid.Parse("aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeee1"),
                Name = "John Doe",
                EmailAddress = "johndoe@example.org",
                TagLine = "The dev no one knew but everyone needed",
                Presences = new[]
                {
                    new Presence { Id = Guid.Parse("00000000-bbbb-cccc-dddd-eeeeeeeeeee1"), MediumId = "twitter", Url = "fake-url", Visibility = Visibility.Public, },
                    new Presence { Id = Guid.Parse("00000000-bbbb-cccc-dddd-eeeeeeeeeee2"), MediumId = "twitter", Url = "fake-url", Visibility = Visibility.Public, },
                }
            },
            new Emp
            {
                Id = Guid.Parse("aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeee2"),
                Name = "Richard Roe",
                EmailAddress = "richard@example.org",
                TagLine = "Hero of the day",
            },
        };

        public void Delete(IEntityIdentity<Guid> identity)
        {
            Delete(identity.Id);
        }

        public void Delete(Guid id)
        {
            var item = GetById(id);
            FakeDatabase.Remove(item);
        }

        public Emp FindById(IEntityIdentity<Guid> identity)
        {
            return FindById(identity.Id);
        }

        public Emp FindById(Guid id)
        {
            return FakeDatabase.SingleOrDefault(e => e.Id == id);
        }

        public Emp GetById(IEntityIdentity<Guid> identity)
        {
            return FindById(identity.Id) ?? throw new EntityNotFoundException(identity);
        }

        public Emp GetById(Guid id)
        {
            return FindById(id) ?? throw new EntityNotFoundException((GuidEntityIdentity)id);
        }

        public void Put(Emp entity)
        {
            var existingEntity = FindById(entity.Id);

            if (existingEntity != null)
            {
                FakeDatabase.Remove(existingEntity);
            }

            FakeDatabase.Add(entity);
        }

        public void Put(params Emp[] entities)
        {
            // TODO: Bulk insert. For now, let's stick with the KISS way.
            foreach (var emp in entities)
            {
                Put(emp);
            }
        }

        public IQueryable<Emp> Query()
        {
            return FakeDatabase.AsQueryable();
        }
    }
}
