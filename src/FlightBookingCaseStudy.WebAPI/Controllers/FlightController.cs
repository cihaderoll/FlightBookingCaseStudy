using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightBookingCaseStudy.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {

        public FlightController() 
        {
            
        }

        public IActionResult Search()
        {
            return Ok(new { Message = "GetFlights endpoint is working!" });
        }
    }
}
