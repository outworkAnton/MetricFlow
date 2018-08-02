using System.Collections.Generic;
using System.Threading.Tasks;

using BusinessLogic.Contract.Interfaces;

namespace BusinessLogic.Contract
{
    public interface IRevisionService
    {
        IEnumerable<IDatabaseRevision> GetAll();
        IDatabaseRevision GetRevisionById(string id);
        Task DownloadLatestDatabaseRevision();
        Task<bool> Changed();
        Task<bool> UploadRevision();
        void CleanRevisions();
    }
}