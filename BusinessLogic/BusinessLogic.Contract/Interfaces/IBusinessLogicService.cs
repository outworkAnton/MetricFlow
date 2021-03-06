using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Contract.Interfaces
{
    public interface IBusinessLogicService<TBL, TDA> where TBL: class where TDA: class
    {
        IReadOnlyCollection<TBL> GetAllItems();
        Task<TBL> GetItemById(string id);
        Task Update(TBL item);
        Task Delete(TBL item);
        Task Create(TBL item);
    }
}