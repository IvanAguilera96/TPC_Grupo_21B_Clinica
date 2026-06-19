using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace Utiles
{
    public class Utils
    {
        public static void MostrarAlertaModal(Page pagina, string mensaje)
        {
            string mensajeLimpio = mensaje.Replace("'", "\\'");

            string script = $@"
                <script type='text/javascript'>
                    document.getElementById('lblMensajeModal').innerText = '{mensajeLimpio}';
                    var modalEl = document.getElementById('modalAlerta');
                    var myModal = bootstrap.Modal.getOrCreateInstance(modalEl);
                    myModal.show();
                </script>";

            pagina.ClientScript.RegisterStartupScript(pagina.GetType(), "PopupError", script);
        }
    }
}
