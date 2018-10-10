using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BusinessLogic.Contract;
using BusinessLogic.Contract.Interfaces;
using BusinessLogic.Contract.Models;
using DataAccess.Contract.Repositories;

namespace BusinessLogic.Services
{
    public class FormulaService : IFormulaService
    {
        private readonly IMapper _mapper;
        private readonly IFormulaRepository _repository;
        private IEnumerable<Formula> _formulas;
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
            .Select(formula => _mapper.Map<Formula>(formula))
            .ToList();

        public IEnumerable<Formula> GetAll()
        {
            return _formulas;
        }
    }
}