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
    private readonly Mock<ICachingService<FlightDto>> _mockCacheService;
    private readonly Mock<IApplicationDbContext> _mockCtx;

    private readonly BookFlightCommandHandler _handler;

    public BookFlightCommandHandlerTests()
    {
        _mockCacheService = new Mock<ICachingService<FlightDto>>();
        _mockCtx = new Mock<IApplicationDbContext>();

        _handler = new BookFlightCommandHandler(_mockCtx.Object, _mockCacheService.Object);
    }

    [Fact]
    public async Task Should_Create_Order()
    {
        //arrange
        var command = new BookFlightCommand
        {
            FlightId = "24550f7f-7b0a-4564-b430-4229043cf290"
        };

        _mockCacheService.Setup(c => c.GetListAsync("", It.IsAny<CancellationToken>())).ReturnsAsync(
            new List<FlightDto>
            {
                new()
                {
                    FlightId = "24550f7f-7b0a-4564-b430-4229043cf290",
                    Price = 150,
                    ArrivalDateTime = DateTime.Now.AddDays(2).AddHours(5),
                    DepartureDateTime = DateTime.Now.AddDays(2).AddHours(4),
                    FlightNumber = "TK1"
                },
                new()
                {
                    FlightId = "1e89cd31-40d5-43db-b8c1-c8fa76f3fffe",
                    Price = 300,
                    ArrivalDateTime = DateTime.Now.AddDays(2).AddHours(3),
                    DepartureDateTime = DateTime.Now.AddDays(2).AddHours(2),
                    FlightNumber = "TK3"
                },
                new()
                {
                    FlightId = "bfb16dac-acaf-404d-9666-243e0e216622",
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
            FlightId = "9c090d5d-3504-4c97-9c9d-37d680824e63"
        };

        _mockCacheService.Setup(c => c.GetAsync("", It.IsAny<CancellationToken>())).ReturnsAsync((FlightDto)null);

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
            FlightId = "3f97dc4f-7b48-4687-9bbc-0010c862a67f"
        };

        _mockCacheService.Setup(c => c.GetAsync("", It.IsAny<CancellationToken>())).ReturnsAsync(
            new FlightDto
            {
                FlightId = "3f97dc4f-7b48-4687-9bbc-0010c862a67f",
                Price = 150,
                ArrivalDateTime = DateTime.Now.AddDays(2).AddHours(5),
                DepartureDateTime = DateTime.Now.AddDays(2).AddHours(4),
                FlightNumber = "TK1"
            });

        var act = async () => await _handler.Handle(command, CancellationToken.None);

        //act
        await act.Should()
            .ThrowAsync<ValidationException>()
            .WithMessage("Please check your flight number.");
    }
}