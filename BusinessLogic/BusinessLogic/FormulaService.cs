using System.Collections.Generic;

using AutoMapper;

using BusinessLogic.Contract;
using BLContractInterfaces = BusinessLogic.Contract.Interfaces;
using BLContractModels = BusinessLogic.Contract.Models;
using DataAccess.Contract.Repositories;
using DAContractInterfaces = DataAccess.Contract.Interfaces;
using DAContractModels = DataAccess.Contract.Models;
using System;
using System.Linq;

namespace BusinessLogic
{
    public class FormulaService : IFormulaService
    {
        private readonly IMapper _mapper;
        private readonly IFormulaRepository _repository;
        private IEnumerable<BLContractInterfaces.IFormula> _formulas;
        public FormulaService(IFormulaRepository repository, IMapper mapper)
        {
            _repository = repository
                ??
                throw new ArgumentNullException(nameof(repository));
            _mapper = mapper
                ??
                throw new ArgumentNullException(nameof(mapper));
            LoadFormulas();
        }

        private void LoadFormulas() => _formulas = _repository
            .Get()
            .GetAwaiter()
            .GetResult()
            .Select(formula => _mapper.Map<BLContractInterfaces.IFormula>(formula))
            .ToList();

        public IEnumerable<BLContractInterfaces.IFormula> GetAll()
        {
            return _formulas;
        }
    }
}