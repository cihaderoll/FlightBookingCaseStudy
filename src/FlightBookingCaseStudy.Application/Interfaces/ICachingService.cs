using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBookingCaseStudy.Application.Interfaces
{
    public interface ICachingService<T>
    {
        Task<List<T>> GetListAsync(string key, CancellationToken cancellationToken = default);
        Task<T> GetAsync(string key, CancellationToken cancellationToken = default);
        Task SetListAsync(string key, List<T> value, TimeSpan? expiration = null, CancellationToken cancellationToken = default);
        Task SetAsync(List<T> valueList, TimeSpan? expiration = null, CancellationToken cancellationToken = default);
        Task RemoveAsync(string key, CancellationToken cancellationToken = default);
    }
}
