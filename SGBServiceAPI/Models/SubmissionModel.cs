using System;

namespace SNServiceAPI.Models
{
    public class SubmissionModel
    {
        public int Id { get; set; }
        public int DistrictId { get; set; }
        public int SubmittedBy { get; set; }
        public DateTime DateSubmitted { get; set; }
        public string DocumentPath { get; set; }
        public string EmisNo { get; set; }
    }
}
