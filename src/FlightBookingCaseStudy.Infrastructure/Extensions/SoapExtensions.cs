using FlightBookingCaseStudy.Domain.Models;
using FlightBookingCaseStudy.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FlightBookingCaseStudy.Infrastructure.Extensions
{
    public static class SoapExtensions
    {
        public static List<FlightOption> ParseSoapResponse(this string xmlContent)
        {
            if (string.IsNullOrWhiteSpace(xmlContent))
            {
                ///TODO log an error to database
                return new List<FlightOption>();
            }

            var serializer = new XmlSerializer(typeof(SoapEnvelope));

            using var reader = new StringReader(xmlContent);
            try
            {
                var envelope = (SoapEnvelope)serializer.Deserialize(reader);

                var result = envelope?.Body?.AvailabilitySearchResponse?.AvailabilitySearchResult;

                if (result != null && result.FlightOptions != null)
                    return result.FlightOptions;

                return new List<FlightOption>();
            }
            catch (Exception ex)
            {
                // Loglama yapabilirsiniz
                throw new Exception("XML Parse edilirken hata oluştu: " + ex.Message);
            }
        }
    }
}
