using FlightBookingCaseStudy.Application.Common.Settings;
using FlightBookingCaseStudy.Application.Interfaces;
using FlightBookingCaseStudy.Application.Use_Cases.Commands.Search;
using FlightBookingCaseStudy.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBookingCaseStudy.Application.Use_Cases.Commands.Book
{
    public class BookFlightCommandHandler : IRequestHandler<BookFlightCommand, Guid>
    {
        private readonly IApplicationDbContext _ctx;
        private readonly ICachingService<FlightDto> _cacheService;

        public BookFlightCommandHandler(
            IApplicationDbContext ctx,
            ICachingService<FlightDto> cacheService)
        {
            _ctx = ctx;
            _cacheService = cacheService;
        }

        public async Task<Guid> Handle(BookFlightCommand request, CancellationToken cancellationToken)
        {
            var targetFlight = await _cacheService.GetAsync(request.FlightId);
            if(targetFlight == null)
                throw new ValidationException("Flight data not found! Search again please");

            var order = new Order
            {
                Id = Guid.NewGuid(),
                FlightNumber = targetFlight.FlightNumber,
                Price = targetFlight.Price,
                CreatedAt = DateTime.UtcNow
            };

            await _ctx.Orders.AddAsync(order, cancellationToken);
            var result = await _ctx.SaveChangesAsync(cancellationToken);
            if (result > 0) return order.Id;

            // error log and return error
            return Guid.NewGuid();
        }
    }
}
