using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SNServiceAPI.Models;
using SNServiceAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SNServiceAPI.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly IDapper _dapper;
        private readonly IMailService mailService;
        public MailController(IDapper dapper, IMailService mailService)
        {
            this._dapper = dapper;
            this.mailService = mailService;
        }

        [HttpPost("Send")]
        public async Task<IActionResult> SendMail([FromForm] MailRequest request)
        {
            try
            {
                await mailService.SendEmailAsync(request);
                return Ok();
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        [HttpPost(nameof(SendWelcomeMail))]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> SendWelcomeMail(string UserName, string Id, string Pass)
        {
            try
            {
                var result = await Task.FromResult(_dapper.Get<UserModel>($"Select * from tblUsers where Id={Id}",null, commandType: System.Data.CommandType.Text));
                await mailService.SendWelcomeEmailAsync(UserName,result.EmailAddress, result.Persal, Pass, Id);
        //await mailService.SendWelcomeEmailAsync(UserName, result.EmailAddress, Pass, Id);

                return Ok();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //Send email to district user
        [HttpPost(nameof(SendMailToDistrict))]       
        public async Task<IActionResult> SendMailToDistrict(string UserName, string Id, string District, string School)
        {
            try
            {
                var result = await Task.FromResult(_dapper.Get<VerificationFormModel>($"Select * from tblVerification where VerificationId={Id}", null, commandType: System.Data.CommandType.Text));
                await mailService.SendMailToDistrictAsync(UserName, Id, District, School);
                return Ok();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost(nameof(SendWelcomeEmailToSupplierAsync))]
        public async Task<IActionResult> SendWelcomeEmailToSupplierAsync(string UserName, string Id, string Pass)
        {
            try
            {
                var result = await Task.FromResult(_dapper.Get<SupplierModel>($"Select * from tblSupplier where SupplierId={Id}", null, commandType: System.Data.CommandType.Text));
                await mailService.SendWelcomeEmailToSupplierAsync(UserName, result.EmailAddress, Pass, Id);
                return Ok();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost(nameof(SendNotificationEmailToSupplierAsync))]
        public async Task<IActionResult> SendNotificationEmailToSupplierAsync(string UserName, string Id, string Pass)
        {
            try
            {
                var result = await Task.FromResult(_dapper.Get<SupplierModel>($"Select * from tblSupplier where SupplierId={Id}", null, commandType: System.Data.CommandType.Text));
                await mailService.SendNotificationEmailToSupplierAsync(UserName, result.EmailAddress, Pass, Id);
                return Ok();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost(nameof(SendNotificationEmailToHeadOfficeAsync))]
        public async Task<IActionResult> SendNotificationEmailToHeadOfficeAsync(string UserName, string Id, string Pass)
        {
            try
            {
                var result = await Task.FromResult(_dapper.Get<SupplierModel>($"Select * from tblSupplier where SupplierId={Id}", null, commandType: System.Data.CommandType.Text));
                await mailService.SendNotificationEmailToHeadOfficeAsync(UserName, result.EmailAddress, Pass, Id);
                return Ok();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost(nameof(SendNotificationEmailToSchoolAsync))]
        public async Task<IActionResult> SendNotificationEmailToSchoolAsync(string UserName, string Id, string Pass)
        {
            try
            {
                var result = await Task.FromResult(_dapper.Get<SupplierModel>($"Select * from tblSupplier where SupplierId={Id}", null, commandType: System.Data.CommandType.Text));
                await mailService.SendNotificationEmailToSchoolAsync(UserName, result.EmailAddress, Pass, Id);
                return Ok();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost(nameof(SendEmailInvite))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> SendEmailInvite(EmailInviteeModel emailInviteeModel)
        {
            try
            {
                if (emailInviteeModel.Invitees.Length > 0)
                {
                    MimeKit.InternetAddressList inviteeMailBoxeslist = new MimeKit.InternetAddressList();
                    foreach (string inviteeEmail in emailInviteeModel.Invitees)
                    {
                        if (inviteeEmail.Contains("@"))
                        {
                            inviteeMailBoxeslist.Add(new MimeKit.MailboxAddress(inviteeEmail));
                        }
                    }
                    await mailService.SendEmailBatchAsync(inviteeMailBoxeslist, emailInviteeModel.Subject, emailInviteeModel.Body);
                }
                return Ok();
            }
            catch (Exception ex)
            {

                return Ok(ex.StackTrace);
            }
        }

        [HttpPost(nameof(SendResetMail))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> SendResetMail(string Hostname, string UserName, string Id)
        {
            try
            {
                var result = await Task.FromResult(_dapper.Get<UserModel>($"Select * from [tblUsers] where Id={Id}", null, commandType: System.Data.CommandType.Text));
                await mailService.SendResetEmailAsync(UserName, Hostname, result.EmailAddress, result.Password, Id);
                return Ok();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
