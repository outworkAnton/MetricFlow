using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Contract;
using BusinessLogic.Contract.Interfaces;
using DataAccess.Contract;
using DataAccess.Contract.Interfaces;

namespace BusinessLogic
{
    public class RevisionService : IRevisionService
    {
        private readonly IDataAccessRepository _repository;

        public RevisionService(IDataAccessRepository repository)
        {
            _repository = repository;
        }

        public async Task<IDatabaseRevision> GetRevisionById(string id)
        {
            var revisions = await _repository.Get<IDatabaseRevision>().ConfigureAwait(false);
            return revisions?.FirstOrDefault(revision => revision.Id == id);
        }

        public async Task DownloadLatestDatabaseRevision()
        {
            var revisions = await _repository.Get<IDatabaseRevision>().ConfigureAwait(false);
            var latestLocalRevision = revisions?.OrderByDescending(revision => revision.Modified).FirstOrDefault();
            if (GoogleDriveHelper.NeedDownload(latestLocalRevision))
            {
                var latestRemoteRevision =
                    await GoogleDriveHelper.GetLatestRemoteRevision().ConfigureAwait(false);
                if (latestRemoteRevision != null)
                {
                    await _repository.Create(latestRemoteRevision).ConfigureAwait(false);
                }
            }
        }
    }
}