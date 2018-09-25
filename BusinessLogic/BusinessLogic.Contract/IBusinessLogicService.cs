using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Contract
{
    public interface IBusinessLogicService<T> where T: class
    {
        IReadOnlyCollection<T> GetAllItems();
        Task<T> GetItemById(string id);
        Task<T> Update(T item);
        Task Delete(T item);
        Task<T> Create(T item);
    }
}