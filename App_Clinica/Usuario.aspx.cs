using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Dominio;

namespace App_Clinica
{
    public partial class Usuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ActualizarGrillaUsuarios();
            }
        }

        private void ActualizarGrillaUsuarios()
        {
            try
            {
                UsuarioNegocio negocio = new UsuarioNegocio();

                dgvUsuario.DataSource = negocio.Listar();
                dgvUsuario.DataBind();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtContrasenia.Text))
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

                //Si grabó el nuevo usuario limpia los campos
                txtNombre.Text ="";
                txtContrasenia.Text = "";

                lblMensaje.ForeColor = System.Drawing.Color.Green;
                lblMensaje.Text = "Usuario registado con éxito.";

                //Actualiza grilla
                ActualizarGrillaUsuarios();
            }
            catch (Exception)
            {

                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Text = "Error dando de alta al nuevo usuario";
            }
        }
    }
}