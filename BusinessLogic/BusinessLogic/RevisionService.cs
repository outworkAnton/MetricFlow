using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogic.Contract;
using BL = BusinessLogic.Contract.Interfaces;
using DataAccess.Contract;
using NLog;
using DA = DataAccess.Contract.Interfaces;

namespace BusinessLogic
{
    public class RevisionService : IRevisionService
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IMapper _mapper;
        private readonly IDataAccessRepository _repository;
        private readonly IEnumerable<DA.IDatabaseRevision> _revisions;

        public RevisionService(IDataAccessRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            Logger.Info("Revision service dependencies is loaded");
            _revisions = _repository.Get<DA.IDatabaseRevision>().GetAwaiter().GetResult();
            Logger.Info("Revisions is loaded");
        }

        public BL.IDatabaseRevision GetRevisionById(string id)
        {
            Logger.Info("Get revision with Id: " + id);
            return _mapper.Map<BL.IDatabaseRevision>(_revisions.FirstOrDefault(revision => revision.Id == id));
        }

        public async Task DownloadLatestDatabaseRevision()
        {
            var latestLocalRevision = _revisions?.OrderByDescending(revision => revision.Modified).FirstOrDefault();
            Logger.Debug(
                $"Latest local revision: Id - {latestLocalRevision?.Id}, Modified - {latestLocalRevision?.Modified}, Size - {latestLocalRevision?.Size}");
            if (GoogleDriveHelper.NeedDownload(_mapper.Map<BL.IDatabaseRevision>(latestLocalRevision)))
            {
                Logger.Info("Needs to download new revision from remote cloud");
                var latestRemoteRevision =
                    await GoogleDriveHelper.GetLatestRemoteRevision().ConfigureAwait(false);
                Logger.Debug(
                $"Latest remote revision: Id - {latestRemoteRevision?.Id}, Modified - {latestRemoteRevision?.Modified}, Size - {latestRemoteRevision?.Size}");
                if (latestRemoteRevision != null)
                {
                    await _repository.Create(latestRemoteRevision).ConfigureAwait(false);
                    Logger.Info("Local revision updated");
                }
            }
        }
    }
}