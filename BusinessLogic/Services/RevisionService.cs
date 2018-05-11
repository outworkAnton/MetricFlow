using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Interfaces;
using DataAccess.Interfaces;
using DataAccess.Repositories;
using Entities.Models;

namespace BusinessLogic.Services
{
    public class RevisionService : IRevisionService
    {
        private readonly IDatabaseRevisionRepository _repository = new DatabaseRevisionRepository();

        public Task<DatabaseRevision> GetRevisionById(string id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<DatabaseRevision> DownloadLatestDatabaseRevision()
        {
            var revisions = await _repository.Get().ConfigureAwait(false);
            return revisions.OrderByDescending(revision => revision.Modified).FirstOrDefault();
        }
    }
}