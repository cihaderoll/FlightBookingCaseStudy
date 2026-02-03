using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBookingCaseStudy.Domain.Entities
{
    public class Airport
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
