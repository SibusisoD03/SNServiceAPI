using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SNServiceAPI.Models
{
    public class FeedingCalendarModel
    {
        public int FeedingId { get; set; }
        public string TaskName { get; set; }
        public string Quarter { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
