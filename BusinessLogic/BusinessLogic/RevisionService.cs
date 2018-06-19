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
        private readonly IEnumerable<DAContractInterfaces.IDatabaseRevision> _revisions;

        public RevisionService(IDatabaseRevisionRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _revisions = _repository.Get().GetAwaiter().GetResult().OfType<DAContractInterfaces.IDatabaseRevision>();
        }

        public BLContractInterfaces.IDatabaseRevision GetRevisionById(string id)
        {
            return _mapper.Map<BLContractInterfaces.IDatabaseRevision>(_revisions?
                .FirstOrDefault(revision => revision.Id == id));
        }

        public async Task DownloadLatestDatabaseRevision()
        {
            var latestLocalRevision = _revisions?
                                      .OrderByDescending(revision => revision.Modified)
                                      .FirstOrDefault();
            if (GoogleDriveHelper.NeedDownload(_mapper.Map<BLContractInterfaces.IDatabaseRevision>(latestLocalRevision)))
            {
                var latestRemoteRevision = await GoogleDriveHelper
                                                 .GetLatestRemoteRevision()
                                                 .ConfigureAwait(false);
                if (latestRemoteRevision != null)
                {
                    await _repository.Create(_mapper.Map<DAContractModels.DatabaseRevision>(latestRemoteRevision))
                                     .ConfigureAwait(false);
                }
            }
        }
    }
}