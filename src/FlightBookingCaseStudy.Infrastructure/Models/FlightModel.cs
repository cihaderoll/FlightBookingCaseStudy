namespace FlightBookingCaseStudy.Domain.Models
{
    public class FlightModel
    {
        public string FlightNumber { get; set; } = string.Empty;
        public DateTime DepartureDateTime { get; set; }
        public DateTime ArrivalDateTime { get; set; }
        public decimal Price { get; set; }
    }
}
