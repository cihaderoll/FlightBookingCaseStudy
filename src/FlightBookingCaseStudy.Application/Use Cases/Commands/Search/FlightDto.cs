namespace FlightBookingCaseStudy.Application.Use_Cases.Commands.Search
{
    public class FlightDto
    {
        public string FlightNumber { get; set; } = string.Empty;
        public DateTime DepartureDateTime { get; set; }
        public DateTime ArrivalDateTime { get; set; }
        public decimal Price { get; set; }
    }
}
