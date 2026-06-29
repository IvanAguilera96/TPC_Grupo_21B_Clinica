using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utiles;

namespace App_Clinica
{
    public partial class TurnoPag : System.Web.UI.Page
    {
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Cargar los DropDownList de los filtros por primera vez
                CargarFiltros();
                CargarGrilla();
            }
        }

        protected void ddlFiltroMedico_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarGrilla();
        }

        protected void ddlFiltroEspecialidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarGrilla();
        }

        protected void txtFiltroFecha_TextChanged(object sender, EventArgs e)
        {
            CargarGrilla();
        }

        protected void btnNuevoTurno_Click(object sender, EventArgs e)
        {

        }

        protected void dgvTurnos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "CancelarTurno")
            {
                TurnoNegocio turnoNegocio = new TurnoNegocio();
                // Recuperamos el IdTurno que viaja en el CommandArgument
                int IdTurno = Convert.ToInt32(e.CommandArgument);

                try
                {
                    // Cambiamos el estado en la base de datos a "Cancelado"
                    int IdEstadoCancelado = 1; // 1 = Cancelado en BD

                    turnoNegocio.CambiarEstado(IdTurno, IdEstadoCancelado);

                    Utils.MostrarAlertaModal(this, "El turno fue cancelado correctamente.");

                    CargarGrilla();
                }
                catch (Exception ex)
                {
                    Utils.MostrarAlertaModal(this, "No se pudo cancelar el turno: " + ex.Message);
                }
            }
        }
        private void CargarFiltros()
        {
    
            //Carga Especialidades
            MedicoNegocio medNegocio = new MedicoNegocio();
            ddlFiltroMedico.DataSource = medNegocio.Listar();
            ddlFiltroMedico.DataValueField = "IdMedico";
            ddlFiltroMedico.DataTextField = "NombreCompleto";
            ddlFiltroMedico.DataBind();
            // Seteamos el primer valor en 0
            ddlFiltroMedico.Items.Insert(0, new ListItem("", "0"));

            EspecialidadNegocio espNegocio = new EspecialidadNegocio();
            ddlFiltroEspecialidad.DataSource = espNegocio.Listar();
            ddlFiltroEspecialidad.DataValueField = "IdEspecialidad";
            ddlFiltroEspecialidad.DataTextField = "Descripcion";
            ddlFiltroEspecialidad.DataBind();
            // Seteamos el primer valor en 0
            ddlFiltroEspecialidad.Items.Insert(0, new ListItem("", "0"));

        }

        private void CargarGrilla()
        {
            TurnoNegocio turnoNegocio = new TurnoNegocio();
            // Pasamos los valores de los filtros al método listar
            int idMedico = int.Parse(ddlFiltroMedico.SelectedValue ?? "0");
            int idEspecialidad = int.Parse(ddlFiltroEspecialidad.SelectedValue ?? "0");
            string fecha = txtFiltroFecha.Text;

            // El metodo ListarConFiltros devuelve una List<Turno>
            dgvTurnos.DataSource = turnoNegocio.ListarConFiltros(idMedico, idEspecialidad, fecha);
            dgvTurnos.DataBind();
        }
    }
}