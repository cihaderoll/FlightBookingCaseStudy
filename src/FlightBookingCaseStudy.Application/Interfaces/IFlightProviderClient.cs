using FlightBookingCaseStudy.Domain.Models;

namespace FlightBookingCaseStudy.Application.Interfaces
{
    public interface IFlightProviderClient
    {
        public Task<List<FlightModel>> SearchFlight(string origin, string destination, DateOnly departdate);
    }
}
