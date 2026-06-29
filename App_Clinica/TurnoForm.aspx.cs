using Dominio;
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
    public partial class TurnoForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarEspecialidades();
                CargarPacientes();
            }
        }

        protected void ddlEspecialidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            int IdEspecialidad = int.Parse(ddlEspecialidad.SelectedValue);
            if (IdEspecialidad > 0)
            {
                MedicoNegocio negocio = new MedicoNegocio();
          
                ddlMedico.DataSource = negocio.ListarMedicoPorEspecialidad(IdEspecialidad);
                ddlMedico.DataValueField = "IdMedico";
                ddlMedico.DataTextField = "NombreCompleto";
                ddlMedico.DataBind();
                ddlMedico.Items.Insert(0, new ListItem("Seleccione un médico...", "0"));

                ddlMedico.Enabled = true;
            }
            else
            {
                ddlMedico.Enabled = false;
                ddlMedico.Items.Clear();
                lstAgendas.Items.Clear();
                lstAgendas.Enabled = false;
            }
        }

        protected void ddlMedico_SelectedIndexChanged(object sender, EventArgs e)
        {
            int IdMedico = int.Parse(ddlMedico.SelectedValue);
            int IdEspecialidad = int.Parse(ddlEspecialidad.SelectedValue);

            if (IdMedico > 0)
            {
                AgendaMedicoNegocio negocio = new AgendaMedicoNegocio();
                // Trae las agendas del medico para esa especialidad concreta
                var listaAgendas = negocio.ListarAgendasPorMedico(IdMedico, IdEspecialidad);

                lstAgendas.Items.Clear();
                foreach (var agenda in listaAgendas)
                {
                    // Se muestra el formato (Lunes de 08:00 a 12:00)"
                    string texto = $"{agenda.TurnoTrabajo.DiaDeTrabajo} de {agenda.TurnoTrabajo.HoraEntrada:hh\\:mm} a {agenda.TurnoTrabajo.HoraSalida:hh\\:mm}";
                    lstAgendas.Items.Add(new ListItem(texto, agenda.IdAgendaMedico.ToString()));
                }
                lstAgendas.Enabled = true;
            }
            else
            {
                lstAgendas.Items.Clear();
                lstAgendas.Enabled = false;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validaciones
                if (lstAgendas.SelectedIndex == -1)
                {
                    Utils.MostrarAlertaModal(this, "Debe seleccionar una agenda de horarios válida.");
                    return;
                }
                if (string.IsNullOrEmpty(txtFecha.Text) || string.IsNullOrEmpty(txtHora.Text))
                {
                    Utils.MostrarAlertaModal(this, "Debe indicar la Fecha y la Hora del turno.");
                    return;
                }
                if (ddlPaciente.SelectedValue == "0")
                {
                    Utils.MostrarAlertaModal(this, "Debe asignar un paciente al turno.");
                    return;
                }

                Turno nuevoTurno = new Turno();
                nuevoTurno.Fecha = DateTime.Parse(txtFecha.Text);
                nuevoTurno.Hora = TimeSpan.Parse(txtHora.Text);
                nuevoTurno.Observacion = txtObservacion.Text;

                nuevoTurno.Agenda = new AgendaMedico();
                nuevoTurno.Agenda.IdAgendaMedico = int.Parse(lstAgendas.SelectedValue);

                nuevoTurno.Paciente = new Paciente();
                nuevoTurno.Paciente.IdPaciente = int.Parse(ddlPaciente.SelectedValue);

                TurnoNegocio negocio = new TurnoNegocio();
                negocio.Agregar(nuevoTurno); 

                Session["MensajeExito"] = "Turno registrado con éxito.";
                Response.Redirect("TurnosPag.aspx", false);
            }
            catch (Exception ex)
            {
                Utils.MostrarAlertaModal(this, "Error al registrar el turno: " + ex.Message);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("TurnosPag.aspx");
        }

        private void CargarEspecialidades()
        {
            EspecialidadNegocio negocio = new EspecialidadNegocio();
            ddlEspecialidad.DataSource = negocio.Listar();
            ddlEspecialidad.DataValueField = "IdEspecialidad";
            ddlEspecialidad.DataTextField = "Descripcion";
            ddlEspecialidad.DataBind();
            ddlEspecialidad.Items.Insert(0, new ListItem("Seleccione una especialidad...", "0"));
        }

        private void CargarPacientes()
        {
            PacienteNegocio negocio = new PacienteNegocio();
            ddlPaciente.DataSource = negocio.Listar(); 
            ddlPaciente.DataValueField = "IdPaciente";
            ddlPaciente.DataTextField = "NombreCompleto";
            ddlPaciente.DataBind();
            ddlPaciente.Items.Insert(0, new ListItem("Seleccione un paciente...", "0"));
        }
    }
}