using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using Dominio;

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

        public static class Seguridad
        {
            /// <summary>
            /// Valida si el usuario logueado pertenece a alguno de los roles permitidos.
            /// </summary>
            /// <param name="pagina">La página actual (this)</param>
            /// <param name="rolesPermitidos">Lista de roles separados por coma</param>
            public static void ValidarAcceso(System.Web.UI.Page pagina, params string[] rolesPermitidos)
            {
                // 1. Reaseguro por si la sesión se cayó o es nula
                if (pagina.Session["UsuarioLogueado"] == null)
                {
                    pagina.Response.Redirect("Login.aspx", false);
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                    return;
                }

                Usuario user = (Usuario)pagina.Session["UsuarioLogueado"];

                // 2. Comparamos el rol del usuario contra los roles que le pasemos al método
                // Si el rol del usuario NO está en la lista de permitidos...
                if (!rolesPermitidos.Contains(user.Perfil.Descripcion))
                {
                    // ...lo mandamos a la home
                    pagina.Response.Redirect("Default.aspx", false);
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                }
            }
        }
    }
}
