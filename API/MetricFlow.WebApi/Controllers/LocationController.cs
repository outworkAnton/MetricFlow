using Microsoft.AspNetCore.Mvc;
using BL = BusinessLogic.Contract.Models;
using BLI = BusinessLogic.Contract.Interfaces;
using DA = DataAccess.Contract.Models;

namespace MetricFlow.WebApi.Controllers
{
    [Route("api/locations")]
    [ApiController]
    public class LocationController : GenericController<BL.Location, DA.Location>
    {
        private BLI.ILocationService _locationService;

        public LocationController(BLI.ILocationService locationService) : base(locationService)
        {
            _locationService = locationService;
        }
    }
}