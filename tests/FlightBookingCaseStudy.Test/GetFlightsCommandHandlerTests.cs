using AutoMapper;
using FlightBookingCaseStudy.Application.Common.Settings;
using FlightBookingCaseStudy.Application.Interfaces;
using FlightBookingCaseStudy.Application.Use_Cases.Commands.Book;
using FlightBookingCaseStudy.Application.Use_Cases.Commands.Search;
using FluentAssertions;
using FluentValidation;
using Moq;

namespace FlightBookingCaseStudy.Test;

public class GetFlightsCommandHandlerTests
{
    private readonly Mock<IFlightProviderClient> _mockProviderClient;
    private readonly Mock<IAirportService> _mockAirportService;
    private readonly GetFlightsCommandHandler _handler;

    public GetFlightsCommandHandlerTests()
    {
        _mockProviderClient = new Mock<IFlightProviderClient>();
        _mockAirportService = new Mock<IAirportService>();

        _handler = new GetFlightsCommandHandler(_mockProviderClient.Object, _mockAirportService.Object);
    }
    
    [Fact]
    public async Task Handle_Should_GetFlights_ReturnsFlights()
    {
        //arrange
        var command = new GetFlightsCommand()
        {
            Origin = "SAW",
            Destination = "AYT",
            DepartDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(2))
        };

        _mockAirportService.Setup(m => m.ValidateAirport(command.Origin, command.Destination, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);
        
        _mockProviderClient.Setup(m => m.SearchFlight(command.Origin, command.Destination, command.DepartDate, command.ReturnDate))
            .ReturnsAsync(new List<FlightDto>
            {
                new FlightDto{
                    Price = 150,
                    ArrivalDateTime = DateTime.Now.AddDays(2).AddHours(5),
                    DepartureDateTime = DateTime.Now.AddDays(2).AddHours(4),
                    FlightNumber = "TK1"
                },
                new FlightDto{
                    Price = 300,
                    ArrivalDateTime = DateTime.Now.AddDays(2).AddHours(3),
                    DepartureDateTime = DateTime.Now.AddDays(2).AddHours(2),
                    FlightNumber = "TK3"
                },
                new FlightDto{
                    Price = 450,
                    ArrivalDateTime = DateTime.Now.AddDays(2).AddHours(7),
                    DepartureDateTime = DateTime.Now.AddDays(2).AddHours(6),
                    FlightNumber = "TK5"
                }
            });

        //act
        var result = await _handler.Handle(command, CancellationToken.None);

        //assert
        result.Should().NotBeEmpty();
        result.Count.Equals(3);
    }

    [Fact]
    public async Task Handle_Should_Return_ValidationError_WhenNotValidAirport()
    {
        //arrange
        var command = new GetFlightsCommand()
        {
            Origin = "SAW",
            Destination = "AMS",
            DepartDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(2))
        };

        _mockAirportService.Setup(m => m.ValidateAirport(command.Origin, command.Destination, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);
        
        _mockProviderClient.Setup(m => m.SearchFlight(command.Origin, command.Destination, command.DepartDate, command.ReturnDate))
            .ReturnsAsync(new List<FlightDto>
            {
                new FlightDto{
                    Price = 150,
                    ArrivalDateTime = DateTime.Now.AddDays(2).AddHours(5),
                    DepartureDateTime = DateTime.Now.AddDays(2).AddHours(4),
                    FlightNumber = "TK1"
                },
                new FlightDto{
                    Price = 300,
                    ArrivalDateTime = DateTime.Now.AddDays(2).AddHours(3),
                    DepartureDateTime = DateTime.Now.AddDays(2).AddHours(2),
                    FlightNumber = "TK3"
                },
                new FlightDto{
                    Price = 450,
                    ArrivalDateTime = DateTime.Now.AddDays(2).AddHours(7),
                    DepartureDateTime = DateTime.Now.AddDays(2).AddHours(6),
                    FlightNumber = "TK5"
                }
            });

        //act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        //assert
        await act.Should()
            .ThrowAsync<ValidationException>()
            .WithMessage("Please search for valid airports!");
    }
}