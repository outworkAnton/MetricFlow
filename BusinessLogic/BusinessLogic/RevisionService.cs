using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using BusinessLogic.Contract;
using BLContractInterfaces = BusinessLogic.Contract.Interfaces;
using BLContractModels = BusinessLogic.Contract.Models;
using DataAccess.Contract;
using DAContractInterfaces = DataAccess.Contract.Interfaces;
using DAContractModels = DataAccess.Contract.Models;

namespace BusinessLogic
{
    public class RevisionService : IRevisionService
    {
        private readonly IMapper _mapper;
        private readonly IDatabaseRevisionRepository _repository;
        private IEnumerable<BLContractInterfaces.IDatabaseRevision> _revisions;

        public RevisionService(IDatabaseRevisionRepository repository, IMapper mapper)
        {
            _repository = repository
                ??
                throw new ArgumentNullException(nameof(repository));
            _mapper = mapper
                ??
                throw new ArgumentNullException(nameof(mapper));
            LoadRevisions();
        }

        private void LoadRevisions() => _revisions = _repository
            .Get()
            .GetAwaiter()
            .GetResult()
            .Select(revision => _mapper.Map<BLContractInterfaces.IDatabaseRevision>(revision))
            .ToList();

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
            var latestLocalRevision = await GetLatestLocalRevisionInfo().ConfigureAwait(false);
            if (await GoogleDriveHelper.NeedDownload(latestLocalRevision).ConfigureAwait(false))
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
            else
            {
                throw new InvalidOperationException();
            }
        }

        public async Task<bool> Changed()
        {
            return await _repository.Changed().ConfigureAwait(false);
        }

        public async Task<bool> UploadRevision()
        {
            return await GoogleDriveHelper.UploadRevision(_repository).ConfigureAwait(false);
        }

        public async Task CleanRevisions()
        {
            if (_revisions.Count() == 1)
            {
                return;
            };
            var lastRevision = await GetLatestLocalRevisionInfo().ConfigureAwait(false);
            foreach (var revision in _revisions)
            {
                await _repository.Delete(_mapper.Map<DAContractModels.DatabaseRevision>(revision)).ConfigureAwait(false);
            }
            lastRevision.Changed = 1;
            await _repository.Create(_mapper.Map<DAContractModels.DatabaseRevision>(lastRevision)).ConfigureAwait(false);
            LoadRevisions();
        }

        public async Task<BLContractInterfaces.IDatabaseRevision> GetLatestLocalRevisionInfo()
        {
            var daRevision = await _repository.GetLatestLocalRevision().ConfigureAwait(false)
                ??
                throw new NullReferenceException();
            return _mapper.Map<BLContractInterfaces.IDatabaseRevision>(daRevision);
        }

        public async Task<BLContractInterfaces.IDatabaseRevision> GetLatestRemoteRevisionInfo()
        {
            var gdhRevision = await GoogleDriveHelper.GetLatestRemoteRevisionInfo().ConfigureAwait(false)
                ??
                throw new NullReferenceException();
            BLContractInterfaces.IDatabaseRevision lastRevision = new BLContractModels.DatabaseRevision(gdhRevision.Id, gdhRevision.ModifiedTime.Value, gdhRevision.Size.Value, 0);
            return lastRevision;
        }
    }
}