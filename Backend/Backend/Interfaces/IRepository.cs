using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Interfaces
{
    public interface IRepository<T> where T: class, IEntity
    {
        Task<List<T>> GetAll();
        Task<T> Get(string id);
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task<T> Delete(string id);
    }
}