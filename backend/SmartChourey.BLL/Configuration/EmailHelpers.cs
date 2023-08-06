using Humanizer.Localisation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SmartChourey.BLL.Configuration
{
    public interface IEmailHelpers
    {
        Task<bool> SendSpaceLowEmail(string email, string siteId);
        Task<bool> AddedExistingAsSubAdmin(string email);
        Task<bool> SendSubAdminAccessPrivilegeInfoEmail(string email);
        Task<bool> SendForgotPasswordEmail(string email, string callbackUrl);
    }
    public class EmailHelpers : IEmailHelpers
    {
        private readonly IHelpers _helpers;

        public EmailHelpers(IHelpers helpers)
        {
            _helpers = helpers;
        }

        public async Task<bool> SendSpaceLowEmail(string email, string siteId)
        {
            var spaceUsed = await _helpers.GetSpaceUsed(siteId);
            var spaceLimit = await _helpers.GetSpaceAllocated(siteId);
            var siteName = await _helpers.GetSiteName(siteId);
            bool opStatus = true;
            if (spaceLimit - spaceUsed <= 100)
            {
                var subject = "Low Storage Space Alert";
                var afternameemail = " 様";
                var body = @"<html>
                      <body>
                      <p>" + email + afternameemail + $@"</p><p>サーバ容量が少なくなっています。.</p>
                      <br /><p>利用した容量: <br />{spaceUsed.ToString("0.0")} MB of {spaceLimit} MB ({((spaceUsed / spaceLimit) * 100).ToString("0.0") + " %"})</p>
                        <p>Site Name:<br /> {siteName} ({siteId})</p>
                       </body></html>";
                opStatus = SendEmail(email, body, subject);
            }
            return opStatus;
        }

        public async Task<bool> AddedExistingAsSubAdmin(string email)
        { 
            var subject = "スマート朝礼 サブ管理者に承認されました";
            var message = "スマート朝礼のサブ管理者権限が付与されました。下記URLよりログインし、権限が付与された現場をご確認ください。";


            var afternameemail = " 様";
            string htmlString = @"<html>
                      <body>
                      <p>" + email + afternameemail + ",</p><p> スマート朝礼のサブ管理者権限が付与されました。</p><p>下記URLよりログインし、権限が付与された現場をご確認ください。</p><p>https://www.smartchourey.com/Account/Login/ </p><p>仮パスワードを入力してログインしてください。仮パスワード<strong> Nait@1234 </strong><br/> ※アカウントの安全性を維持するためログイン後、パスワードの変更をお願いします。.</p></body></html>";
            var opStatus = SendEmail(email, htmlString, subject);
            return opStatus;
        }

        public async Task<bool> SendSubAdminAccessPrivilegeInfoEmail(string email)
        {
            var subject = email + "スマート朝礼 サブ管理者に承認されました";

            var afterNameEmail = " 様";
            string htmlString = @"<html>
                      <body>
                      <p> " + email + afterNameEmail + ",</p><p> スマート朝礼のサブ管理者権限が付与されました。</p><p>下記URLよりログインし、権限が付与された現場をご確認ください。</p><p>https://www.smartchourey.com/Account/Login/ </p><p>仮パスワードを入力してログインしてください。仮パスワード<strong> Nait@1234 </strong><br/> ※アカウントの安全性を維持するためログイン後、パスワードの変更をお願いします。.</p></body></html>";

            var opStatus = SendEmail(email, htmlString, subject);
            return opStatus;
        }

        private bool SendEmail(string email, string body, string subject)
        {
            bool opStatus;
            try
            {
                using (SmtpClient client = new SmtpClient())
                {
                    client.Port = 587;
                    client.Host = "smtp.gmail.com";
                    client.EnableSsl = true;
                    client.Timeout = 10000;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;

                    string htmlString = body;
                    var hostEmail = "info.smartchourey@gmail.com";
                    var hostPassword = "uqjqgxdzingqlltw";
                    client.Credentials = new System.Net.NetworkCredential(hostEmail, hostPassword);
                    using (MailMessage message = new MailMessage())
                    {
                        message.Priority = MailPriority.High;
                        message.From = new MailAddress(hostEmail);
                        message.To.Add(email);
                        message.Subject = subject;
                        message.IsBodyHtml = true;
                        message.Body = htmlString;
                        client.Send(message);
                    }
                }
                opStatus = true;
            }
            catch (Exception e)
            {
                opStatus = false;
            }
            return opStatus;
        }

        public async Task<bool> SendForgotPasswordEmail(string email, string callbackUrl)
        {
            var forgotMessage = "以下リンク先をクリックし、パスワードを変更してください。";
            var clickHereMessage = "パスワード変更";

            var subject = email + "パスワード変更";

            string htmlString = forgotMessage + "<br/>" + "<a href=\"" + callbackUrl + "\">" + clickHereMessage + "</a>";

            var opStatus = SendEmail(email, htmlString, subject);
            return opStatus;
        }
    }
}
