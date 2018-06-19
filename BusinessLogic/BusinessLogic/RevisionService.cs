using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogic.Contract;
using BL = BusinessLogic.Contract.Interfaces;
using DataAccess.Contract;
using DataAccess.Contract.Models;
using DA = DataAccess.Contract.Interfaces;

namespace BusinessLogic
{
    public class RevisionService : IRevisionService
    {
        private readonly IMapper _mapper;
        private readonly IDatabaseRevisionRepository _repository;
        private readonly IEnumerable<DA.IDatabaseRevision> _revisions;

        public RevisionService(IDatabaseRevisionRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _revisions = _repository.Get().GetAwaiter().GetResult().OfType<DA.IDatabaseRevision>();
        }

        public BL.IDatabaseRevision GetRevisionById(string id)
        {
            return _mapper.Map<BL.IDatabaseRevision>(_revisions?.FirstOrDefault(revision => revision.Id == id));
        }

        public async Task DownloadLatestDatabaseRevision()
        {
            var latestLocalRevision = _revisions?.OrderByDescending(revision => revision.Modified).FirstOrDefault();
            if (GoogleDriveHelper.NeedDownload(_mapper.Map<BL.IDatabaseRevision>(latestLocalRevision)))
            {
                var latestRemoteRevision = await GoogleDriveHelper.GetLatestRemoteRevision().ConfigureAwait(false);
                if (latestRemoteRevision != null)
                {
                    var convertedModel = _mapper.Map<DatabaseRevision>(latestRemoteRevision);
                    await _repository.Create(convertedModel).ConfigureAwait(false);
                }
            }
        }
    }
}