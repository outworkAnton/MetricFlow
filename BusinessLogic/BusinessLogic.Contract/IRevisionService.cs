using System.Threading.Tasks;
using BusinessLogic.Contract.Interfaces;

namespace BusinessLogic.Contract
{
    public interface IRevisionService
    {
        Task<IDatabaseRevision> GetRevisionById(string id);
        Task DownloadLatestDatabaseRevision();
    }
}