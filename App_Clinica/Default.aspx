<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="App_Clinica.Default1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="container-fluid mt-4" style="font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;">
        
        <div class="row mb-4">
            <div class="col">
                <div class="p-4 rounded-3 shadow-sm bg-white border d-flex align-items-center justify-content-between">
                    <div>
                        <h2 class="fw-bold text-dark m-0">Panel de Control</h2>
                        <p class="text-muted m-0 mt-1">Hola, <asp:Label ID="lblNombreUsuario" runat="server" Font-Bold="true" class="text-primary"></asp:Label>. Bienvenido al sistema de gestión de la clínica.</p>
                    </div>
                    <div class="text-muted small text-end d-none d-md-block fw-semibold">
                        <i class="bi bi-calendar3 me-1 text-primary"></i> <%= DateTime.Now.ToString("dd 'de' MMMM, yyyy") %>
                    </div>
                </div>
            </div>
        </div>

        <%-- PANEL PARA EL ADMINISTRADOR --%>
        <asp:Panel ID="pnlAdmin" runat="server" Visible="false">
            <div class="row g-4 mb-4">
                
                <div class="col-12 col-md-4">
                    <div class="card border-0 shadow-sm p-4 rounded-3 position-relative" style="background-color: #D0E4F7; color: #1C3345;">
                        <div class="d-flex justify-content-between align-items-center">
                            <div>
                                <h6 class="fw-bold text-uppercase small m-0" style="letter-spacing: 0.5px; opacity: 0.85;">Médicos Registrados</h6>
                                <h2 class="fw-bold mb-0 mt-2">
                                    <asp:Label ID="lblCantMedicos" runat="server" Text="0"></asp:Label>
                                </h2>
                            </div>
                            <i class="bi bi-heart-pulse-fill fs-1 opacity-25"></i>
                        </div>
                        <a href="MedicoPag.aspx" class="stretched-link small text-decoration-none mt-3 d-inline-block fw-bold" style="color: #1C3345;">Ver listado completo →</a>
                    </div>
                </div>

                <div class="col-12 col-md-4">
                    <div class="card border-0 shadow-sm p-4 rounded-3 position-relative" style="background-color: #D0E4F7; color: #1C3345;">
                        <div class="d-flex justify-content-between align-items-center">
                            <div>
                                <h6 class="fw-bold text-uppercase small m-0" style="letter-spacing: 0.5px; opacity: 0.85;">Pacientes Totales</h6>
                                <h2 class="fw-bold mb-0 mt-2">
                                    <asp:Label ID="lblCantPacientes" runat="server" Text="0"></asp:Label>
                                </h2>
                            </div>
                            <i class="bi bi-people-fill fs-1 opacity-25"></i>
                        </div>
                        <a href="PacientePag.aspx" class="stretched-link small text-decoration-none mt-3 d-inline-block fw-bold" style="color: #1C3345;">Administrar historias →</a>
                    </div>
                </div>

                <div class="col-12 col-md-4">
                    <div class="card border-0 shadow-sm p-4 rounded-3 position-relative" style="background-color: #D0E4F7; color: #1C3345;">
                        <div class="d-flex justify-content-between align-items-center">
                            <div>
                                <h6 class="fw-bold text-uppercase small m-0" style="letter-spacing: 0.5px; opacity: 0.85;">Turnos del Mes</h6>
                                <h2 class="fw-bold mb-0 mt-2">
                                    <asp:Label ID="lblCantTurnosMes" runat="server" Text="0"></asp:Label>
                                </h2>
                            </div>
                            <i class="bi bi-calendar-check-fill fs-1 opacity-25"></i>
                        </div>
                        <a href="TurnosPag.aspx" class="stretched-link small text-decoration-none mt-3 d-inline-block fw-bold" style="color: #1C3345;">Auditar agenda global →</a>
                    </div>
                </div>

            </div>

            <div class="row g-4">
                
                <div class="col-12 col-lg-8">
                    <div class="card bg-white p-4 rounded-3 shadow-sm border-0 h-100">
                        <h5 class="text-dark fw-bold mb-3">
                            <i class="bi bi-shield-check text-primary me-2"></i>Últimos Turnos Registrados
                        </h5>
                        <div class="table-responsive">
                            <asp:GridView ID="dgvAuditoriaTurnos" runat="server" CssClass="table table-hover align-middle border-0 small" 
                                AutoGenerateColumns="false" GridLines="None">
                                <Columns>
                                    <asp:BoundField HeaderText="Fecha" DataField="Fecha" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-Width="100px" />
                                    <asp:BoundField HeaderText="Hora" DataField="Hora" DataFormatString="{0:hh\:mm}" ItemStyle-CssClass="fw-semibold" />
                                    <asp:BoundField HeaderText="Paciente" DataField="Paciente.NombreCompleto" />
                                    <asp:BoundField HeaderText="MédicoAsignado" DataField="Agenda.Medico.NombreCompleto" />
                                    <asp:TemplateField HeaderText="Estado">
                                        <ItemTemplate>
                                            <span class="badge <%# Eval("Estado.Descripcion").ToString() == "Asignado" ? "bg-warning text-dark" : "bg-danger" %>">
                                                <%# Eval("Estado.Descripcion") %>
                                            </span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>

                <div class="col-12 col-lg-4">
                    <div class="card bg-white p-4 rounded-3 shadow-sm border-0 h-100">
                        <h5 class="text-dark fw-bold mb-3">
                            <i class="bi bi-gear-fill text-secondary me-2"></i>Configuración Clínica
                        </h5>
                        
                        <div class="d-grid gap-2 mt-2">
                            <a href="EspecialidadPag.aspx" class="btn border-0 fw-bold py-2 text-start ps-3 rounded-3 small" style="background-color: #E2EEF9; color: #2C5282;">
                                <i class="bi bi-tags me-2"></i> Gestionar Especialidades
                            </a>
                            <a href="Usuario.aspx" class="btn border-0 fw-bold py-2 text-start ps-3 rounded-3 small" style="background-color: #E2EEF9; color: #2C5282;">
                                <i class="bi bi-person-lock me-2"></i> Control de Usuarios Internos
                            </a>
                        </div>
                    </div>
                </div>

            </div>
        </asp:Panel>

        <%-- PANEL PARA LA RECEPCIONISTA --%>
        <asp:Panel ID="pnlRecepcion" runat="server" Visible="false">
    <div class="row g-4">
        <!-- Columna de Acciones Rápidas -->
        <div class="col-12 col-md-4">
            <div class="card bg-white p-4 rounded-3 shadow-sm border-0 h-100">
                <h5 class="text-dark fw-bold mb-3"><i class="bi bi-lightning-charge-fill text-warning me-1"></i>Acciones Rápidas</h5>
                <div class="d-grid gap-3">
                    <a href="PacienteForm.aspx" class="btn border-0 fw-bold py-3 text-start ps-3 rounded-3" style="background-color: #E6F0FA; color: #1C3345;">
                        <i class="bi bi-person-plus me-2"></i> Nuevo Paciente
                    </a>
                    <a href="TurnosPag.aspx" class="btn border-0 fw-bold py-3 text-start ps-3 rounded-3" style="background-color: #BEE3F8; color: #2C5282;">
                        <i class="bi bi-calendar-plus me-2"></i> Asignar Turno Nuevo
                    </a>
                </div>
            </div>
        </div>
        
        <!-- Columna del Monitor de Turnos del Día -->
        <div class="col-12 col-md-8">
            <div class="card bg-white p-4 rounded-3 shadow-sm border-0 h-100">
                <h5 class="text-dark fw-bold mb-3"><i class="bi bi-clock text-primary me-1"></i>Turnos Programados para Hoy</h5>
                <div class="table-responsive">
                    <asp:GridView ID="dgvProximosTurnos" runat="server" CssClass="table table-hover align-middle border-0 small" 
                        AutoGenerateColumns="false" GridLines="None">
                        <Columns>
                            <%-- Formateamos el TimeSpan de la hora a un formato amigable hh:mm --%>
                            <asp:BoundField HeaderText="Hora" DataField="Hora" DataFormatString="{0:hh\:mm}" ItemStyle-CssClass="fw-bold text-secondary" />
                            
                            <%-- Navegamos las propiedades del objeto Turno tal como lo hiciste en la grilla principal --%>
                            <asp:BoundField HeaderText="Paciente" DataField="Paciente.NombreCompleto" />
                            <asp:BoundField HeaderText="Médico" DataField="Agenda.Medico.NombreCompleto" />
                            <asp:BoundField HeaderText="Especialidad" DataField="Agenda.Especialidad.Descripcion" />
                            
                            <%-- Badge dinámico para identificar el estado visualmente --%>
                            <asp:TemplateField HeaderText="Estado">
                                <ItemTemplate>
                                    <span class="badge <%# Eval("Estado.Descripcion").ToString() == "Asignado" ? "bg-warning text-dark" : "bg-danger" %>">
                                        <%# Eval("Estado.Descripcion") %>
                                    </span>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Panel>

        <%-- PANEL PARA EL MÉDICO --%>
        <asp:Panel ID="pnlMedico" runat="server" Visible="false">
            <div class="card bg-white p-4 rounded-3 shadow-sm border-0">
                <div class="d-flex justify-content-between align-items-center mb-3">
                    <h4 class="text-dark fw-bold m-0">
                        <i class="bi bi-calendar-day text-success me-2"></i>Mi Agenda para Hoy
                    </h4>
                    <span class="badge bg-success bg-opacity-10 text-success px-3 py-2 rounded-pill fw-semibold">
                        Pacientes en Espera
                    </span>
                </div>
                
                <div class="table-responsive">
                    <asp:GridView ID="dgvTurnosDelDia" runat="server" CssClass="table table-hover align-middle border-0 small" 
                        AutoGenerateColumns="false" GridLines="None">
                        <Columns>
                            <%-- Hora del Turno destacada --%>
                            <asp:BoundField HeaderText="Hora" DataField="Hora" DataFormatString="{0:hh\:mm}" ItemStyle-CssClass="fw-bold text-success" ItemStyle-Width="100px" />
                            
                            <%-- Datos esenciales del paciente --%>
                            <asp:BoundField HeaderText="Paciente" DataField="Paciente.NombreCompleto" />
                            
                            <%-- Por si el médico atiende por más de una especialidad (Ej: Clínica y Guardia) --%>
                            <asp:BoundField HeaderText="Especialidad" DataField="Agenda.Especialidad.Descripcion" />
                            
                            <%-- Estado visual del turno --%>
                            <asp:TemplateField HeaderText="Estado" ItemStyle-Width="120px">
                                <ItemTemplate>
                                    <span class="badge <%# Eval("Estado.Descripcion").ToString() == "Asignado" ? "bg-warning text-dark" : "bg-danger" %>">
                                        <%# Eval("Estado.Descripcion") %>
                                    </span>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </asp:Panel>

    </div>
</asp:Content>