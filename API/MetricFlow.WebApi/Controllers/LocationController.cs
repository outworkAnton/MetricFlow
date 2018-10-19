using BL = BusinessLogic.Contract.Models;
using BLI = BusinessLogic.Contract.Interfaces;
using DA = DataAccess.Contract.Models;
using Microsoft.AspNetCore.Mvc;

namespace MetricFlow.WebApi.Controllers
{
    [Route("api/locations")]
    [ApiController]
    public class LocationController : GenericController<BL.Location,DA.Location>
    {
        private BLI.ILocationService _locationService;

        public LocationController(BLI.ILocationService locationLocationService) : base(locationLocationService)
        {
            _locationService = locationLocationService;
        }
    }
}