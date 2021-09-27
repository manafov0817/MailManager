using AE.Net.Mail;
using MailManager.Configuration;
using MailManager.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MailManager
{
    public class MailConsumer : CronJobService
    {
        public static ImapClient IC;

        private readonly ILogger<MailConsumer> _logger;

        public MailConsumer(IScheduleConfig<MailConsumer> config, ILogger<MailConsumer> logger)
            : base(config.CronExpression, config.TimeZoneInfo)
        {
            _logger = logger;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Mail Consumer starts.");
            return base.StartAsync(cancellationToken);
        }

        public override Task DoWork(CancellationToken cancellationToken)
        {
            GetMails();

            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Mail Consumer is stopping.");
            return base.StopAsync(cancellationToken);
        }


        public void GetMails()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            IC = new ImapClient("imap.gmail.com", "atltask07@gmail.com", "maqa1221", AuthMethods.Login, 993, true);

            IC.SelectMailbox("INBOX");

            List<MailMessage> messages = new List<MailMessage>();

            int messageCount = IC.GetMessageCount();

            for (int i = 0; i < messageCount; i++)
            {

                if (messageCount == i || i == 21)
                    break;

                messages.Add(IC.GetMessage(messageCount - i - 1));

            }

            foreach (var message in messages)
            {
                Console.WriteLine(message.Subject);
                Console.WriteLine(message.Body);
                _logger.LogWarning ($"Subject: ({message.Subject}) with Message: ({message.Body})");
            }

            Console.ReadLine();
        }

    }
}
