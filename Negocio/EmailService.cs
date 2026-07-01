using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class EmailService
    {
        private MailMessage email;
        private SmtpClient server;

        public EmailService()
        {
            // Inicializamos el objeto de correo
            email = new MailMessage();

            // Configuramos el servidor SMTP (Este ejemplo usa los parámetros estándar de Gmail)
            server = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("tpclinica21b@gmail.com", "jfyy ftib ngso blfs"),
                EnableSsl = true
            };
        }

        public void EnviarConfirmacionTurno(string emailDestino, string nombrePaciente, string numeroTurno, string fecha, string hora, string medico)
        {
            try
            {
                email.From = new MailAddress("tpclinica21b@gmail.com", "Clínica Médica");
                email.To.Add(emailDestino);
                email.Subject = $"Confirmación de Turno N° {numeroTurno} - Clínica Médica";

                // Usamos IsBodyHtml = true para que el correo se vea elegante y estructurado
                email.IsBodyHtml = true;

                email.Body = $@"
                    <html>
                    <body style='font-family: Arial, sans-serif; color: #333;'>
                        <div style='max-width: 600px; margin: 0 auto; border: 1px solid #d0e7ff; padding: 20px; border-radius: 8px;'>
                            <h2 style='color: #4a90e2;'>¡Hola, {nombrePaciente}!</h2>
                            <p>Te confirmamos que tu turno ha sido agendado correctamente en nuestro sistema.</p>
                            <hr style='border: none; border-top: 1px solid #eee;' />
                            <p><strong>Detalles de tu cita:</strong></p>
                            <ul>
                                <li><strong>Número de Turno:</strong> {numeroTurno}</li>
                                <li><strong>Médico:</strong> {medico}</li>
                                <li><strong>Fecha:</strong> {fecha}</li>
                                <li><strong>Hora:</strong> {hora} hs</li>
                            </ul>
                            <hr style='border: none; border-top: 1px solid #eee;' />
                            <p style='font-size: 12px; color: #777;'>Por favor, si necesitás cancelar o reprogramar, ingresá al sistema con anticipación.</p>
                        </div>
                    </body>
                    </html>";

                server.Send(email);
            }
            catch (Exception ex)
            {
                // Podés manejar el error o relanzarlo para registrarlo en un log
                throw new Exception("Error al enviar el correo electrónico: " + ex.Message);
            }
            finally
            {
                // Liberamos los recursos para no bloquear el puerto
                email.Dispose();
                email.Dispose();
            }
        }
    }
}
