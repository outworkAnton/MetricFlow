using AutoMapper;
using DataAccess.Contract.Models;
using DataAccess.Contract.Repositories;

namespace DataAccess.Repositories
{
    public class FormulaRepository : DataAccessRepository<Formula>, IFormulaRepository
    {
        private DataAccessContext _context;
        private IMapper _mapper;

        public FormulaRepository(DataAccessContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}