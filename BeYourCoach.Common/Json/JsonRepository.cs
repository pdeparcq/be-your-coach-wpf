using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BeYourCoach.Common.Json
{
    public abstract class JsonRepository<T> where T : IEntity
    {
        protected string FileName { get; }
        protected List<T> Entities { get; private set; }

        protected JsonRepository(string fileName)
        {
            FileName = fileName;
            Entities = new List<T>();
            ReadFromFile();
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

        public void Add(T entity)
        {
            Update(entity);
        }

        public void Update(T entity)
        {
            Entities.RemoveAll(s => s.Id == entity.Id);
            Entities.Add(entity);
            WriteToFile();
        }

        public T Get(Guid id)
        {
            return Query.Single(s => s.Id == id);
        }
    }
}