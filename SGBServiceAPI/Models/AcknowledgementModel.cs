using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SNServiceAPI.Models
{
    public class AcknowledgementModel
    {
        public int AcknowledgementId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string IdNumber { get; set; }
        public DateTime Date { get; set; }
        public string Month { get; set; }
        public Boolean Status { get; set; }
    }
}
