using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogic.Contract;
using BL = BusinessLogic.Contract.Interfaces;
using DataAccess.Contract;
using DA = DataAccess.Contract.Interfaces;

namespace BusinessLogic
{
    public class RevisionService : IRevisionService
    {
        private readonly IMapper _mapper;
        private readonly IDataAccessRepository _repository;
        private readonly IEnumerable<DA.IDatabaseRevision> _revisions;

        public RevisionService(IDataAccessRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _revisions = _repository.Get<DA.IDatabaseRevision>().GetAwaiter().GetResult();
        }

        public BL.IDatabaseRevision GetRevisionById(string id)
        {
            return _mapper.Map<BL.IDatabaseRevision>(_revisions.FirstOrDefault(revision => revision.Id == id));
        }

        public async Task DownloadLatestDatabaseRevision()
        {
            var latestLocalRevision = _revisions?.OrderByDescending(revision => revision.Modified).FirstOrDefault();
            if (GoogleDriveHelper.NeedDownload(_mapper.Map<BL.IDatabaseRevision>(latestLocalRevision)))
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