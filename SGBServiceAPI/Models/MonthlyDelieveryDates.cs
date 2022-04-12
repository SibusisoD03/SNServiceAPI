using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SNServiceAPI.Models
{
    public class MonthlyDelieveryDates
    {
         public int id { get; set; }   
         public string productType  { get; set; }
         public DateTime deliveryDates  {get; set; }
         
    }
}