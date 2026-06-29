using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace App_Clinica
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Si el usuario ya está logueado y vuelve a entrar a Login, lo mandamos directo al inicio
                if (Session["UsuarioLogueado"] != null)
                {
                    Response.Redirect("Default.aspx");
                }
            }
        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsuario.Text) || string.IsNullOrWhiteSpace(txtContrasenia.Text))
            {
                lblMensajeError.Text = "Por favor, complete ambos campos.";
                lblMensajeError.Visible = true;
                return;
            }

            try
            {
                UsuarioNegocio negocio = new UsuarioNegocio();
                Dominio.Usuario usuarioValido = negocio.ValidarLogin(txtUsuario.Text.Trim(), txtContrasenia.Text.Trim());

                if (usuarioValido != null)
                {
                    if (!usuarioValido.Estado)
                    {
                        lblMensajeError.Text = "Su usuario se encuentra deshabilitado. Contacte al administrador.";
                        lblMensajeError.Visible = true;
                        return; // Frenamos el flujo aquí
                    }

                    // Si está todo OK y activo (Estado = true), ingresa normalmente
                    Session["UsuarioLogueado"] = usuarioValido;
                    Response.Redirect("Default.aspx", false);
                }
                else
                {
                    lblMensajeError.Text = "Usuario o contraseña incorrectos.";
                    lblMensajeError.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblMensajeError.Text = "Error de conexión: " + ex.Message;
                lblMensajeError.Visible = true;
            }
        }
    }
}