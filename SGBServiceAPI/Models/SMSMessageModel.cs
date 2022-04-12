using System;

namespace SNServiceAPI.Models
{
    public class SMSMessageModel
    {
        public int Id { get; set; }
        public string OtpNumber { get; set; }
        public string MobileNumber { get; set; }
        public DateTime OtpTimeCreated { get; set; }
        public DateTime OtpTimeExpiry { get; set; }
        public bool IsActive { get; set; }
        public string Message { get; set; }
    }
}
