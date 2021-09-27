using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;

namespace MailManager
{
    public static class MailProducer
    {
        public static void Sends()
        {
            List<Thread> threads = new List<Thread>();

            for (int i = 0; i < 20; i++)
            {
                int temp = i;
                Thread task = new Thread(() => SendMail($"This is message number {temp + 1}", temp + 1));
                threads.Add(task);
            }

            foreach (Thread item in threads)
            {
                item.Start();
                Thread.Sleep(300);
            }

        }
        public static void SendMail(string str, int num)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtp = new SmtpClient();

                mail.From = new MailAddress("tamkumaha@gmail.com");
                mail.To.Add("atltask07@gmail.com");
               
                mail.Subject = $"Mail Manager Message No.{num}";
                mail.Body = str;

                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.Credentials = new System.Net.NetworkCredential("tamkumaha@gmail.com", "maqa1221");

                smtp.EnableSsl = true;

                smtp.Send(mail);

                Console.WriteLine($"Mail No.{num} sent at: " + DateTime.Now);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
