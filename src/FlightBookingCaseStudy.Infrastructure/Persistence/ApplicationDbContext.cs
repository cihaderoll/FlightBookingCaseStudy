using FlightBookingCaseStudy.Application.Interfaces;
using FlightBookingCaseStudy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FlightBookingCaseStudy.Infrastructure.Persistence
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options), IApplicationDbContext
    {
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<Airport> Airports => Set<Airport>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
