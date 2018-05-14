using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IDataAccessRepository
    {
        Task<IEnumerable<T>> Get<T>();
        Task Update<T>(T item);
        Task Delete<T>(T item);
        Task<T> Create<T>(T item) where T : class;
    }
}