using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using CityInfoTracker.Application;
using CityInfoTracker.Application.DTOs;
using CityInfoTracker.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CityInfoTracker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CityInfoController : ControllerBase
    {
        private readonly ICityInfoService _cityInfoService;

        public CityInfoController(ICityInfoService cityInfoService)
        {
            _cityInfoService = cityInfoService;
        }
        
        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var cityInfo = await _cityInfoService.Get(id);
            if (cityInfo != null)
                return Ok(cityInfo);
            else
                return BadRequest("Cannot get info");
        }
    }
}