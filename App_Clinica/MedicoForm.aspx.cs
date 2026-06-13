using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace App_Clinica
{
    public partial class MedicoForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Si viene ID, es modo edición
                if (Request.QueryString["id"] != null)
                {
                    int idMedico = int.Parse(Request.QueryString["id"]);

                    MedicoNegocio negocio = new MedicoNegocio();
                    Medico seleccionado = negocio.BuscarPorId(idMedico);

                    if (seleccionado != null)
                    {
                        txtDni.Text = seleccionado.Dni;
                        txtNombre.Text = seleccionado.Nombre;
                        txtApellido.Text = seleccionado.Apellido;
                        txtMatricula.Text = seleccionado.Matricula.ToString();
                        chkEstado.Checked = seleccionado.Estado;
                    }
                }
                else
                {
                    chkEstado.Checked = true;
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                MedicoNegocio negocio = new MedicoNegocio();
                Medico nuevo = new Medico();

                nuevo.Dni = txtDni.Text;
                nuevo.Nombre = txtNombre.Text;
                nuevo.Apellido = txtApellido.Text;
                nuevo.Matricula = int.Parse(txtMatricula.Text);
                nuevo.Estado = chkEstado.Checked;

                if (Request.QueryString["id"] != null)
                {
                    nuevo.IdMedico = int.Parse(Request.QueryString["id"]);
                    negocio.Modificar(nuevo);
                    Session["MensajeExito"] = "Médico modificado correctamente.";
                }
                else
                {
                    negocio.Agregar(nuevo);
                    Session["MensajeExito"] = "Médico registrado correctamente.";
                }

                Response.Redirect("MedicoPag.aspx", false);
            }
            catch (Exception ex)
            {
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Text = "Error: " + ex.Message;
                lblMensaje.Visible = true;
            }
        }
    }
}