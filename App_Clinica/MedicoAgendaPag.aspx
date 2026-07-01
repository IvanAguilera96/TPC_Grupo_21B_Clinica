<%@ Page Title="" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="MedicoAgendaPag.aspx.cs" Inherits="App_Clinica.MedicoAgendaPag" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="row justify-content-center">
        <div class="col-md-6 d-flex flex-column align-items-center">

            <asp:Label ID="lblMensajeGrilla" runat="server" CssClass="alert alert-success d-block text-center mb-3" Visible="false"></asp:Label>

            <h3 class="mb-3">Horarios Asignados</h3>

            <div id="contenedorAgenda" runat="server" class="p-4 border rounded bg-light mt-3">
                <h4 class="text-secondary mb-3"><i class="bi bi-clock-history"></i>Agendas y Especialidades del Médico</h4>

                <asp:GridView ID="dgvAgenda" runat="server" AutoGenerateColumns="false" DataKeyNames="IdAgendaMedico" OnRowCommand="dgvAgenda_RowCommand" CssClass="table table-hover align-middle border-0 small" GridLines="None">
                    <Columns>
                        <asp:BoundField HeaderText="Especialidad" DataField="Especialidad.Descripcion" />
                        <asp:BoundField HeaderText="Día" DataField="TurnoTrabajo.DiaDeTrabajo" />
                        <asp:BoundField HeaderText="Hora Entrada" DataField="TurnoTrabajo.HoraEntrada" />
                        <asp:BoundField HeaderText="Hora Salida" DataField="TurnoTrabajo.HoraSalida" />
                        
                        <asp:TemplateField HeaderText="Acciones" ItemStyle-Width="150px" ItemStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnEliminar" runat="server" CssClass="btn btn-sm btn-link text-danger text-decoration-none fw-bold p-0"
                                    CommandName="EliminarAgenda"
                                    CommandArgument='<%# Container.DataItemIndex %>'
                                    OnClientClick="return confirmarEliminar(this);">
                                    <i class="bi bi-trash"></i><br />Baja
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

            <div class="d-flex justify-content-between w-100 mt-3">
                <asp:LinkButton ID="btnAsignarHorario" runat="server" CssClass="btn btn-primary px-4 py-2" OnClick="btnAsignarHorario_Click">
                    <i class="bi bi-plus-lg"></i> Asignar Nuevo Horario
                </asp:LinkButton>
                <a href="MedicoPag.aspx" class="btn btn-outline-secondary px-4 py-2">Volver</a>
            </div>

        </div>
    </div>
</asp:Content>
