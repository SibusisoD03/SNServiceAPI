using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SNServiceAPI.Models
{
    public class BreakfastModel
    {
        public int BreakfastId { get; set; }
        public string Breakfast { get; set; }
        public string Unit { get; set; }
        public int FoodGroupID { get; set; }
        public string Primary { get; set; }
        public string Secondary { get; set; }
    }
}
