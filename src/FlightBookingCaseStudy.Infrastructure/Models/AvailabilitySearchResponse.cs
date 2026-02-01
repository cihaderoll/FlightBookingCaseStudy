using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FlightBookingCaseStudy.Infrastructure.Models
{
    public class AvailabilitySearchResponse
    {
        [XmlElement(ElementName = "AvailabilitySearchResult", Namespace = "http://tempuri.org/")]
        public AvailabilitySearchResult AvailabilitySearchResult { get; set; }
    }
}
