using AutoMapper;
using FlightBookingCaseStudy.Application.Interfaces;
using FlightBookingCaseStudy.Application.Use_Cases.Commands.Search;
using FlightBookingCaseStudy.Domain.Models;
using FlightBookingCaseStudy.Infrastructure.Extensions;
using System.Text;

namespace FlightBookingCaseStudy.Infrastructure.Client
{
    public class FlightProviderClient(HttpClient client, IMapper _mapper) : IFlightProviderClient
    {
        public async Task<List<FlightDto>> SearchFlight(string origin, string destination, DateOnly departDate, DateOnly? returnDate)
        {
            var soapEnvelope = $@"<?xml version=""1.0"" encoding=""utf-8""?>
                <s:Envelope xmlns:s=""http://schemas.xmlsoap.org/soap/envelope/"">
                  <s:Body>
                    <AvailabilitySearch xmlns=""http://tempuri.org/"">
                      <request>
                        <Origin>{origin}</Origin>
                        <Destination>{destination}</Destination>
                        <DepartureDate>{departDate}</DepartureDate>
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

            return _mapper.Map<List<FlightDto>>(responseString.ParseSoapResponse());
        }
    }
}
