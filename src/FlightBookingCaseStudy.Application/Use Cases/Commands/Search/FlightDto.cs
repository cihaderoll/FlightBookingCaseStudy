using FlightBookingCaseStudy.Application.Interfaces;

namespace FlightBookingCaseStudy.Application.Use_Cases.Commands.Search
{
    public class FlightDto : ICacheable
    {
        public string FlightNumber { get; set; }
        public DateTime DepartureDateTime { get; set; }
        public DateTime ArrivalDateTime { get; set; }
        public decimal Price { get; set; }
        public string FlightId { get; set; }
    }
}
