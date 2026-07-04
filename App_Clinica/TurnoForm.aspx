<%@ Page Title="" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="TurnoForm.aspx.cs" Inherits="App_Clinica.TurnoForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container mt-4" style="max-width: 600px;">
        <div class="card shadow">
            <div class="card-header bg-primary text-white">
                <h3 id="h3TituloTurno" runat="server" class="card-title mb-0">Registrar Nuevo Turno </h3>
            </div>
            <div class="card-body">

                <div class="mb-3">
                    <label class="form-label fw-semibold">Seleccione Especialidad:</label>
                    <asp:DropDownList ID="ddlEspecialidad" runat="server" CssClass="form-select"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlEspecialidad_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>

                <div class="mb-3">
                    <label class="form-label fw-semibold">Seleccione Médico:</label>
                    <asp:DropDownList ID="ddlMedico" runat="server" CssClass="form-select" Enabled="false"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlMedico_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>

                <asp:Label ID="lblDiasAtencion" runat="server" CssClass="d-block text-muted small mb-2" Font-Italic="true"></asp:Label>

                <div class="mb-3">
                    <label class="form-label fw-semibold">Seleccione la Fecha del Turno:</label>
                    <asp:TextBox ID="txtFecha" runat="server" TextMode="Date" CssClass="form-control"
                        AutoPostBack="true" OnTextChanged="txtFecha_TextChanged"></asp:TextBox>
                </div>

                <div class="mb-3">
                    <label class="form-label d-block">Horarios Disponibles para esta fecha:</label>

                    <asp:HiddenField ID="hfHoraSeleccionada" runat="server" />

                    <div class="row row-cols-4 g-2">
                        <asp:Repeater ID="repHorarios" runat="server" OnItemCommand="repHorarios_ItemCommand">
                            <ItemTemplate>
                                <div class="col">
                                    <asp:Button ID="btnSlot" runat="server"
                                        Text='<%# Container.DataItem.ToString().Substring(0, 5) %>'
                                        CommandName="SeleccionarHora"
                                        CommandArgument='<%# Container.DataItem.ToString() %>'
                                        CssClass='<%# ValidarEstiloSlot(Container.DataItem.ToString()) %>'
                                        Enabled='<%# !EsTurnoOcupado(Container.DataItem.ToString()) %>' />
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>

                <div class="mb-3">
                    <label class="form-label fw-semibold">Buscar Paciente:</label>
                    <div class="input-group mb-2">
                        <asp:TextBox ID="txtDniPaciente" runat="server" CssClass="form-control" 
                            placeholder="Ingrese DNI del paciente..." MaxLength="12"></asp:TextBox>
                        <asp:Button ID="btnBuscarPaciente" runat="server" Text="Buscar 🔍" 
                            CssClass="btn btn-primary" OnClick="btnBuscarPaciente_Click" />
                    </div>
                    
                    <div class="p-2 bg-light rounded border">
                        <span class="text-muted small d-block">Paciente seleccionado:</span>
                        <asp:Label ID="lblNombrePaciente" runat="server" CssClass="fw-bold text-dark small" 
                            Text="Ninguno (Ingrese un DNI y busque)"></asp:Label>
                    </div>

                    <asp:HiddenField ID="hfIdPaciente" runat="server" />
                </div>

                <div class="mb-4">
                    <label class="form-label fw-semibold">Observaciones (Motivo de consulta):</label>
                    <asp:TextBox ID="txtObservacion" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="d-flex justify-content-between">
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-outline-secondary" OnClick="btnCancelar_Click" />
                    <asp:Button ID="btnGuardar" runat="server" Text="Confirmar Turno 💾" CssClass="btn btn-success" OnClick="btnGuardar_Click" />
                </div>

            </div>
        </div>
    </div>
</asp:Content>