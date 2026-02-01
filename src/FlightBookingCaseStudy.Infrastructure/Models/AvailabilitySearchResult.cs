using FlightBookingCaseStudy.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FlightBookingCaseStudy.Infrastructure.Models
{
    public class AvailabilitySearchResult
    {
        [XmlArray(ElementName = "FlightOptions", Namespace = "http://schemas.datacontract.org/2004/07/FlightProvider")]
        [XmlArrayItem(ElementName = "FlightOption", Namespace = "http://schemas.datacontract.org/2004/07/FlightProvider")]
        public List<FlightOption> FlightOptions { get; set; }

        [XmlElement(Namespace = "http://schemas.datacontract.org/2004/07/FlightProvider")]
        public bool HasError { get; set; }
    }
}
