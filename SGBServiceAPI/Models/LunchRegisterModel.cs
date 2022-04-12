using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SNServiceAPI.Models
{
    public class LunchRegisterModel
    {
		public int LunchId { get; set; }
		public DateTime LunchDate { get; set; }
		public int LunchDay { get; set; }
		public string LunchStarttime { get; set; }
		public string LunchEndtime { get; set; }
		public string CerealServed { get; set; }
		public int TotnNumLearnersFed { get; set; }
		public string PrincipalORNsnpEducatorSignature { get; set; }
	}
}
