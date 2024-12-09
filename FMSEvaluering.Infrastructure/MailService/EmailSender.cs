using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using FMSEvaluering.Application.MailService;

namespace FMSEvaluering.Infrastructure.MailService
{
    public class EmailSender : IEmailSender
    {
        public void SendEmail(string toAddress, string message)
        {
            var smtpServer = "fakemailservice";
            var smtpPort = 1025;

            using (var client = new TcpClient(smtpServer, smtpPort))
            using (var networkStream = client.GetStream())
            using (var writer = new StreamWriter(networkStream, Encoding.ASCII))
            using (var reader = new StreamReader(networkStream, Encoding.ASCII))
            {
                // Read server response
                var response = reader.ReadLine();
                Console.WriteLine("Server: " + response);

                // Send HELO command
                writer.WriteLine("HELO localhost");
                writer.Flush();
                response = reader.ReadLine();
                Console.WriteLine("Server: " + response);

                // Send MAIL FROM command
                writer.WriteLine("MAIL FROM: FMSEvaluering@eva.dk");
                writer.Flush();
                response = reader.ReadLine();
                Console.WriteLine("Server: " + response);

                // Send RCPT TO command
                writer.WriteLine($"RCPT TO:<{toAddress}>");
                writer.Flush();
                response = reader.ReadLine();
                Console.WriteLine("Server: " + response);

                // Send DATA command
                writer.WriteLine("DATA");
                writer.Flush();
                response = reader.ReadLine();
                Console.WriteLine("Server: " + response);

                // Send email headers and body
                writer.WriteLine("Subject: Nyt post");
                writer.WriteLine("From: FMSEvaluering@eva.dk");
                writer.WriteLine($"To: {toAddress}");
                writer.WriteLine();
                writer.WriteLine(message);
                writer.WriteLine(".");
                writer.Flush();
                response = reader.ReadLine();
                Console.WriteLine("Server: " + response);

                // Send QUIT command
                writer.WriteLine("QUIT");
                writer.Flush();
                response = reader.ReadLine();
                Console.WriteLine("Server: " + response);
            }
        }
    }
}
