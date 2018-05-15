using System.Threading.Tasks;
using Entities.Interfaces;
using Entities.Models;

namespace BusinessLogic.Interfaces
{
    public interface IRevisionService
    {
        Task<IDatabaseRevision> GetRevisionById(string Id);
        Task DownloadLatestDatabaseRevision();
    }
}