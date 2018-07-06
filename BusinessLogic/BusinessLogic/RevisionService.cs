using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogic.Contract;
using BLContractInterfaces = BusinessLogic.Contract.Interfaces;
using DataAccess.Contract;
using DAContractInterfaces = DataAccess.Contract.Interfaces;
using DAContractModels = DataAccess.Contract.Models;

namespace BusinessLogic
{
    public class RevisionService : IRevisionService
    {
        private readonly IMapper _mapper;
        private readonly IDatabaseRevisionRepository _repository;
        private readonly IEnumerable<BLContractInterfaces.IDatabaseRevision> _revisions;

        public RevisionService(IDatabaseRevisionRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _revisions = _repository
                         .Get()
                         .GetAwaiter()
                         .GetResult()
                         .Select(revision => _mapper.Map<BLContractInterfaces.IDatabaseRevision>(revision))
                         .ToList();
        }

        public IEnumerable<BLContractInterfaces.IDatabaseRevision> GetAll()
        {
            return _revisions;
        }

        public BLContractInterfaces.IDatabaseRevision GetRevisionById(string id)
        {
            return _revisions?.FirstOrDefault(revision => revision.Id == id);
        }

        public async Task DownloadLatestDatabaseRevision()
        {
            var latestLocalRevision = _revisions?
                                      .OrderByDescending(revision => revision.Modified)
                                      .FirstOrDefault();
            if (GoogleDriveHelper.NeedDownload(latestLocalRevision)
            )
            {
                var latestRemoteRevision = await GoogleDriveHelper
                                                 .DownloadRevision()
                                                 .ConfigureAwait(false);
                if (latestRemoteRevision != null)
                {
                    await _repository.Create(_mapper.Map<DAContractModels.DatabaseRevision>(latestRemoteRevision))
                                     .ConfigureAwait(false);
                }
            }
        }

        public bool Changed()
        {
            return _revisions?
                   .OrderByDescending(revision => revision.Modified)
                   .FirstOrDefault()
                   ?.Changed == 1;
        }
    }
}