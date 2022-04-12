using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SNServiceAPI.Models
{
    public class DonationModel
    {
        public int DonationID { get; set; }
        public string FoodGroup { get; set; }
        public string Product { get; set; }
        public int Quantity1 { get; set; }
        public int NumberofLearnerstobeFed { get; set; }
        public DateTime DateCaptured { get; set; }
        public string ReasonForrequestingDonation { get; set; }
        public string Quarantine { get; set; }
    }
}
