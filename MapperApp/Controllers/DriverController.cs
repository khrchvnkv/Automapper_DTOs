using AutoMapper;
using MapperApp.Models;
using MapperApp.Models.DTOs.Incoming;
using MapperApp.Models.DTOs.Outgoing;
using Microsoft.AspNetCore.Mvc;

namespace MapperApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DriverController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILogger<DriverController> _logger;

        private static List<Driver> _drivers = new();
        
        public DriverController(IMapper mapper, ILogger<DriverController> logger)
        {
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetDrivers()
        {
            var allDrivers = _drivers.Where(d => d.Status == 1).ToList();
            var allDriversDtos = _mapper.Map<IEnumerable<DriverDto>>(allDrivers);
            return Ok(allDriversDtos ); 
        }
        
        [HttpGet("{id}")]
        public IActionResult GetDriver(Guid id)
        {
            var driver = GetDriverData(id);
            if (driver is null) return NotFound();

            var driverDto = _mapper.Map<DriverDto>(driver);
            return Ok(driverDto);
        }

        [HttpPost]
        public IActionResult CreateDriver(DriverForCreationDto data)
        {
            if (ModelState.IsValid)
            {
                var driver = _mapper.Map<Driver>(data);
                _drivers.Add(driver);

                var driverDto = _mapper.Map<DriverDto>(driver);
                return CreatedAtAction(nameof(GetDriver), new { driverDto.Id }, driverDto);
            }

            return new JsonResult("Something went wrong")
            {
                StatusCode = 500
            };
        }

        [HttpPut("{id}")]
        public IActionResult UpdateDriver(Guid id, Driver data)
        {
            if (id == data.Id) return BadRequest();

            var driver = GetDriverData(data.Id);
            if (driver is null) return BadRequest();

            driver.FirstName = data.FirstName;
            driver.LastName = data.LastName;
            driver.DriverNumber = data.DriverNumber;
            driver.DateAdded = data.DateAdded;
            driver.DateUpdated = data.DateUpdated;
            driver.Status = data.Status;
            driver.WorldChampionship = data.WorldChampionship;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDriver(Guid id)
        {
            var driver = GetDriverData(id);
            if (driver is null) return NotFound();

            driver.Status = 0; 
            return NoContent();
        }

        private Driver? GetDriverData(Guid id) => _drivers.FirstOrDefault(d => d.Id == id);
    }
}