using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SNServiceAPI.Models
{
    public class NSNPModel
    {
        public int NSNPId { get; set; }
        public string Quintile { get; set; }
        public string ChildrenLive { get; set; }
        public int Schoolroll { get; set; }
        public string Facility { get; set; }
        public string EmisNumber { get; set; }
        public string DistrictName { get; set; }
        public string SchoolName { get; set; }
        public string LearnersInfo { get; set; }
        public DateTime DateReceived { get; set; }
        public string Status { get; set; }

    }
}
