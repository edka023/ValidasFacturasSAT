using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ValidaFacturassSAT.Clases
{
    public class CMail
    {


        public CMail()
        {

        }
        public bool MailAdjunto(string Destinatarios, string Asunto, string msg, List<string> ListaArchivos)
        {
            bool estatus = false;
            try
            {
                string cliente = ConfigurationManager.AppSettings["cliente"];
                string copy2 = ConfigurationManager.AppSettings["copy2"];
                string User = ConfigurationManager.AppSettings["Remitente"];
                string Pass = ConfigurationManager.AppSettings["PswRemitente"];
                string host = ConfigurationManager.AppSettings["Host"];
                int puerto = int.Parse(ConfigurationManager.AppSettings["Puerto"]);
                string Usuario = User;
                string Password = Pass;
                MailMessage mail = new MailMessage();
                SmtpClient _cliente = new SmtpClient(cliente);
                _cliente.Port = puerto;
                _cliente.Host = host;
                _cliente.UseDefaultCredentials = true;
                _cliente.Credentials = new System.Net.NetworkCredential(Usuario, Password);
                mail.From = new MailAddress(Usuario, "Notificaciones");
                mail.To.Add(Destinatarios);
                mail.CC.Add(copy2);
                mail.Subject = Asunto;
                mail.IsBodyHtml = true;
                foreach (string item in ListaArchivos)
                {
                    if (System.IO.File.Exists(item))
                    {
                        mail.Attachments.Add(new Attachment(item));
                    }
                }
                string html = "<div style='font-family:Verdana;font-size:14px;text-align:justify;padding: 15px 15px 15px 15px'>" +
                    "<div style='background-color:brown;height:70px'>" +
                    "<div style='font-family:Verdana;font-style:oblique;font-size:24px;color:white;text-align:center;padding: 15px 15px 15px 15px'>" +
                    "<strong>Almacenes García </strong></div></div><br>" +
                    "<b>Notificación generada automáticamente por el área de sistemas</b><br><br> <b> Detalles: </b></div>";
                html += "<br>" + msg;
                mail.Body = html;
                _cliente.EnableSsl = true;
                _cliente.Send(mail);
                estatus = true;
            }
            catch (Exception ex)
            {

            }
            return estatus;
        }
    }

}
