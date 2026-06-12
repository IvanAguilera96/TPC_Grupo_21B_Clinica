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
    public partial class MedicoForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            MedicoNegocio negocio = new MedicoNegocio();
            Medico nuevo = new Medico();

            try
            {
                nuevo.Dni = txtDni.Text;
                nuevo.Nombre = txtNombre.Text;
                nuevo.Apellido = txtApellido.Text;
                nuevo.Matricula = int.Parse(txtMatricula.Text);
                nuevo.Estado = chkEstado.Checked;
                negocio.Agregar(nuevo);
                Response.Redirect("MedicoPag.aspx", false);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}