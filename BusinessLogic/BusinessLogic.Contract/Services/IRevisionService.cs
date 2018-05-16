using System.Threading.Tasks;
using BusinessLogic.BusinessLogic.Contract.Models;

namespace BusinessLogic.BusinessLogic.Contract.Services
{
    public interface IRevisionService
    {
        Task<IDatabaseRevision> GetRevisionById(string Id);
        Task DownloadLatestDatabaseRevision();
    }
}