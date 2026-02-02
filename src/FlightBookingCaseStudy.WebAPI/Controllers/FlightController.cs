using FlightBookingCaseStudy.Application.Use_Cases.Commands.Search;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlightBookingCaseStudy.WebAPI.Controllers
{
    [Route("api/flights")]
    [ApiController]
    public class FlightController(IMediator mediator) : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> Search([FromQuery]GetFlightsCommand command)
        {
            var flights = await mediator.Send(command);

            ///TODO: Add proper response codes and error handling
            ///TODO: Add pagination support
            ///TODO: LOGGING(DB)
            ///TODO: AIRPORT VALIDATION
            ///TODO: BOOK RECORDS
            ///TODO: RETURN COMMON SERVICE RESPONSE
            return Ok(flights);
        }
    }
}
