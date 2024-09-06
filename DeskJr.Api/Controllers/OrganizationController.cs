using DeskJr.Service.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace DeskJr.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationUnitService _organizationUnitService;

        public OrganizationController(IOrganizationUnitService organizationUnitService)
        {
            _organizationUnitService = organizationUnitService;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {
            var organizationUnits = await _organizationUnitService.GetOrganizationUnitsAsync();
            return Ok(organizationUnits);
        }
    }
}
