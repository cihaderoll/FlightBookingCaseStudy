using FlightBookingCaseStudy.Application.Use_Cases.Commands.Book;
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
        public async Task<IActionResult> Search([FromQuery] GetFlightsCommand command)
        {
            var flights = await mediator.Send(command);

            return Ok(flights);
        }

        [HttpPost]
        public async Task<IActionResult> Book([FromBody] BookFlightCommand command)
        {
            var orderId = await mediator.Send(command);
            return Ok(orderId);
        }
    }
}
