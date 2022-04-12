using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SNServiceAPI.Models
{
    //public class ProductReq
    //{
    //    public int typeID { get; set; }
    //    public int unitID { get; set; }
    //    public int foodGroupID { get; set; }
    //    public string quantity { get; set; }
    //}

 /*   public class SchoolDonation
    {
       
        public int DonationId { get; set; }
        public int PrincipalId { get; set; }
        public string FoodGroup { get; set; }
        public string Product { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }
        public string Reason { get; set; }
        public int NoLearners { get; set; }
        public string CaseNo { get; set; }


    }*/
    public class ProductOrder
    {
        public int ProductOrderId { get; set; }
        public int DonationId { get; set; }
        public int TypeID { get; set; }
        public int UnitID { get; set; }
        public int FoodGroupID { get; set; }
        public int Quantity { get; set; }
    }



    public class ProductDonation
    {
        public List<ProductOrder> products { get; set; }
        public int NoLearners { get; set; }
        public string Reason { get; set; }
        public string CaseNo { get; set; }
        public int DonationId { get; set; }
        public int PrincipalId { get; set; }

            

    }



}
