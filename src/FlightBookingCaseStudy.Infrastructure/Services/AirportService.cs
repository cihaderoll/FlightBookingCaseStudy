using FlightBookingCaseStudy.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBookingCaseStudy.Infrastructure.Services
{
    public class AirportService : IAirportService
    {
        private readonly IApplicationDbContext _ctx;

        public AirportService(IApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<bool> ValidateAirport(string origin, string destination, CancellationToken cancellationToken)
        {
            var airports = await _ctx.Airports.ToListAsync(cancellationToken);

            var originExists = airports.Any(a => a.Code.Equals(origin, StringComparison.OrdinalIgnoreCase));
            if (!originExists) return false;
            var destinationExists = airports.Any(a => a.Code.Equals(destination, StringComparison.OrdinalIgnoreCase));
            if (!destinationExists) return false;

            return true;
        }
    }
}
