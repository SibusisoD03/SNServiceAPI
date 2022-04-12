using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SNServiceAPI.Services
{
    public class SMSService : ISMSService
    {
        public async void SendSMS(string MobileNumber, string Message)
        {
            var client = new HttpClient();
            await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, "https://www.xml2sms.gsm.co.za/send?username=anisadefreitas&password=education15&number=" + MobileNumber + "&message=" + Message));
        }
    }
}
