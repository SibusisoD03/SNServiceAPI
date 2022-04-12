using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using SNServiceAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SNServiceAPI.Services
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;
        public MailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();
            if (mailRequest.Attachments != null)
            {
                byte[] fileBytes;
                foreach (var file in mailRequest.Attachments)
                {
                    if (file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            fileBytes = ms.ToArray();
                        }
                        builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }
            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();
            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

        //public async Task<bool> SendEmailToMany(string Addresses, string Subject, string Body)
        //{
        //    using (System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient())
        //    {
        //        var basicCredential = new System.Net.NetworkCredential(_mailSettings.Mail, _mailSettings.Password);
        //        using (System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage())
        //        {
        //            System.Net.Mail.MailAddress fromAddress = new System.Net.Mail.MailAddress(_mailSettings.Mail, _mailSettings.DisplayName);

        //            smtpClient.Host = _mailSettings.Host;
        //            smtpClient.UseDefaultCredentials = false;
        //            smtpClient.Credentials = basicCredential;

        //            message.From = fromAddress;
        //            message.Subject = Subject;

        //            message.IsBodyHtml = true;
        //            message.Body = Body;
        //            message.To.Add(Addresses);

        //            try
        //            {
        //                smtpClient.Port = _mailSettings.Port;
        //                smtpClient.EnableSsl = true;
        //                smtpClient.Send(message);
        //            }
        //            catch (Exception ex)
        //            {
        //                //Error, could not send the message
        //                return await Task.FromResult(false);
        //            }
        //        }
        //    }

        //    return await Task.FromResult(true);

        //}

        public async Task SendMailToDistrictAsync(string UserName, string DistrictName, string SchoolName, string Id)
        {
            string FilePath = Directory.GetCurrentDirectory() + "\\Templates\\VerificationRequest.html";
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();
            MailText = MailText.Replace("[username]", UserName).Replace("[district]", DistrictName).Replace("[school]", SchoolName).Replace("[id]", Id);
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse("sdlamini@skhomotech.co.za"));
            email.Subject = $"Dear {UserName}";
            var builder = new BodyBuilder();
            builder.HtmlBody = MailText;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            //smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }


        public async Task SendParentNotificationEmail(InternetAddressList Addresses, string Subject, SchoolModel schoolInfo, string NominationDate, string ElectionDate, string ElectionStartEndTime, string SGBBoardMembersNoRequired, string SEOName, string Principal, string CurrentDate)
        {
            string FilePath = Directory.GetCurrentDirectory() + "\\Templates\\ElectionNoticeTemplate.html";
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();
            MailText = MailText.Replace("[telephone]", schoolInfo.Telephone).Replace("[privatebag]", schoolInfo.PrivateBag).Replace("[email]", schoolInfo.EmailAddress).Replace("[postoffice]", schoolInfo.PostOffice).Replace("[postalcode]", schoolInfo.PostalCode).Replace("[emisno]", schoolInfo.EmisCode).Replace("[streetname]", schoolInfo.StreetName).Replace("[schoolname]", schoolInfo.Institution).Replace("[districtcode]", schoolInfo.District_Code).Replace("[nominationdate]", NominationDate).Replace("[electiondate]", ElectionDate).Replace("[electionstartendtime]", ElectionStartEndTime).Replace("[sgbboardmembersrequired]", SGBBoardMembersNoRequired).Replace("[seoname]", SEOName).Replace("[currentdate]", CurrentDate).Replace("[principal]", Principal);
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            //email.To.Add(MailboxAddress.Parse(Addresses));
            email.To.AddRange(Addresses);
            email.Subject = Subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = MailText;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

        public async Task SendWelcomeEmailAsync(string UserName, string ToEmail, string Persal, string Pass, string Id)
        {
            string FilePath = Directory.GetCurrentDirectory() + "\\Templates\\WelcomeTemplate.html";
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();
            MailText = MailText.Replace("[username]", UserName).Replace("[email]", ToEmail).Replace("[persal]", Persal).Replace("[pass]", Pass).Replace("[id]", Id);
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(ToEmail));
            email.Subject = $"Dear {UserName}";
            var builder = new BodyBuilder();
            builder.HtmlBody = MailText;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            //smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

        public async Task SendWelcomeEmailToSupplierAsync(string UserName, string ToEmail, string Pass, string Id)
        {
            string FilePath = Directory.GetCurrentDirectory() + "\\Templates\\WelcomeSupplierTemplate.html";
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();
            MailText = MailText.Replace("[username]", UserName).Replace("[email]", ToEmail).Replace("[pass]", Pass).Replace("[id]", Id);
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(ToEmail));
            email.Subject = $"Dear {UserName}";
            var builder = new BodyBuilder();
            builder.HtmlBody = MailText;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            //smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
        public async Task SendNotificationEmailToSupplierAsync(string UserName, string ToEmail, string Pass, string Id)
        {
            string FilePath = Directory.GetCurrentDirectory() + "\\Templates\\SupplierNotificationTemplate.html";
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();
            MailText = MailText.Replace("[username]", UserName).Replace("[email]", ToEmail).Replace("[pass]", Pass).Replace("[id]", Id);
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(ToEmail));
            email.Subject = $"Dear {UserName}";
            var builder = new BodyBuilder();
            builder.HtmlBody = MailText;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            //smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
        public async Task SendNotificationEmailToHeadOfficeAsync(string UserName, string ToEmail, string Pass, string Id)
        {
            string FilePath = Directory.GetCurrentDirectory() + "\\Templates\\HeadOfficeNotificationTemplate.html";
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();
            MailText = MailText.Replace("[username]", UserName).Replace("[email]", ToEmail).Replace("[pass]", Pass).Replace("[id]", Id);
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(ToEmail));
            email.Subject = $"Dear {UserName}";
            var builder = new BodyBuilder();
            builder.HtmlBody = MailText;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            //smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

        public async Task SendNotificationEmailToSchoolAsync(string UserName, string ToEmail, string Pass, string Id)
        {
            string FilePath = Directory.GetCurrentDirectory() + "\\Templates\\SchoolNotificationTemplate.html";
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();
            MailText = MailText.Replace("[username]", UserName).Replace("[email]", ToEmail).Replace("[pass]", Pass).Replace("[id]", Id);
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(ToEmail));
            email.Subject = $"Dear {UserName}";
            var builder = new BodyBuilder();
            builder.HtmlBody = MailText;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            //smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
        public async Task SendMemoReminderAsync(InternetAddressList Addresses, string Subject, int days)
        {
            string FilePath = Directory.GetCurrentDirectory() + "\\Templates\\MemoReminderTemplate.html";
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();
            MailText = MailText.Replace("[days]", days.ToString());
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            //email.To.Add(MailboxAddress.Parse(Addresses));
            email.To.AddRange(Addresses);
            email.Subject = Subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = MailText;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }


        public async Task SendScheduleReminderAsync(InternetAddressList Addresses, string Subject, int days)
        {
            string FilePath = Directory.GetCurrentDirectory() + "\\Templates\\ScheduleReminderTemplate.html";
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();
            MailText = MailText.Replace("[days]", days.ToString());
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            //email.To.Add(MailboxAddress.Parse(Addresses));
            email.To.AddRange(Addresses);
            email.Subject = Subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = MailText;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }


        public async Task SendTrainingReminderAsync(InternetAddressList Addresses, string Subject, int days)
        {
            string FilePath = Directory.GetCurrentDirectory() + "\\Templates\\TrainingReminderTemplate.html";
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();
            MailText = MailText.Replace("[days]", days.ToString());
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            //email.To.Add(MailboxAddress.Parse(Addresses));
            email.To.AddRange(Addresses);
            email.Subject = Subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = MailText;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }



        public async Task SendHandoverReminderAsync(InternetAddressList Addresses, string Subject, int days)
        {
            string FilePath = Directory.GetCurrentDirectory() + "\\Templates\\TrainingReminderTemplate.html";
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();
            MailText = MailText.Replace("[days]", days.ToString());
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            //email.To.Add(MailboxAddress.Parse(Addresses));
            email.To.AddRange(Addresses);
            email.Subject = Subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = MailText;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

        public async Task SendMeetingReminderAsync(InternetAddressList Addresses, string MeetingSubject, int Days, string MeetingDate, string MeetingTime, string MeetingVenue)
        {
            string FilePath = Directory.GetCurrentDirectory() + "\\Templates\\MeetingReminderTemplate.html";
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();
            MailText = MailText.Replace("[days]", Days.ToString()).Replace("[meetingsubject]", MeetingSubject).Replace("[meetingdate]", MeetingDate).Replace("[meetingtime]", MeetingTime).Replace("[meetingvenue]", MeetingVenue);
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);

            email.To.AddRange(Addresses);
            email.Subject = MeetingSubject;
            var builder = new BodyBuilder();
            builder.HtmlBody = MailText;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

        public async Task SendResetEmailAsync(string UserName, string Hostname, string ToEmail, string Pass, string Id)
        {
            string FilePath = Directory.GetCurrentDirectory() + "\\Templates\\PasswordResetTemplate.html";
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();
            MailText = MailText.Replace("[username]", UserName).Replace("[hostname]", Hostname).Replace("[email]", ToEmail).Replace("[pass]", Pass).Replace("[id]", Id);
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(ToEmail));
            email.Subject = "Password Reset";
            var builder = new BodyBuilder();
            builder.HtmlBody = MailText;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }


        public async Task SendMemoEmailAsync(MimeKit.InternetAddressList Addresses, string ProposedElectionDate, string ElectionYear, string DeoNameSurname, string Subject, string DateSend, string DeoTelephone, string DeoEmail, string DistrictName)
        {
            string FilePath = Directory.GetCurrentDirectory() + "\\Templates\\MemoTemplate.html";
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();
            MailText = MailText.Replace("[ProposedElectionDate]", ProposedElectionDate).Replace("[ElectionYear]", ElectionYear).Replace("[DeoNameSurname]", DeoNameSurname).Replace("[DateSend]", DateSend).Replace("[DeoTelephone]", DeoTelephone).Replace("[DeoEmail]", DeoEmail).Replace("[DistrictName]", DistrictName);
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            //email.To.Add(MailboxAddress.Parse(Addresses));
            email.To.AddRange(Addresses);
            email.Subject = Subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = MailText;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

        public async Task SendEmailBatchAsync(MimeKit.InternetAddressList Addresses, string Subject, string Body)
        {
            string FilePath = Directory.GetCurrentDirectory() + "\\Templates\\InviteTemplate.html";
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();
            //MailText = MailText.Replace("[Firstname]", ProposedElectionDate)
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);

            email.To.AddRange(Addresses);
            email.Subject = Subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = MailText;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

    }
}
