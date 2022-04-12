using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SNServiceAPI.Models
{
    public class FacilitiesVerificationModel
    {
        public int FacilityId { get; set; }
        public string Facilities { get; set; }
        public int VerificationId { get; set; }
    }
}
