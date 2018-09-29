using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Contract
{
    public interface IBusinessLogicService<TBL, TDA> where TBL: class where TDA: class
    {
        IReadOnlyCollection<TBL> GetAllItems();
        Task<TBL> GetItemById(string id);
        Task UpdateAsync(TBL item);
        Task DeleteAsync(TBL item);
        Task Create(TBL item);
    }
}