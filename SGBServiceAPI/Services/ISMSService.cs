using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SNServiceAPI.Services
{
    public interface ISMSService
    {
        void SendSMS(string MobileNumber, string Message);

    }
}
