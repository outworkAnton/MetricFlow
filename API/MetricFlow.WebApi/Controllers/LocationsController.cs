using Microsoft.AspNetCore.Mvc;
using BL = BusinessLogic.Contract.Models;
using BLI = BusinessLogic.Contract.Interfaces;
using DA = DataAccess.Contract.Models;

namespace MetricFlow.WebApi.Controllers
{
    [ApiController]
    public class LocationsController : GenericController<BL.Location, DA.Location>
    {
        private BLI.ILocationService _locationService;

        public LocationsController(BLI.ILocationService locationService) : base(locationService)
        {
            _locationService = locationService;
        }
    }
}