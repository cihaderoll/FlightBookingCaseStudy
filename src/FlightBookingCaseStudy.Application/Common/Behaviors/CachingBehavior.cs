using FlightBookingCaseStudy.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBookingCaseStudy.Application.Common.Behaviors
{
    public class CachingBehavior<TRequest, TResponse>(ICachingService cacheService)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICacheable
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var cachedResponse = await cacheService.GetAsync<TResponse>(request.CacheKey, cancellationToken);

            if (cachedResponse != null)
            {
                ///TODO log
                return cachedResponse;
            }

            var response = await next();

            if (response != null)
            {
                await cacheService.SetAsync(request.CacheKey, response, request.Expiration, cancellationToken);
                ///TODO log
            }

            return response;
        }
    }
}
