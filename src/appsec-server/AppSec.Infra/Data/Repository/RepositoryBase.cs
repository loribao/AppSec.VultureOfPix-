using AppSec.Domain.Entities;
using AppSec.Domain.Interfaces.IRepository;
using MongoDB.Driver;

namespace AppSec.Infra.Data.Repository
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : EntityBase
    {
        private readonly IMongoCollection<T> _entities;
        public List<T> Get() =>
       _entities.Find(entity => true).ToList();

        public T Get(string id) =>
            _entities.Find<T>(entity => entity.Id == id).FirstOrDefault();

        public T Create(T entity)
        {
            _entities.InsertOne(entity);
            return entity;
        }

        public void Update(string id, T entityIn) =>
            _entities.ReplaceOne(entity => entity.Id == id, entityIn);

        public void Remove(T entityIn) =>
            _entities.DeleteOne(entity => entity.Id == entityIn.Id);

        public void Remove(string id) =>
            _entities.DeleteOne(entity => entity.Id == id);
    }
}
