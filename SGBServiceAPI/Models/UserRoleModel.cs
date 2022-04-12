using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SNServiceAPI.Models
{
    public class UserRoleModel
    {
        public int Id { get; set; }
        //public int RoleId { get; set; }
        public int UserId { get; set; }
        public string EmisCode { get; set; }
        public string DistrictCode { get; set; }
        public string RoleName { get; set; }


    }
}
