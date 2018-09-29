using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using BusinessLogic.Contract;

using DataAccess.Contract.Repositories;

namespace BusinessLogic
{
    public abstract class BusinessLogicService<TBL, TDA> : IBusinessLogicService<TBL, TDA> where TBL : class where TDA : class
    {
        private readonly IMapper _mapper;
        private readonly IDataAccessRepository<TDA> _repository;
        private IEnumerable<TBL> _items;

        protected BusinessLogicService(IDataAccessRepository<TDA> repository, IMapper mapper)
        {
            _repository = repository
                ??
                throw new ArgumentNullException(nameof(repository));
            _mapper = mapper
                ??
                throw new ArgumentNullException(nameof(mapper));
            LoadItems();
        }

        private void LoadItems() => _items = _repository
            .Get()
            .GetAwaiter()
            .GetResult()
            .Select(item => _mapper.Map<TBL>(item))
            .ToList();

        public virtual IReadOnlyCollection<TBL> GetAllItems() => _items.ToArray();

        public virtual async Task<TBL> GetItemById(string id)
        {
            var foundedItem = await _repository.Find(id).ConfigureAwait(false);
            return _mapper.Map<TBL>(foundedItem);
        }

        public virtual async Task UpdateAsync(TBL item) => await _repository.Update(_mapper.Map<TDA>(item)).ConfigureAwait(false);

        public virtual async Task DeleteAsync(TBL item) => await _repository.Delete(_mapper.Map<TDA>(item)).ConfigureAwait(false);

        public virtual async Task Create(TBL item)
        {
            await _repository.Create(_mapper.Map<TDA>(item)).ConfigureAwait(false);
        }
    }
}