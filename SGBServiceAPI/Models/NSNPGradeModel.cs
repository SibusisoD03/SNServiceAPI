using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SNServiceAPI.Models
{
    public class NSNPGradeModel
    {
        public int NSNPGradeID { get; set; }
        public string Grade { get; set; }
        public int NoOfLearners { get; set; }
        public string OtherGrade { get; set; }
    }
}
