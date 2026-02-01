using System.Xml.Serialization;

namespace FlightBookingCaseStudy.Domain.Models
{
    [XmlType(Namespace = "http://schemas.datacontract.org/2004/07/FlightProvider")]
    public class FlightOption
    {
        public string FlightNumber { get; set; }
        public DateTime DepartureDateTime { get; set; }
        public DateTime ArrivalDateTime { get; set; }
        public decimal Price { get; set; }
    }
}
