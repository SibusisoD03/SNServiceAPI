using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SNServiceAPI.Models
{
    public class SupplierSummaryModel
    {
        public int SupplierSummaryId { get; set; }
        public string Quarter { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string SupplierName { get; set; }
        public string NSNPAllocation { get; set; }
        public string DistrictName { get; set; }
        public int NumberOfLearners { get; set; }
        //public int SchoolId { get; set; }
        public string SchoolName { get; set; }
        public string Month { get; set; }
        public int EmisNo { get; set; }
        public int ClusterNo { get; set; }
        public int CircuitNo { get; set; }
        public List<PerishableModel> PerishablesList { get; set; }
    }
}
