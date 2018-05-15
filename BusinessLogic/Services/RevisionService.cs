using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Interfaces;
using DataAccess;
using Entities.Interfaces;
using Entities.Models;
using Utilities;

namespace BusinessLogic.Services
{
    public class RevisionService : IRevisionService
    {
        private readonly IDataAccessRepository _repository = new DataAccessRepository();

        public async Task<IDatabaseRevision> GetRevisionById(string id)
        {
            var revisions = await _repository.Get<IDatabaseRevision>().ConfigureAwait(false);
            return revisions.FirstOrDefault(revision => revision.Id == id);
        }

        public async Task DownloadLatestDatabaseRevision()
        {
            var revisions = await _repository.Get<IDatabaseRevision>().ConfigureAwait(false);
            var latestLocalRevision = revisions.OrderByDescending(revision => revision.Modified).FirstOrDefault();
            var updatedRevision = await GoogleDriveHelper.GetLatestRevision(latestLocalRevision).ConfigureAwait(false);
            if (updatedRevision != null)
            {
                await _repository.Create(updatedRevision).ConfigureAwait(false);
            }
        }
    }
}