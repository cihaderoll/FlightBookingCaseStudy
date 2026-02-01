using FlightBookingCaseStudy.Application.Interfaces;
using FlightBookingCaseStudy.Domain.Models;
using System.Text;

namespace FlightBookingCaseStudy.Infrastructure.Client
{
    public class FlightProviderClient(HttpClient client) : IFlightProviderClient
    {
        public async Task<List<FlightModel>> SearchFlight(string origin, string destination, DateOnly departdate)
        {
            var soapEnvelope = $@"<?xml version=""1.0"" encoding=""utf-8""?>
                <s:Envelope xmlns:s=""http://schemas.xmlsoap.org/soap/envelope/"">
                  <s:Body>
                    <AvailabilitySearch xmlns=""http://tempuri.org/"">
                      <request>
                        <Origin>{origin}</Origin>
                        <Destination>{destination}</Destination>
                        <DepartureDate>{departdate}</DepartureDate>
                      </request>
                    </AvailabilitySearch>
                  </s:Body>
                </s:Envelope>";

            var content = new StringContent(soapEnvelope, Encoding.UTF8, "text/xml");

            content.Headers.Add("SOAPAction", "http://tempuri.org/IAirSearch/AvailabilitySearch");

            
            var response = await client.PostAsync("https://localhost:5001/Service.svc", content);

            if (!response.IsSuccessStatusCode)
                return null;

            var responseString = await response.Content.ReadAsStringAsync();

            return null;
        }
    }
}
