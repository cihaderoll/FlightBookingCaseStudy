using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace FlightBookingCaseStudy.Application.Common.Settings
{
    public class CacheSettings
    {
        public string CacheKeyPrefix { get; set; }
        public int ExpirationInMinutes { get; set; }
    }
}
