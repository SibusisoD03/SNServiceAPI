using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SNServiceAPI.Models
{
    public class PerishableProductModel
    {
        public int PerishableProductId { get; set; }
        public string Expected { get; set; }
        public int SupplierSummaryId { get; set; }
        public string ProductType { get; set; }
        public string FoodGroup { get; set; }
    }
}
