using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SNServiceAPI.Models
{
    public class SchoolMenuModel
    {
        public int Id { get; set; }
        public string Day { get; set; }
        public string Menu { get; set; }
        public string Product { get; set; }
        public string SchoolType { get; set; }
    }
}
