namespace AppSec.Domain.Interfaces.IRepository
{
    public interface IRepositoryBase<T>
    {
        List<T> Get();
       
        T Get(string id);
        T Create(T entity);
        void Update(string id, T entityIn);
        void Remove(T entityIn);
        void Remove(string id);
    }
}
