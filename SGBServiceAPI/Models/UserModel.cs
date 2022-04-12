using System;
using System.ComponentModel.DataAnnotations;

namespace SNServiceAPI.Models
{
    ///<Summary>
    /// CreateUserModel
    ///</Summary>
    public class UserModel
    {
     
        public int Id { get; set; }           
        public int RoleId { get; set; }
        public string Citizenship { get; set; }
        public string Persal { get; set; }
        public string IDNumber { get; set; }
        public string Firstname { get; set; }      
        public string Surname { get; set; }
        public string Gender { get; set; }
        public string CellNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; } 
        public string HouseNumber { get; set; }
        public string StreetName { get; set; }
        public string Suburb { get; set; }       
        public string City { get; set; }
        public string PostalCode { get; set; }
        public bool IsInformalSettlement { get; set; }
        //public string UserType { get; set; }
        public string DistrictCode { get; set; }
        public string EmisNumber { get; set; }
        public bool IsEmployee { get; set; }
        public string Status { get; set; }
        public string Level { get; set; }
        public string Region { get; set; }
        public string Position { get; set; }
        public DateTime DateCreated { get; set; }

        //public string CreatedBy { get; set; }
        //public DateTime UpdatedDate { get; set; }
        //public int UpdatedBy { get; set; }
    }


}
