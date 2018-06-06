using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Contract
{
    public interface IDataAccessRepository<T> where T: class
    {
        Task<IEnumerable<T>> Get();
        Task Update(T item);
        Task Delete(T item);
        Task<T> Create(T item);
    }
}