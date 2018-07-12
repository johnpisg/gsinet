//using Limilabs.Client.SMTP;
//using Limilabs.Mail;
//using Limilabs.Mail.Fluent;
//using Limilabs.Mail.Headers;
//using Limilabs.Mail.Templates;
using EASendMail;
using System;
using System.Security.Cryptography.X509Certificates;

namespace Jhon.SendEmailWithSignature
{
    class Program
    {
        private const string _server = "secure.emailsrvr.com";
        private const int _port = 465;
        private const bool _useSSL = true;
        private const string _user = "qbdgsa@bdgsa.net";
        private const string _password = "Bdgsa2017";

        static void Main()
        {
            SmtpMail oMail = new SmtpMail("TryIt");
            SmtpClient oSmtp = new SmtpClient();

            // Set sender email address, please change it to yours
            oMail.From = "jsamame@bdgsa.net";

            // Set recipient email address, please change it to yours
            oMail.To = "jsamame@bdgsa.net";

            // Set email subject
            oMail.Subject = "Asunto: Firma digital Encriptado";

            // Set email body
            oMail.TextBody = "this is a test email with digital signature and encrypted";
            // Your smtp server address
            SmtpServer oServer = new SmtpServer(_server);

            // User and password for ESMTP authentication, if your server doesn't require
            // User authentication, please remove the following codes.
            oServer.User = _user;
            oServer.Password = _password;

            // If your SMTP server requires SSL connection, please add this line
            if (_useSSL)
                oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

            try
            {
                // Find certificate by email adddress in My Personal Store.
                // Once the certificate is loaded to From, the email content
                // will be signed automatically
                //oMail.From.Certificate.FindSubject(oMail.From.Address,
                //    Certificate.CertificateStoreLocation.CERT_SYSTEM_STORE_CURRENT_USER,
                //    "My");
                oMail.From.Certificate.LoadEncryptCertAuto("Test.pfx");
            }
            catch (Exception exp)
            {
                Console.WriteLine("No sign certificate found for <" +
                    oMail.From.Address + ">:" + exp.Message);
            }

            int count = oMail.To.Count;
            for (int i = 0; i < count; i++)
            {
                MailAddress oAddress = oMail.To[i] as MailAddress;
                try
                {
                    // Find certificate by email adddress in My Other Peoples Store.
                    // The certificate can be also imported by *.cer file like this:
                    // oAddress.Certificate.Load("c:\\encrypt1.cer");
                    // Once the certificate is loaded to MailAddress, the email content
                    // will be encrypted automatically
                    //oAddress.Certificate.FindSubject(oAddress.Address,
                    //    Certificate.CertificateStoreLocation.CERT_SYSTEM_STORE_CURRENT_USER,
                    //    "AddressBook");
                    //oAddress.Certificate.Load("Test.cer");
                    oAddress.Certificate.LoadEncryptCertAuto("Test.pfx");
                }
                catch (Exception ep)
                {
                    try
                    {
                        //oAddress.Certificate.FindSubject(oAddress.Address,
                        //    Certificate.CertificateStoreLocation.CERT_SYSTEM_STORE_CURRENT_USER,
                        //    "My");
                        oAddress.Certificate.LoadEncryptCertAuto("Test.pfx");
                    }
                    catch (Exception exp)
                    {
                        Console.WriteLine("No encryption certificate found for <" +
                             oAddress.Address + ">:" + exp.Message);
                    }
                }
            }

            try
            {
                Console.WriteLine("start to send email with digital signature ...");
                oSmtp.SendMail(oServer, oMail);
                Console.WriteLine("email was sent successfully!");
            }
            catch (Exception ep)
            {
                Console.WriteLine("failed to send email with the following error:");
                Console.WriteLine(ep.Message);
            }

            Console.WriteLine("Listo...");
            Console.ReadLine();
        }

        static void Main_Library()
        {
            /*
            // Create test data for the template:
            NotificacionDto dto = new NotificacionDto();
            dto.EstadoDescripcion = "Rechazada".ToUpper();
            dto.Fecha = DateTime.Now.AddDays(-2);
            dto.NombreEmpleado = "Jhon Samamé";
            dto.NombreUsuario = "Director Joáo Samamé";
            dto.RequisicionId = 256;

            // Load and render the template with test data:
            //string html = Template
            //    .FromFile("Notificacion.template")
            //    .DataFrom(dto)
            //    .PermanentDataFrom(DateTime.Now)    // Year is used in the email footer/
            //    .Render();

            //// You can save the HTML for preview:
            //File.WriteAllText("Notificacion.html", html);

            // Create an email:
            IMail email = Mail.Html(Template
                    .FromFile("Notificacion.template")
                    .DataFrom(dto)
                    .PermanentDataFrom(DateTime.Now)
                    .Render())
                .Text("Notificación de la requisición nro." + dto.RequisicionId)
                .AddVisual("Firma.jpg").SetContentId("lemon@id")        // Here we attach an image and assign the content-id.
                .AddAttachment("Archivo.txt").SetFileName("Notif.txt")
                .From(new MailBox("jsamame@bdgsa.net", "Notificador"))
                .To(new MailBox("jsamame@bdgsa.net", "Jhon Samamé"))
                .Subject("Notificación de Requisición " + dto.EstadoDescripcion)
                .SignWith(new X509Certificate2("TestCertificate.pfx", ""))
                .Create();

            // Send this email:
            using (Smtp smtp = new Smtp())              // Connect to SMTP server
            {
                smtp.Connect(_server, _port, _useSSL);                  // Use overloads or ConnectSSL if you need to specify different port or SSL.
                smtp.UseBestLogin(_user, _password);    // You can also use: Login, LoginPLAIN, LoginCRAM, LoginDIGEST, LoginOAUTH methods,
                                                        // or use UseBestLogin method if you want Mail.dll to choose for you.

                ISendMessageResult result = smtp.SendMessage(email);
                Console.WriteLine(result.Status);

                smtp.Close();
            }

            Console.WriteLine("Listo...");
            Console.ReadLine();
            */
        }
    }
}
