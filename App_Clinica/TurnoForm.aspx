<%@ Page Title="" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="TurnoForm.aspx.cs" Inherits="App_Clinica.TurnoForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container mt-4" style="max-width: 600px;">
        <div class="card shadow">
            <div class="card-header bg-primary text-white">
                <h3 class="card-title mb-0">Registrar Nuevo Turno</h3>
            </div>
            <div class="card-body">
                
                <!--Especialidad -->
                <div class="mb-3">
                    <label class="form-label">1. Seleccione Especialidad:</label>
                    <asp:DropDownList ID="ddlEspecialidad" runat="server" CssClass="form-select" 
                        AutoPostBack="true" OnSelectedIndexChanged="ddlEspecialidad_SelectedIndexChanged"></asp:DropDownList>
                </div>

                <!--Medico (Se habilita al elegir especialidad) -->
                <div class="mb-3">
                    <label class="form-label">2. Seleccione Médico:</label>
                    <asp:DropDownList ID="ddlMedico" runat="server" CssClass="form-select" Enabled="false"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlMedico_SelectedIndexChanged"></asp:DropDownList>
                </div>

                <!--Horarios de Agenda Disponibles-->
                <div class="mb-3">
                    <label class="form-label">Horarios de atención de este médico:</label>
                    <asp:ListBox ID="lstAgendas" runat="server" CssClass="form-control" Enabled="false" SelectionMode="Single"></asp:ListBox>
                    <small class="text-muted">Seleccione la agenda específica para este turno.</small>
                </div>

                <!--Datos del Turno (Fecha y Hora libre)-->
                <div class="row">
                    <div class="col-md-6 mb-3">
                        <label class="form-label">3. Fecha del Turno:</label>
                        <asp:TextBox ID="txtFecha" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-6 mb-3">
                        <label class="form-label">4. Hora del Turno:</label>
                        <asp:TextBox ID="txtHora" runat="server" TextMode="Time" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

                <!--Paciente-->
                <div class="mb-3">
                    <label class="form-label">5. Seleccione Paciente:</label>
                    <asp:DropDownList ID="ddlPaciente" runat="server" CssClass="form-select"></asp:DropDownList>
                </div>

                <!--Observaciones opcional-->
                <div class="mb-4">
                    <label class="form-label">Observaciones (Motivo de consulta):</label>
                    <asp:TextBox ID="txtObservacion" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control"></asp:TextBox>
                </div>

                <!-- Botones de Accion -->
                <div class="d-flex justify-content-between">
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-outline-secondary" OnClick="btnCancelar_Click" />
                    <asp:Button ID="btnGuardar" runat="server" Text="Confirmar Turno 💾" CssClass="btn btn-success" OnClick="btnGuardar_Click" />
                </div>

            </div>
        </div>
    </div>

</asp:Content>
