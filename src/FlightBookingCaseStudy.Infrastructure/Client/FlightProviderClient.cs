using AutoMapper;
using FlightBookingCaseStudy.Application.Common.Settings;
using FlightBookingCaseStudy.Application.Interfaces;
using FlightBookingCaseStudy.Application.Use_Cases.Commands.Search;
using FlightBookingCaseStudy.Domain.Models;
using FlightBookingCaseStudy.Infrastructure.Extensions;
using Microsoft.Extensions.Options;
using System.Text;
using System.Threading;

namespace FlightBookingCaseStudy.Infrastructure.Client
{
    public class FlightProviderClient(HttpClient client, IMapper _mapper, ICachingService _cachingService, IOptions<CacheSettings> _cacheSettings) : IFlightProviderClient
    {
        private CacheSettings cacheSettings => _cacheSettings.Value;


        public async Task<List<FlightDto>> SearchFlight(string origin, string destination, DateOnly departDate, DateOnly? returnDate)
        {
            var cachedResponse = await _cachingService.GetAsync<List<FlightDto>>(cacheSettings.CacheKeyPrefix);
            if (cachedResponse != null)
            {
                return cachedResponse;
            }

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

            var flights = _mapper.Map<List<FlightDto>>(responseString.ParseSoapResponse());
            await _cachingService.SetAsync(cacheSettings.CacheKeyPrefix, flights, TimeSpan.FromMinutes(cacheSettings.ExpirationInMinutes));

            return flights;
        }
    }
}
