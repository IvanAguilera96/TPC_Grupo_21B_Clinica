using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace App_Clinica
{
    public partial class UsuarioForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                try
                {   
                    //Configuración inicial
                    if (!IsPostBack)
                    {
                        PerfilNegocio negocio = new PerfilNegocio();

                        ddlPerfil.DataSource = negocio.Listar();
                        ddlPerfil.DataValueField = "IdPerfil";
                        ddlPerfil.DataTextField = "Descripcion";
                        ddlPerfil.DataBind();
                    }

                //Configuración si recibe ID (modificar)
                if (Request.QueryString["id"] != null)
                {
                    int idUrl = int.Parse(Request.QueryString["id"]);

                    UsuarioNegocio negocio = new UsuarioNegocio();
                    Dominio.Usuario seleccionado = negocio.BuscarPorId(idUrl);

                    if (seleccionado != null)
                    {
                        txtNombre.Text = seleccionado.Nombre;
                        txtContrasenia.Text = seleccionado.Contrasenia;
                        ddlPerfil.SelectedValue = seleccionado.Perfil.IdPerfil.ToString();
                    }
                }

                }
                catch (Exception ex)
                {

                    throw ex;
                }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtContrasenia.Text))
            {
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Text = "Debe completar todos los campos para continuar.";
                return;
            }

            try
            {
                Dominio.Usuario nuevo = new Dominio.Usuario();
                nuevo.Nombre = txtNombre.Text;
                nuevo.Contrasenia = txtContrasenia.Text;

                nuevo.Perfil = new Perfil();
                nuevo.Perfil.IdPerfil = int.Parse(ddlPerfil.SelectedValue);

                UsuarioNegocio negocio = new UsuarioNegocio();
                negocio.Agregar(nuevo);

                Response.Redirect("Usuario.aspx", false);
            }
            catch (Exception)
            {

                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Text = "Error dando de alta al nuevo usuario";
            }
        }
    }
}