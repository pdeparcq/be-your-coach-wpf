using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Deparcq.Common.Application;
using Deparcq.Common.Domain;

namespace Deparcq.Common.Json
{
    public abstract class JsonRepository<T> : IEntityRepository where T : IEntity
    {
        protected string FileName { get; }
        protected List<T> Entities { get; private set; }
        protected bool HasChanges { get; private set; }

        protected JsonRepository(IUnitOfWork unitOfWork, string fileName)
        {
            unitOfWork?.Register(this);
            FileName = fileName;
            Entities = new List<T>();
            ReadFromFile();
            HasChanges = false;
        }

        public IQueryable<T> Query => Entities.AsQueryable();

        private void ReadFromFile()
        {
            if (File.Exists(FileName))
            {
                using (var sr = File.OpenText(FileName))
                {
                    Entities = sr.Deserialize<List<T>>();
                }
            }
        }

        private void WriteToFile()
        {
            using (var sw = File.CreateText(FileName))
            {
                Entities.Serialize(sw);
            }
        }

        public T Add(T entity)
        {
            Update(entity);
            return entity;
        }

        public void Update(T entity)
        {
            Entities.RemoveAll(s => s.Id == entity.Id);
            Entities.Add(entity);
            HasChanges = true;
        }

        public T Get(Guid id)
        {
            return Query.Single(s => s.Id == id);
        }

        public void SaveChanges()
        {
            if (!HasChanges) return;
            WriteToFile();
            HasChanges = false;
        }

        public void UndoChanges()
        {
            ReadFromFile();
            HasChanges = false;
        }

        public IEnumerable<IEntity> EntitiesWithEvents => Entities.Where(e => e.Events.Any()).Select(e => e as IEntity).AsEnumerable();
    }
}