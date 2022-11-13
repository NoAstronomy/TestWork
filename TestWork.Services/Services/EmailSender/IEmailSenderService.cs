using System.Threading;

namespace TestWork.Services.Services.EmailSender
{
    public interface IEmailSenderService
    {
        /// <summary>
        /// Email рассылка сообщений по указаным адресам
        /// </summary>
        void SendTextMessage(string textMessage, string subject, CancellationToken cancellationToken = default, params string[] emails);
    }
}