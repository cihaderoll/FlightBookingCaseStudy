using FlightBookingCaseStudy.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlightBookingCaseStudy.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Order> Orders { get; }
        DbSet<Airport> Airports { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
