using FlightBookingCaseStudy.Application.Interfaces;
using FlightBookingCaseStudy.Application.Use_Cases.Commands.Search;
using FlightBookingCaseStudy.Infrastructure.Client;
using FlightBookingCaseStudy.WebAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddClients();

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(GetFlightsCommand).Assembly));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
