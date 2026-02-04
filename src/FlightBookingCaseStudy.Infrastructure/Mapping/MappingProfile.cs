using AutoMapper;
using FlightBookingCaseStudy.Application.Use_Cases.Commands.Search;
using FlightBookingCaseStudy.Domain.Models;

namespace FlightBookingCaseStudy.Infrastructure.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<FlightOption, FlightDto>().AfterMap((src, dest) =>
            {
                dest.FlightId = Guid.NewGuid().ToString();
            });
        }
    }
}
