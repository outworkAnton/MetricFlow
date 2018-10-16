using AutoMapper;
using DataAccess.Contract.Models;
using DataAccess.Contract.Repositories;

namespace DataAccess.Repositories
{
    public class FormulaRepository : DataAccessRepository<Formula>, IFormulaRepository
    {
        public FormulaRepository(DataAccessContext context, IMapper mapper) : base(context, mapper) { }
    }
}