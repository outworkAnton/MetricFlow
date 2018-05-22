using System.Threading.Tasks;
using BusinessLogic.Contract.Interfaces;

namespace BusinessLogic.Contract
{
    public interface IRevisionService
    {
        IDatabaseRevision GetRevisionById(string id);
        Task DownloadLatestDatabaseRevision();
    }
}