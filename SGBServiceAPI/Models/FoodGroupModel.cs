using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SNServiceAPI.Models
{
    public class FoodGroupModel
    {
        public int FoodGroupID { get; set; }
        public string Description { get; set; }     
        public ICollection<Product> Products { get; set; }
    }
    public class Product
    {
        public TypeModel Type { get; set; }
        public UnitModel Unit { get; set; }
    }
}
