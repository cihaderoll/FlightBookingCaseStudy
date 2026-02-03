using FlightBookingCaseStudy.Application.Common.Settings;
using FlightBookingCaseStudy.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBookingCaseStudy.Application.Common.Behaviors
{
    public class CachingBehavior<TRequest, TResponse>(ICachingService cacheService, IOptions<CacheSettings> _cacheSettings)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICacheable
    {
        private readonly CacheSettings cacheSettings = _cacheSettings.Value;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var cachedResponse = await cacheService.GetAsync<TResponse>(cacheSettings.CacheKeyPrefix, cancellationToken);

            if (cachedResponse != null)
            {
                ///TODO log
                return cachedResponse;
            }

            var response = await next();

            if (response != null)
            {
                await cacheService.SetAsync(cacheSettings.CacheKeyPrefix, response, TimeSpan.FromMinutes(cacheSettings.ExpirationInMinutes), cancellationToken);
                ///TODO log
            }

            return response;
        }
    }
}
