using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Contract
{
    public interface IDataAccessRepository<T> where T: class
    {
        Task<IReadOnlyCollection<T>>Get();
        Task Update(T item);
        Task Delete(T item);
        Task<T> Create(T item);
    }
}