using MediatR;

namespace FlightBookingCaseStudy.Application.Use_Cases.Commands
{
    public class SearchCommandHandler : IRequestHandler<SearchCommand, int>
    {
        public SearchCommandHandler()
        {
            
        }

        public Task<int> Handle(SearchCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
