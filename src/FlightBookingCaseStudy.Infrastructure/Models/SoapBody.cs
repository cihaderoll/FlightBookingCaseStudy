using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FlightBookingCaseStudy.Infrastructure.Models
{
    public class SoapBody
    {
        [XmlElement(ElementName = "AvailabilitySearchResponse", Namespace = "http://tempuri.org/")]
        public AvailabilitySearchResponse AvailabilitySearchResponse { get; set; }
    }
}
