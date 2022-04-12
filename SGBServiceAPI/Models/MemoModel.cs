using System;

namespace SNServiceAPI.Models
{
    public class MemoModel
    {
        public int Id { get; set; }
        public int DistrictId { get; set; }
        public string EmisNo { get; set; }
        public int SubmittedById { get; set; }
        public DateTime DateSubmitted { get; set; }
        public string DocumentPath { get; set; }

    }
}
