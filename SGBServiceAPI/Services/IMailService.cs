using SNServiceAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SNServiceAPI.Services
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
        Task SendEmailBatchAsync(MimeKit.InternetAddressList Addresses, string Subject, string Body);
        Task SendMemoReminderAsync(MimeKit.InternetAddressList Addresses, string Subject, int days);
        Task SendScheduleReminderAsync(MimeKit.InternetAddressList Addresses, string Subject, int days);
        Task SendTrainingReminderAsync(MimeKit.InternetAddressList Addresses, string Subject, int days);
        Task SendHandoverReminderAsync(MimeKit.InternetAddressList Addresses, string Subject, int days);
        Task SendMeetingReminderAsync(MimeKit.InternetAddressList Addresses, string MeetingSubject, int Days, string MeetingDate, string MeetingTime, string MeetingVenue);
        Task SendParentNotificationEmail(MimeKit.InternetAddressList Addresses, string Subject, SchoolModel schoolInfo, string NominationDate, string ElectionDate, string ElectionStartEndTime, string SGBBoardMembersNoRequired, string SEOName, string Principal, string CurrentDate);
       // Task SendWelcomeEmailAsync(string UserName, string ToEmail, string Pass, string Id);
        Task SendWelcomeEmailAsync(string UserName, string ToEmail, string Persal, string Pass, string Id);
        Task SendMemoEmailAsync(MimeKit.InternetAddressList Addresses, string ProposedElectionDate, string ElectionYear, string DeoNameSurname, string Subject, string DateSend, string DeoTelephone, string DeoEmail, string DistrictName);
        Task SendResetEmailAsync(string UserName, string Hostname, string ToEmail, string Pass, string Id);
        Task SendWelcomeEmailToSupplierAsync(string UserName, string ToEmail, string Pass, string Id);
        Task SendMailToDistrictAsync(string UserName, string DistrictName, string SchoolName, string Id);
        Task SendNotificationEmailToSupplierAsync(string UserName, string ToEmail, string Pass, string Id);
        Task SendNotificationEmailToHeadOfficeAsync(string UserName, string ToEmail, string Pass, string Id);
        Task SendNotificationEmailToSchoolAsync(string UserName, string ToEmail, string Pass, string Id);
    }

}
