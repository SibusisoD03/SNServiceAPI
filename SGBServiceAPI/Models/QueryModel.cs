using System;

namespace SNServiceAPI.Models
{
    public class QueryModel
    {
        public int Id { get; set; }
        public string AssignedTo { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public DateTime CapturedDate { get; set; }
        public string Status { get; set; }
        public int PerishableProductId { get; set; }
        public string ClosedBy { get; set; }
        public string Quarter { get; set; }
        public string Month { get; set; }
    }
}
