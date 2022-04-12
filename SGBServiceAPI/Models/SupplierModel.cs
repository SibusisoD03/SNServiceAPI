using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SNServiceAPI.Models
{
	public class SupplierModel
	{
		public int SupplierId { get; set; }
		public string DistrictCode { get; set; }
		public string DistrictName { get; set; }
		public string SupplierName { get; set; }
		public string VendorNo { get; set; } //mod 
		public string EmailAddress { get; set; }
		public string TelephoneNo { get; set; }
		public string Line1Address { get; set; }
		public string Line2Address { get; set; }
		public string Town { get; set; }
		public string City { get; set; }
		public string Province { get; set; }
		public string PostalCode { get; set; }
		public string ContactPersonName { get; set; }
		public string ContactPersonSurname { get; set; }
		public string ContactNo { get; set; } //mod
		public string CSDNumber { get; set; }
		public DateTime ContractStartDate { get; set; }
		public DateTime ContractEndDate { get; set; }
		public string Status { get; set; }
		public string AssignedSchool { get; set; }
		public string Password { get; set; }
		public string RoleName { get; set; }

	}
}
