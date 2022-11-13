using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading;

namespace TestWork.Services.Services.EmailSender
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly IConfiguration _configuration;
        private string _host;
        private int _port;
        private bool _useDefaultCredentials;
        private bool _enableSsl;
        private string _email;
        private string _password;

        public EmailSenderService(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

            _host = _configuration["Email:SmtpClient"];
            _email = _configuration["Email:Ardess"];
            _password = _configuration["Email:Password"];
            _port = int.Parse(_configuration["Email:Port"]);
            _useDefaultCredentials = bool.Parse(_configuration["Email:UseDefaultCredentials"]);
            _enableSsl = bool.Parse(_configuration["Email:EnableSsl"]);
        }

        /// <summary>
        /// Email рассылка сообщений по указаным адресам
        /// </summary>
        public void SendTextMessage(
            string textMessage,
            string subject,
            CancellationToken cancellationToken = default,
            params string[] emails)
        {
            var smtpClient = new SmtpClient(_host)
            {
                Port = _port,
                UseDefaultCredentials = _useDefaultCredentials,
                Credentials = new NetworkCredential(_email, _password),
                EnableSsl = _enableSsl,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_email),
                Subject = subject,
                Body = textMessage,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(string.Join(",", emails));

            smtpClient.SendAsync(mailMessage, cancellationToken);
        }
    }
}