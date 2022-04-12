using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SNServiceAPI.Models
{
    public class SchoolsModel
    {
        public string InstitutionName { get; set; }
        public float EmisNumber { get; set; }
        public string Level { get; set; }
        public string DistrictName { get; set; }
        public int CircuitNo { get; set; }
        public int ClusterNo { get; set; }
        public string SchoolEmail { get; set; }
    }
}
