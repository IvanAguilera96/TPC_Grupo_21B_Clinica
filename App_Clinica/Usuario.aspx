<%@ Page Title="Administración de Usuarios" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="Usuario.aspx.cs" Inherits="App_Clinica.Usuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row justify-content-center">
        <div class="col-12 col-md-10 col-lg-8">

            <asp:Label ID="lblMensajeGrilla" runat="server" 
                CssClass="alert alert-success d-block text-center mb-4 border-0 shadow-sm fw-medium" 
                style="background-color: #E6F5EC; color: #1E4620; border-radius: 8px;" 
                Visible="false">
            </asp:Label>

            <div class="d-flex justify-content-between align-items-center mb-3">
                <h4 class="text-dark fw-bold m-0">
                    <i class="bi bi-people-fill text-primary me-2"></i>Usuarios Registrados
                </h4>
                <a href="UsuarioForm.aspx" class="btn border-0 fw-bold px-4 py-2 shadow-sm rounded-3 btn-sm" style="background-color: #D0E4F7; color: #1C3345;">
                    <i class="bi bi-plus-lg me-1"></i> Agregar Nuevo
                </a>
            </div>

            <div class="card bg-white p-4 rounded-3 shadow-sm border-0 mb-4">
                <div class="table-responsive" style="max-height: 500px; overflow-y: auto;">
                    <asp:GridView ID="dgvUsuario" runat="server" CssClass="table table-hover align-middle border-0 small" 
                        AutoGenerateColumns="false" GridLines="None" DataKeyNames="IdUsuario" OnRowCommand="dgvUsuario_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="IdUsuario" HeaderText="ID" ItemStyle-CssClass="text-muted fw-semibold" ItemStyle-Width="60px" />
                            
                            <asp:BoundField DataField="Nombre" HeaderText="Nombre de Usuario" ItemStyle-CssClass="fw-bold text-dark" />
                            
                            <asp:BoundField DataField="DescripcionPerfil" HeaderText="Perfil Asignado" />
                            
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
                                            CommandArgument='<%# Eval("IdUsuario") %>'>
                                            <i class="bi bi-pencil-square me-1"></i>Editar
                                        </asp:LinkButton>
                        
                                        <asp:LinkButton ID="btnEliminar" runat="server" CssClass="btn btn-sm btn-link text-danger text-decoration-none fw-bold p-0" 
                                            CommandName="Eliminar" 
                                            CommandArgument='<%# Eval("IdUsuario") %>'
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