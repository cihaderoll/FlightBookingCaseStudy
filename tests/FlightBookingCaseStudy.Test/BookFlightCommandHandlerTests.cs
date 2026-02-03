using FlightBookingCaseStudy.Application.Common.Settings;
using FlightBookingCaseStudy.Application.Interfaces;
using FlightBookingCaseStudy.Application.Use_Cases.Commands.Book;
using FlightBookingCaseStudy.Application.Use_Cases.Commands.Search;
using FlightBookingCaseStudy.Domain.Entities;
using FluentAssertions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;

namespace FlightBookingCaseStudy.Test;

public class BookFlightCommandHandlerTests
{
    private readonly Mock<ICachingService> _mockCacheService;
    private readonly Mock<IApplicationDbContext> _mockCtx;
    
    private readonly BookFlightCommandHandler _handler;
    
    public BookFlightCommandHandlerTests()
    {
        var cacheSettings = new CacheSettings
        {
            CacheKeyPrefix = "",
            ExpirationInMinutes = 15
        };
        var _mockCacheSettings = Options.Create(cacheSettings);
        
        _mockCacheService = new Mock<ICachingService>();
        _mockCtx = new Mock<IApplicationDbContext>();
        
        
        _handler = new BookFlightCommandHandler(_mockCtx.Object, _mockCacheService.Object, _mockCacheSettings);
    }

    [Fact]
    public async Task Should_Create_Order()
    {
        //arrange
        var command = new BookFlightCommand
        {
            FlightNumber = "TK5"
        };

        _mockCacheService.Setup(c => c.GetAsync<List<FlightDto>>("", It.IsAny<CancellationToken>())).ReturnsAsync(
            new List<FlightDto>
            {
                new()
                {
                    Price = 150,
                    ArrivalDateTime = DateTime.Now.AddDays(2).AddHours(5),
                    DepartureDateTime = DateTime.Now.AddDays(2).AddHours(4),
                    FlightNumber = "TK1"
                },
                new()
                {
                    Price = 300,
                    ArrivalDateTime = DateTime.Now.AddDays(2).AddHours(3),
                    DepartureDateTime = DateTime.Now.AddDays(2).AddHours(2),
                    FlightNumber = "TK3"
                },
                new()
                {
                    Price = 450,
                    ArrivalDateTime = DateTime.Now.AddDays(2).AddHours(7),
                    DepartureDateTime = DateTime.Now.AddDays(2).AddHours(6),
                    FlightNumber = "TK5"
                }
            });
        
        var mockDbSet = new Mock<DbSet<Order>>();

        _mockCtx.Setup(c => c.Orders).Returns(mockDbSet.Object);
        
        _mockCtx.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var result = await _handler.Handle(command, CancellationToken.None);

        //act
        result.Should().NotBeEmpty();
        
        _mockCtx.Verify(m => m.Orders.AddAsync(It.IsAny<Order>(), It.IsAny<CancellationToken>()), Times.Once);
        _mockCtx.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Should_Throw_Error_When_NoCache()
    {
        //arrange
        var command = new BookFlightCommand
        {
            FlightNumber = "TK5"
        };

        _mockCacheService.Setup(c => c.GetAsync<List<FlightDto>>("", It.IsAny<CancellationToken>())).ReturnsAsync(
            new List<FlightDto>());

        var act = async () => await _handler.Handle(command, CancellationToken.None);

        //act
        await act.Should()
            .ThrowAsync<ValidationException>()
            .WithMessage("Flight data not found! Search again please");
    }
    
    [Fact]
    public async Task Should_Throw_Error_When_Wrong_FlightNumber()
    {
        //arrange
        var command = new BookFlightCommand
        {
            FlightNumber = "TK7"
        };

        _mockCacheService.Setup(c => c.GetAsync<List<FlightDto>>("", It.IsAny<CancellationToken>())).ReturnsAsync(
            new List<FlightDto>{
                new()
                {
                    Price = 150,
                    ArrivalDateTime = DateTime.Now.AddDays(2).AddHours(5),
                    DepartureDateTime = DateTime.Now.AddDays(2).AddHours(4),
                    FlightNumber = "TK1"
                },
                new()
                {
                    Price = 300,
                    ArrivalDateTime = DateTime.Now.AddDays(2).AddHours(3),
                    DepartureDateTime = DateTime.Now.AddDays(2).AddHours(2),
                    FlightNumber = "TK3"
                },
                new()
                {
                    Price = 450,
                    ArrivalDateTime = DateTime.Now.AddDays(2).AddHours(7),
                    DepartureDateTime = DateTime.Now.AddDays(2).AddHours(6),
                    FlightNumber = "TK5"
                }
            });

        var act = async () => await _handler.Handle(command, CancellationToken.None);

        //act
        await act.Should()
            .ThrowAsync<ValidationException>()
            .WithMessage("Please check your flight number.");
    }
}