using System;
using System.Collections.Generic;
using System.Linq;
using EmpGrid.Domain;
using EmpGrid.Domain.Core;
using Newtonsoft.Json;

namespace EmpGrid.DataAccess.Core
{
    public class EmpRepository : IBulkEntityRepository<Emp>
    {
        private static readonly List<Emp> s_fakeDatabase = new List<Emp>();

        public static void SeedFakeDatabase(string path)
        {
            var contents = System.IO.File.ReadAllText(path);
            s_fakeDatabase.Clear();
            s_fakeDatabase.AddRange(JsonConvert.DeserializeObject<List<Emp>>(contents));
        }

        public void Delete(IEntityIdentity<Guid> identity)
        {
            Delete(identity.Id);
        }

        public void Delete(Guid id)
        {
            var item = GetById(id);
            s_fakeDatabase.Remove(item);
        }

        public Emp FindById(IEntityIdentity<Guid> identity)
        {
            return FindById(identity.Id);
        }

        public Emp FindById(Guid id)
        {
            return s_fakeDatabase.SingleOrDefault(e => e.Id == id);
        }

        public Emp GetById(IEntityIdentity<Guid> identity)
        {
            return FindById(identity.Id) ?? throw new EntityNotFoundException(identity);
        }

        public Emp GetById(Guid id)
        {
            return FindById(id) ?? throw new EntityNotFoundException((GuidEntityIdentity)id);
        }

        public PutResult Put(Emp entity)
        {
            var result = PutResult.Created;
            var existingEntity = FindById(entity.Id);

            if (existingEntity != null)
            {
                result = PutResult.Updated;
                s_fakeDatabase.Remove(existingEntity);
            }

            s_fakeDatabase.Add(entity);

            return result;
        }

        public IQueryable<Emp> Query()
        {
            return s_fakeDatabase.AsQueryable();
        }
    }
}
