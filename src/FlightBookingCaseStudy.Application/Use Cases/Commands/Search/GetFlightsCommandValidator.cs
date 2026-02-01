using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBookingCaseStudy.Application.Use_Cases.Commands.Search
{
    public class GetFlightsCommandValidator : AbstractValidator<GetFlightsCommand>
    {
        public GetFlightsCommandValidator()
        {
            RuleFor(x => x.Origin)
                .NotEmpty().WithMessage("Origin cannot be empty!")
                .Length(3).WithMessage("Please check origin code!");

            RuleFor(x => x.Destination)
                .NotEmpty().WithMessage("Destination cannot be empty!")
                .Length(3).WithMessage("Please check destination code!");

            RuleFor(x => x.DepartDate)
                .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now))
                .WithMessage("Depart date cannot be in the past!");
        }
    }
}
