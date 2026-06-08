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
    public partial class PacienteForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                // Si obtengo ID por la url guardo ID (Quiere modificar)
                if (Request.QueryString["ID"] != null)
                {
                    int IdPaciente = int.Parse(Request.QueryString["ID"].ToString());
                    PacienteNegocio negocio = new PacienteNegocio();
                    Paciente paciente = new Paciente();

                    paciente = negocio.buscoPaciente(IdPaciente);
                    if (paciente != null)
                    {
                        txtDni.Text = paciente.Dni;
                        txtNombre.Text = paciente.Nombre;
                        txtApellido.Text = paciente.Apellido;
                        txtEmail.Text = paciente.Email;
                        txtTelefono.Text = paciente.Telefono;
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
            PacienteNegocio negocio = new PacienteNegocio();
            Paciente nuevo = new Paciente();

            try
            {
                nuevo.Apellido = txtApellido.Text;
                nuevo.Nombre = txtNombre.Text;
                // validar que no tenga mas de 8 caracteres DNI
                nuevo.Dni = txtDni.Text;
                nuevo.Email = txtEmail.Text;
                nuevo.Telefono = txtTelefono.Text;

                negocio.Agregar(nuevo);

                // Limpio campos si agrego el paciente
                txtApellido.Text = "";
                txtNombre.Text = "";
                txtDni.Text = "";
                txtEmail.Text = "";
                txtTelefono.Text = "";

                // Trata de hacer aparecer el msj en la pantalla principal de "Paciente registado con éxito."

                //lblMensaje.ForeColor = System.Drawing.Color.Green;
                //lblMensaje.Text = "Paciente registado con éxito.";
                Response.Redirect("PacientePag.aspx", false);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}