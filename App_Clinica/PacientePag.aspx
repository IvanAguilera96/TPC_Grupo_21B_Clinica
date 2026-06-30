<%@ Page Title="Administración de Pacientes" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="PacientePag.aspx.cs" Inherits="App_Clinica.PacientePag" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

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
                            
                            <asp:TemplateField HeaderText="Acciones" ItemStyle-Width="150px" ItemStyle-CssClass="text-end">
                                <ItemTemplate>
                                    <div class="d-inline-flex justify-content-end w-100">
                                        <asp:LinkButton ID="btnEditar" runat="server" CssClass="btn btn-sm btn-link text-primary text-decoration-none fw-bold p-0 me-3"
                                            CommandName="Editar"
                                            CommandArgument='<%# Eval("IdPaciente")%>'>
                                            <i class="bi bi-pencil-square me-1"></i>Editar
                                        </asp:LinkButton>

                                        <asp:LinkButton ID="btnEliminar" runat="server" CssClass="btn btn-sm btn-link text-danger text-decoration-none fw-bold p-0"
                                            CommandName="Eliminar"
                                            CommandArgument='<%# Eval("IdPaciente")%>'
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
</asp:Content>