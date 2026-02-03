using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBookingCaseStudy.Application.Use_Cases.Commands.Book
{
    public class BookFlightCommandValidator : AbstractValidator<BookFlightCommand>
    {
        public BookFlightCommandValidator()
        {
        }
    }
}
