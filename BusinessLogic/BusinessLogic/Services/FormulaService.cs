using AutoMapper;
using DataAccess.Contract.Repositories;
using BL = BusinessLogic.Contract.Models;
using BLI = BusinessLogic.Contract.Interfaces;
using DA = DataAccess.Contract.Models;

namespace BusinessLogic.Services
{
    public class FormulaService : BusinessLogicService<BL.Formula, DA.Formula>, BLI.IFormulaService
    {
        private IFormulaRepository _repository;
        private IMapper _mapper;

        public FormulaService(IFormulaRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
    }
}