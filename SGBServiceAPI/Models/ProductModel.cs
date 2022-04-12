using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SNServiceAPI.Models
{
    public class ProductModel
    {
        public int ProductID { get; set; }
        public int TypeID { get; set; }
        public string TypeDescription { get; set; }
        public int UnitID { get; set; }
        public string UnitDescription { get; set; }
        public int FoodGroupID { get; set; }
        public string FoodGroupDescription { get; set; }
        public float Grammage { get; set; }
        public DateTime DateCreated { get; set; }
        public float GrammageSecondary { get; set; }        
    }

  }
