using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogic.Contract;
using DataAccess.Contract.Interfaces;
using DataAccess.Contract.Repositories;
using BLContractInterfaces = BusinessLogic.Contract.Interfaces;
using DAContractModels = DataAccess.Contract.Models;

namespace BusinessLogic
{
    public abstract class BusinessLogicService<T> : IBusinessLogicService<T> where T: class
    {
        private readonly IMapper _mapper;
        private readonly IDataAccessRepository<T> _repository;
        private IEnumerable<T> _items;

        protected BusinessLogicService(IDataAccessRepository<T> repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            LoadItems();
        }
        
        private void LoadItems() => _items = _repository
            .Get()
            .GetAwaiter()
            .GetResult()
            .Select(item => _mapper.Map<T>(item))
            .ToList();
        
        public virtual IReadOnlyCollection<T> GetAllItems()
        {
            return _items.ToArray();
        }

        public virtual async Task<T> GetItemById(string id)
        {
            return await _repository.Find(id).ConfigureAwait(false);
        }

        public virtual Task<T> Update(T item)
        {
            throw new System.NotImplementedException();
        }

        public virtual Task Delete(T item)
        {
            throw new System.NotImplementedException();
        }

        public virtual Task<T> Create(T item)
        {
            throw new System.NotImplementedException();
        }
    }
}