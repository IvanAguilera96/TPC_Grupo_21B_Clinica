using System;
using System.Web.UI;

namespace App_Clinica
{
    public static class Utils
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