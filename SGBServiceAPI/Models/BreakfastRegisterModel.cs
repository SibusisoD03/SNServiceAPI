using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SNServiceAPI.Models
{
    public class BreakfastRegisterModel
    {
		public int BreakfastId { get; set; }
		public DateTime BreakfastDate { get; set; }
		public int BreakfastDay { get; set; }
		public string BreakfastStarttime { get; set; }
		public string BreakfastEndtime { get; set; }
		public string CerealServed { get; set; }
		public int TotNumLearnersFed { get; set; }
		public string PrincipalORNsnpEducatorSignature { get; set; }
	}
}
