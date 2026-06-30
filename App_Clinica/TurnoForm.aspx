<%@ Page Title="" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="TurnoForm.aspx.cs" Inherits="App_Clinica.TurnoForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <!-- El ScriptManager es obligatorio para que funcione el UpdatePanel -->
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="container mt-4" style="max-width: 600px;">
        <div class="card shadow">
            <div class="card-header bg-primary text-white">
                <h3 class="card-title mb-0">Registrar Nuevo Turno</h3>
            </div>
            <div class="card-body">

                <!-- Envolvemos el corazón del formulario en el UpdatePanel -->
                <asp:UpdatePanel ID="UpdatePanelFormulario" runat="server">
                    <ContentTemplate>

                        <!--Especialidad -->
                        <div class="mb-3">
                            <label class="form-label">Seleccione Especialidad:</label>
                            <asp:DropDownList ID="ddlEspecialidad" runat="server" CssClass="form-select"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlEspecialidad_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>

                        <!--Medico (Se habilita al elegir especialidad) -->
                        <div class="mb-3">
                            <label class="form-label">Seleccione Médico:</label>
                            <asp:DropDownList ID="ddlMedico" runat="server" CssClass="form-select" Enabled="false"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlMedico_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>

                        <!-- El Label para el aviso de dias de atencion -->
                        <asp:Label ID="lblDiasAtencion" runat="server" CssClass="d-block text-muted small mb-2" Font-Italic="true"></asp:Label>

                        <!-- Fecha del Turno con AutoPostBack -->
                        <div class="mb-3">
                            <label class="form-label">Seleccione la Fecha del Turno:</label>
                            <asp:TextBox ID="txtFecha" runat="server" TextMode="Date" CssClass="form-control"
                                AutoPostBack="true" OnTextChanged="txtFecha_TextChanged"></asp:TextBox>
                        </div>

                        <!-- Selector de Horarios en Bloques Modificado -->
                        <div class="mb-3">
                            <label class="form-label d-block">Horarios Disponibles para esta fecha:</label>

                            <!-- Input oculto para guardar la hora exacta en la que el usuario hizo clic -->
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
                    </ContentTemplate>
                </asp:UpdatePanel>
     
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
