using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SNServiceAPI.Models
{
    public class SendNSNPModel
    {
        public int SendNSNPId { get; set; }
        public string EmisNumber { get; set; }
        public string DistrictName { get; set; }
        public string SchoolName { get; set; }
        public string SchoolEmail { get; set; }
        public DateTime DateReceived { get; set; }
    }
}
