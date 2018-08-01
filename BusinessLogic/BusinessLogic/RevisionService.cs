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
            var latestLocalRevision = _revisions?
                .OrderByDescending(revision => revision.Modified)
                .FirstOrDefault();
            if (GoogleDriveHelper.NeedDownload(latestLocalRevision))
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

        public bool Changed()
        {
            return _repository.Changed();
        }

        public async Task<bool> UploadRevision()
        {
            return await GoogleDriveHelper.UploadRevision().ConfigureAwait(false);
        }

        public void CleanRevisions()
        {
            if (_revisions.Count() == 1)
            {
                return;
            };
            var lastRevision = _revisions?
                .OrderByDescending(revision => revision.Modified)
                .FirstOrDefault()
                ??
                throw new NullReferenceException();
            foreach (var revision in _revisions)
            {
                _repository.Delete(_mapper.Map<DAContractModels.DatabaseRevision>(revision));
            }
            lastRevision.Changed = 1;
            _repository.Create(_mapper.Map<DAContractModels.DatabaseRevision>(lastRevision));
            LoadRevisions();
        }
    }
}