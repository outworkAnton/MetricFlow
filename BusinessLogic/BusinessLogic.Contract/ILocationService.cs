using System.Collections.Generic;

using BusinessLogic.Contract.Interfaces;

namespace BusinessLogic.Contract
{
    public interface ILocationService
    {
        IEnumerable<ILocation> GetAll();
        ILocation GetLocationById(string id);
    }
}