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
            if (!IsPostBack)
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
                        chkEstado.Checked = paciente.Estado;
                        txtFechaNacimiento.Text = paciente.FechaNacimiento.ToString("yyyy-MM-dd");
                    }
                }
            }

        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            PacienteNegocio negocio = new PacienteNegocio();
            Paciente aux = new Paciente();

            try
            {
                if (string.IsNullOrWhiteSpace(txtApellido.Text) ||
                    string.IsNullOrWhiteSpace(txtNombre.Text) ||
                    string.IsNullOrWhiteSpace(txtDni.Text) ||
                    string.IsNullOrWhiteSpace(txtEmail.Text) ||
                    string.IsNullOrWhiteSpace(txtTelefono.Text) ||
                    string.IsNullOrWhiteSpace(txtFechaNacimiento.Text))
                {
                    lblMensaje.Text = "Todos los campos son obligatorios.";
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    lblMensaje.Visible = true;
                    return;
                }

                //Validaciones para la fecha de nac
                if (DateTime.TryParse(txtFechaNacimiento.Text, out DateTime fechaIngresada))
                {
                    // Validamos que el año no sea menor a 1926
                    if (fechaIngresada.Year < 1926)
                    {
                        lblMensaje.Text = "La fecha no puede ser anterior al 1926.";
                        lblMensaje.ForeColor = System.Drawing.Color.Red;
                        lblMensaje.Visible = true;
                        return;
                    }

                    if (fechaIngresada > DateTime.Today)
                    {
                        lblMensaje.Text = "La fecha no puede ser mayor a hoy.";
                        lblMensaje.ForeColor = System.Drawing.Color.Red;
                        lblMensaje.Visible = true;
                        return;
                    }
                }
                else
                {
                    lblMensaje.Text = "Formato de fecha inválido";
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    lblMensaje.Visible = true;
                    return;
                }

                aux.Apellido = txtApellido.Text;
                aux.Nombre = txtNombre.Text;
                aux.FechaNacimiento = DateTime.Parse(txtFechaNacimiento.Text);
                aux.Dni = txtDni.Text;
                aux.Email = txtEmail.Text;
                aux.Telefono = txtTelefono.Text;
                aux.Estado = chkEstado.Checked;

                if (Request.QueryString["ID"] != null)
                {
                    aux.IdPaciente = Convert.ToInt32(Request.QueryString["ID"]);
                    negocio.Modificar(aux);
                    Session["MensajeExito"] = "Paciente modificado con éxito.";
                }
                else
                {
                    negocio.Agregar(aux);
                    Session["MensajeExito"] = "Paciente registrado con éxito.";
                }

                Response.Redirect("PacientePag.aspx", false);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}