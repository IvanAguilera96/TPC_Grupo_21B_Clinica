<%@ Page Title="" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="App_Clinica.Default1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="container-fluid mt-4">
        <div class="row mb-4">
            <div class="col">
                <h2 class="fw-bold text-dark">Sistema de Gestión</h2>
                <p class="text-muted">Hola, <asp:Label ID="lblNombreUsuario" runat="server" Font-Bold="true" class="text-primary"></asp:Label></p>
            </div>
        </div>

        <%-- PANEL PARA EL ADMINISTRADOR --%>
        <asp:Panel ID="pnlAdmin" runat="server" Visible="false">
            <div class="row g-4">
                <div class="col-12 col-md-4">
                    <div class="card border-0 shadow-sm bg-primary bg-opacity-10 text-primary p-4 rounded">
                        <h6 class="fw-semibold text-uppercase small">Médicos registrados</h6>
                        <h2 class="fw-bold mb-0">
                            <asp:Label ID="lblCantMedicos" runat="server" Text="0"></asp:Label>
                        </h2>
                    </div>
                </div>

                <div class="col-12 col-md-4">
                    <div class="card border-0 shadow-sm bg-success bg-opacity-10 text-success p-4 rounded">
                        <h6 class="fw-semibold text-uppercase small">Pacientes Totales</h6>
                        <h2 class="fw-bold mb-0">
                            <asp:Label ID="lblCantPacientes" runat="server" Text="0"></asp:Label>
                        </h2>
                    </div>
                </div>

                <div class="col-12 col-md-4">
                    <div class="card border-0 shadow-sm bg-dark bg-opacity-10 text-dark p-4 rounded">
                        <h6 class="fw-semibold text-uppercase small text-secondary">Turnos del Mes</h6>
                        <h2 class="fw-bold mb-0">0</h2>
                    </div>
                </div>
            </div>
            </asp:Panel>

        <%-- PANEL PARA LA RECEPCIONISTA --%>
        <asp:Panel ID="pnlRecepcion" runat="server" Visible="false">
            <div class="card bg-white p-4 rounded shadow-sm border mb-4">
                <h4 class="text-secondary fw-bold mb-3">Acciones Rápidas</h4>
                <div class="d-flex gap-3">
                    <a href="PacienteForm.aspx" class="btn btn-outline-primary p-3 fw-bold"><i class="bi bi-person-plus"></i> Nuevo Paciente</a>
                    <a href="TurnosPag.aspx" class="btn btn-outline-success p-3 fw-bold"><i class="bi bi-calendar-plus"></i> Asignar Turno Nuevo</a>
                </div>
            </div>
            </asp:Panel>

        <%-- PANEL PARA EL MÉDICO --%>
        <asp:Panel ID="pnlMedico" runat="server" Visible="false">
            <div class="card bg-white p-4 rounded shadow-sm border">
                <h4 class="text-primary fw-bold mb-3">Agenda del día:</h4>
                <div class="table-responsive">
                    <%-- Grilla con los turnos del día del médico logueado --%>
                    <asp:GridView ID="dgvTurnosDelDia" runat="server" CssClass="table table-hover border-top-0" AutoGenerateColumns="true">
                    </asp:GridView>
                </div>
            </div>
        </asp:Panel>

    </div>

</asp:Content>
