<%@ Page Title="Administración de Pacientes" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="PacientePag.aspx.cs" Inherits="App_Clinica.PacientePag" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <asp:UpdatePanel ID="UpdatePanelPacientes" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

            <asp:HiddenField ID="hfPacienteSeleccionadoId" runat="server" />
            <div class="row justify-content-center">
                <div class="col-12 col-md-11 col-lg-10">

                    <asp:Label ID="lblMensajeGrilla" runat="server" 
                        CssClass="alert alert-success d-block text-center mb-4 border-0 shadow-sm fw-medium" 
                        style="background-color: #E6F5EC; color: #1E4620; border-radius: 8px;" 
                        Visible="false">
                    </asp:Label>

                    <div class="d-flex justify-content-between align-items-center mb-3">
                        <h4 class="text-dark fw-bold m-0">
                            <i class="bi bi-people-fill text-primary me-2"></i>Pacientes Registrados
                        </h4>
                        <a href="PacienteForm.aspx" class="btn border-0 fw-bold px-4 py-2 shadow-sm rounded-3 btn-sm" style="background-color: #D0E4F7; color: #1C3345;">
                            <i class="bi bi-person-plus me-1"></i> Agregar Nuevo Paciente
                        </a>
                    </div>

                    <div class="row g-3 mb-3 align-items-end">
                        <div class="col-12 col-md-3">
                            <label class="form-label fw-semibold text-secondary mb-1" style="font-size: 0.85rem;">Buscar por DNI:</label>
                            <asp:TextBox ID="txtFiltroDni" runat="server" CssClass="form-control" 
                                AutoPostBack="true" OnTextChanged="txtFiltro_TextChanged" placeholder="Ej: 12345678"
                                onkeypress="return event.charCode >= 48 && event.charCode <= 57;"></asp:TextBox>
                        </div>

                        <div class="col-12 col-md-4">
                            <label class="form-label fw-semibold text-secondary mb-1" style="font-size: 0.85rem;">Buscar por Nombre o Apellido:</label>
                            <asp:TextBox ID="txtFiltroNombre" runat="server" CssClass="form-control" 
                                AutoPostBack="true" OnTextChanged="txtFiltro_TextChanged" placeholder="Escriba un nombre o apellido..."></asp:TextBox>
                        </div>
            
                        <div class="col-12 col-md-2">
                            <asp:Button ID="btnBuscar" runat="server" Text="🔍 Buscar" 
                                CssClass="btn border-0 w-100 fw-bold py-2 shadow-sm" 
                                style="background-color: #F0F4F8; color: #4A5568;" OnClick="btnBuscar_Click" />
                        </div>

                        <div class="col-12 col-md-3">
                            <asp:Button ID="btnLimpiarFiltros" runat="server" Text="Limpiar Filtros 🔄" 
                                CssClass="btn border-0 w-100 fw-bold py-2 shadow-sm" OnClick="btnLimpiarFiltros_Click"
                                style="background-color: #F0F4F8; color: #4A5568;"/>
                        </div>
                    </div>

                    <div class="card bg-white p-4 rounded-3 shadow-sm border-0 mb-4">
                        <div class="table-responsive" style="max-height: 500px; overflow-y: auto;">
                            <asp:GridView ID="dgvPaciente" runat="server" DataKeyNames="IdPaciente" 
                                CssClass="table table-hover align-middle border-0 small" GridLines="None"
                                OnRowCommand="dgvPaciente_RowCommand" AutoGenerateColumns="false" 
                                AllowPaging="true" PageSize="10" OnPageIndexChanging="dgvPaciente_PageIndexChanging">
                                
                                <PagerStyle CssClass="pagination justify-content-center border-0 pt-3" />
                                
                                <Columns>
                                    <asp:BoundField HeaderText="DNI" DataField="DNI" ItemStyle-CssClass="text-muted fw-semibold" ItemStyle-Width="100px" />
                                    <asp:BoundField HeaderText="Nombre" DataField="Nombre" ItemStyle-CssClass="fw-bold text-dark" />
                                    <asp:BoundField HeaderText="Apellido" DataField="Apellido" ItemStyle-CssClass="fw-bold text-dark" />
                                    <asp:BoundField HeaderText="F. Nacimiento" DataField="FechaNacimiento" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" ItemStyle-Width="110px" />
                                    <asp:BoundField HeaderText="Email" DataField="Email" />
                                    <asp:BoundField HeaderText="Teléfono" DataField="Telefono" />
                                    
                                    <asp:TemplateField HeaderText="Estado" ItemStyle-Width="100px">
                                        <ItemTemplate>
                                            <span class="badge <%# Convert.ToBoolean(Eval("Estado")) ? "bg-success bg-opacity-10 text-success" : "bg-danger bg-opacity-10 text-danger" %> px-2.5 py-1.5 rounded-2 fw-semibold">
                                                <%# Convert.ToBoolean(Eval("Estado")) ? "Activo" : "Inactivo" %>
                                            </span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Acciones" ItemStyle-Width="220px" ItemStyle-CssClass="text-end">
                                        <ItemTemplate>
                                            <div class="d-inline-flex justify-content-end w-100 gap-2">
                                                <asp:LinkButton ID="btnHistorial" runat="server" CssClass="btn btn-sm btn-link text-info text-decoration-none fw-bold p-0 me-2"
                                                    CommandName="Historial" CommandArgument='<%# Eval("IdPaciente")%>'>
                                                    <i class="bi bi-clock-history me-1"></i>Historial
                                                </asp:LinkButton>

                                                <asp:LinkButton ID="btnEditar" runat="server" CssClass="btn btn-sm btn-link text-primary text-decoration-none fw-bold p-0 me-2"
                                                    CommandName="Editar" CommandArgument='<%# Eval("IdPaciente")%>'>
                                                    <i class="bi bi-pencil-square me-1"></i>Editar
                                                </asp:LinkButton>

                                                <asp:LinkButton ID="btnEliminar" runat="server" CssClass="btn btn-sm btn-link text-danger text-decoration-none fw-bold p-0"
                                                    CommandName="Eliminar" CommandArgument='<%# Eval("IdPaciente")%>'
                                                    OnClientClick="return confirmarEliminar(this);">
                                                    <i class="bi bi-trash me-1"></i>Baja
                                                </asp:LinkButton>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>

                </div>
            </div>

            <div class="modal fade" id="modalHistorialPaciente" tabindex="-1" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered modal-md">
                    <div class="modal-content border-0 shadow" style="background-color: #f0f8ff;">
                        <div class="modal-header border-0" style="background-color: #d0e7ff; color: #2c3e50;">
                            <h5 class="modal-title fw-bold"><i class="bi bi-clock-history me-2" style="color: #4a90e2;"></i>Últimos 5 Turnos</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body p-4 text-dark">
                            
                            <asp:GridView ID="dgvHistorialTurnos" runat="server" CssClass="table table-hover align-middle border-0 small bg-white rounded-3 shadow-sm mb-0" 
                                AutoGenerateColumns="false" GridLines="None">
                                <Columns>
                                    <asp:BoundField HeaderText="Fecha" DataField="Fecha" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-Width="110px" />
                                    <asp:BoundField HeaderText="Hora" DataField="Hora" DataFormatString="{0:hh\:mm}" ItemStyle-Width="80px" />
        
                                    <asp:BoundField HeaderText="Especialidad" DataField="Agenda.Especialidad.Descripcion" />
        
                                    <asp:BoundField HeaderText="Médico" DataField="Agenda.Medico.Nombre" />
        
                                    <asp:TemplateField HeaderText="Estado" ItemStyle-Width="130px">
                                        <ItemTemplate>
                                            <span class="badge <%# Eval("Estado.Descripcion").ToString() == "Asignado" ? "bg-warning text-dark" : 
                                                                Eval("Estado.Descripcion").ToString() == "Cerrado" ? "bg-success bg-opacity-10 text-success" :
                                                                Eval("Estado.Descripcion").ToString() == "Reprogramado" ? "bg-info bg-opacity-10 text-info" :
                                                                Eval("Estado.Descripcion").ToString() == "No Asistió" ? "bg-secondary bg-opacity-20 text-dark" : "bg-danger bg-opacity-10 text-danger" %> px-2.5 py-1.5 rounded-2 fw-semibold">
                                                <%# Eval("Estado.Descripcion") %>
                                            </span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            
                            <asp:Label ID="lblHistorialVacio" runat="server" CssClass="text-muted fst-italic d-block text-center p-4 fs-6" Text="El paciente no registra turnos previos en el sistema." Visible="false"></asp:Label>

                        </div>
                        <div class="modal-footer border-0 p-3" style="background-color: #e6f2ff;">
                            <asp:LinkButton ID="btnNuevoTurnoDesdeModal" runat="server" CssClass="btn btn-primary fw-semibold rounded-3 px-3 me-auto" OnClick="btnNuevoTurnoDesdeModal_Click">
                                <i class="bi bi-calendar-plus me-1"></i> Agendar Turno
                            </asp:LinkButton>
    
                            <button type="button" class="btn btn-light border px-4 fw-semibold rounded-3" data-bs-dismiss="modal">Cerrar</button>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript">
        // Evita el bug de la pantalla gris congelada con los cierres del UpdatePanel
        if (typeof Sys !== 'undefined') {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function (sender, args) {
                if (!document.querySelector('.modal.show')) {
                    document.querySelectorAll('.modal-backdrop').forEach(el => el.remove());
                    document.body.classList.remove('modal-open');
                    document.body.style.removeProperty('overflow');
                    document.body.style.removeProperty('padding-right');
                }
            });
        }

        function abrirModalHistorial() {
            var modal = new bootstrap.Modal(document.getElementById('modalHistorialPaciente'));
            modal.show();
        }
    </script>
</asp:Content>