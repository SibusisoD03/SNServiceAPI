using Microsoft.AspNetCore.Mvc;
using SNServiceAPI.Services;
using System.Threading.Tasks;
using Dapper;
using SNServiceAPI.Models;
using System.Data;
using System.Net.Http;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace SNServiceAPI.Controllers.v1
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    [Route("api/[controller]")]
    [ApiController]
    public class SMSController : ControllerBase
    {
        private readonly IDapper _dapper;
        private readonly IConfiguration _config;
        public SMSController(IDapper dapper, IConfiguration config)
        {
            _dapper = dapper;
            _config = config;
        }

        [HttpPost(nameof(SendOTP))]
        public async Task<string> SendOTP(string IDNumber)
        {
            string randomOTP = GenerateOTPNumber();

            var parentMobileRequest = Task.FromResult(_dapper.Get<SMSMessageModel>($"SELECT DISTINCT CellNumber FROM [dbo].[tblUsers] WHERE [IDNumber]='{IDNumber}'", null,
                    commandType: CommandType.Text));

            string parentMobileNumber = parentMobileRequest.Result.MobileNumber;

            var dbparams = new DynamicParameters();
            dbparams.Add("@OtpNumber", randomOTP);
            dbparams.Add("@MobileNumber", parentMobileNumber);
            dbparams.Add("@OtpTimeExpiry", DateTime.Now.AddHours(4));
            dbparams.Add("@Message", String.Format("Never share this OTP with anyone:{0} Queries? 0861004000. Sent {1}", randomOTP, DateTime.Now.ToString()));
            dbparams.Add("@IsActive", true);
            dbparams.Add("@Id", 0, DbType.Int32, ParameterDirection.Output);

            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[SP_SendOTP]"
                , dbparams,
                commandType: CommandType.StoredProcedure));
            var retVal = dbparams.Get<int>("@Id");

            SendSMS(parentMobileNumber, String.Format("Never share this OTP with anyone:{0} Queries? 0861004000. Sent {1}", randomOTP, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")));

            return parentMobileNumber;
        }

        [HttpPost(nameof(AuthenticateOTP))]
        public async Task<bool> AuthenticateOTP(string MobileNumber, string OTP)
        {
            var currentDate = DateTime.Now;
            var result = await Task.FromResult(_dapper.Get<SMSMessageModel>($"Select [OtpTransactionId] Id,[OtpNumber],[MobileNumber],[OtpTimeCreated],[OtpTimeExpiry],[IsActive],[Message] from [OTPTransaction] where MobileNumber = {MobileNumber} AND OtpNumber = {OTP} AND OtpTimeExpiry>'{currentDate}' AND IsActive=1", null,
                    commandType: CommandType.Text));
            
            if (result != null)
            {
               //Update the OTP status to prevent reuse
               await Task.FromResult(_dapper.Get<SMSMessageModel>($"UPDATE [OTPTransaction] SET IsActive=0 Where OtpTransactionId = {result.Id}", null,
                    commandType: CommandType.Text));
                return true;
            }

            return false;
        }
         
        [HttpGet(nameof(ResendOTP))]
        public bool ResendOTP(int Id)
        {
            var smsmessage = Task.FromResult(_dapper.Get<SMSMessageModel>($"Select * from [OTPTransaction] where Id = {Id} AND IsActive=1", null,
                    commandType: CommandType.Text));

            //TODO: send sms again
            return true;
        }

        private async void SendSMS(string MobileNumber, string Message)
        {
            var client = new HttpClient();
            await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, "https://www.xml2sms.gsm.co.za/send?username=anisadefreitas&password=education16&number=" + MobileNumber + "&message=" + Message));

        }

        private string GenerateRandomOTP(int iOTPLength, string[] saAllowedCharacters)
        {
            string sOTP = String.Empty;
            string sTempChars = String.Empty;
            Random rand = new Random();

            for (int i = 0; i < iOTPLength; i++)
            {
                int p = rand.Next(0, saAllowedCharacters.Length);
                sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                sOTP += sTempChars;

            }
            return sOTP;

        }

        private string GenerateOTPNumber()
        {
            string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };

            string randomOTP = GenerateRandomOTP(4, saAllowedCharacters);

            return randomOTP;
        }

        [HttpPost(nameof(SendWelcomeSMS))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public SMSResponseModel SendWelcomeSMS(string UserName, string Id)
        {
            var result = Task.FromResult(_dapper.Get<UserModel>($"Select * from [tblUsers] where Id = {Id}", null, commandType: CommandType.Text));

            string message = $"Dear {UserName}, You are currently registered on the system and your temporary password is {result.Result.Password}. Click this link {_config["Hostname"]}/disclaimer?userid={Id} to activate your account.";
            SendSMS(result.Result.CellNumber, message);

            var res = new SMSResponseModel();
            res.Message = message;
            return res;
        }

        [HttpPost(nameof(SendResetPasswordSMS))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public SMSResponseModel SendResetPasswordSMS(string UserName, string Id)
        {
            var result = Task.FromResult(_dapper.Get<UserModel>($"Select * from [tblUsers] where Id = {Id}", null, commandType: CommandType.Text));

            string message = $"Dear {UserName}, You have requested to change your password and your temporary password is {result.Result.Password}. Click this link {_config["Hostname"]}/disclaimer?userid={Id} to reset your account.";
            SendSMS(result.Result.CellNumber, message);

            var res = new SMSResponseModel();
            res.Message = message;
            return res;
        }
    }
}
