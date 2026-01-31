using MediatR;

namespace FlightBookingCaseStudy.Application.Use_Cases.Commands
{
    public class SearchCommand : IRequest<int>
    {
        public int Departure { get; set; }
        public int Arrival { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ArrivalDate { get; set; }
    }
}
