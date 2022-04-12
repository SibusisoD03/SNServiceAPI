using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SNServiceAPI.Models
{
    public class InvoiceModel
    {
        public int invoiceId { get; set; }
        public DateTime datePurchased { get; set; }
        public int itemId { get; set; }
        public string supplier { get; set; }
        public string uploadInvoice { get; set; }
        public int subTotal { get; set; }
    }
}
