using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SNServiceAPI.Models
{
    public class MonthlyBudgetModel
    {
        public int id { get; set; }
        public string currentMonth { get; set; }
        public int monthlyFund { get; set; }
        public int balance { get; set; }
    }
}
