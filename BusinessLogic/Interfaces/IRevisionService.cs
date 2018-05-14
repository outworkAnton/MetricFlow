using System.Threading.Tasks;
using Entities.Models;

namespace BusinessLogic.Interfaces
{
    public interface IRevisionService
    {
        Task<DatabaseRevision> GetRevisionById(string Id);
        Task DownloadLatestDatabaseRevision();
    }
}