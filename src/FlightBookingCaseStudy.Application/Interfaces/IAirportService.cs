using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBookingCaseStudy.Application.Interfaces
{
    public interface IAirportService
    {
        Task<bool> ValidateAirport(string origin, string destination, CancellationToken cancellationToken);
    }
}
