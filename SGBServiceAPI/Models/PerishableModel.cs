using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SNServiceAPI.Models
{
    public class PerishableModel
    {
        public int PerishableProductId { get; set; }
        public int FoodGroupId { get; set; }
        public string FoodGroupDescription { get; set; }
        public int ProductId { get; set; }
        public string TypeDescription { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }
        public float Grammage { get; set; }
        public int SupplierSummaryId { get; set; }
        public int EmisNumber { get; set; }
        public bool isQualityWrong { get; set; }
        public bool isProductWrong { get; set; }
        public bool isQuantityWrong { get; set; }
        public int QuantityRecieved { get; set; }
        public DateTime ItemDelieveryDate { get; set; }
        public string WrongProductName  { get; set; }
        public string DocumentPath { get; set; }
    }
}
