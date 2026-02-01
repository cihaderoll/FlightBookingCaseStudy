using MediatR;

namespace FlightBookingCaseStudy.Application.Use_Cases.Commands.Search
{
    public class GetFlightsCommand : IRequest<List<FlightDto>>
    {
        public string? Origin { get; set; }
        public string? Destination { get; set; }
        public DateOnly DepartDate { get; set; }
        public DateOnly? ReturnDate { get; set; }
    }
}
