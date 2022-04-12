using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SNServiceAPI.Models
{
    public class TypeModel
    {
        public int TypeID { get; set; }
        public string TypeDescription { get; set; }
        public int FoodGroupId { get; set; }
        public int UnitId { get; set; }

    }
}
