<%@ Page Title="Administración de Usuarios" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="Usuario.aspx.cs" Inherits="App_Clinica.Usuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row justify-content-center">

        <div class="col-md-6 d-flex flex-column align-items-center">

            <asp:Label ID="lblMensajeGrilla" runat="server" CssClass="alert alert-success d-block text-center mb-3" Visible="false"></asp:Label>

            <h3 class="mb-3"> Usuarios registrados </h3>
            <%-- AutogenerateColumns = False para poder cargar las columnas a mano y traer la descripción del perfil (objeto dentro de Usuario) --%>
            <asp:GridView ID="dgvUsuario" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false" DataKeyNames="IdUsuario" OnRowCommand="dgvUsuario_RowCommand">
                <Columns>
                    <asp:BoundField DataField="IdUsuario" HeaderText="ID" />
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="DescripcionPerfil" HeaderText="Perfil" />
                    <asp:CheckBoxField HeaderText ="Estado" DataField="Estado" />
                    <asp:TemplateField HeaderText="Acciones">
                        <ItemStyle HorizontalAlign="Center" Width="120px" />
                        <ItemTemplate>
                            <asp:LinkButton ID="btnEditar" runat="server" CssClass="btn btn-sm btn-outline-primary me-2" 
                                CommandName="Editar" 
                                CommandArgument='<%# Eval("IdUsuario") %>'>
                                <i class="bi bi-pencil-square"></i>
                            </asp:LinkButton>
            
                            <asp:LinkButton ID="btnEliminar" runat="server" CssClass="btn btn-sm btn-outline-danger" 
                                CommandName="Eliminar" 
                                CommandArgument='<%# Eval("IdUsuario") %>'
                                OnClientClick="return confirm('⚠️ ¿Está seguro que quiere eliminar el usuario seleccionado?');">
                                <i class="bi bi-trash"></i>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

            <div class="d-flex justify-content-center">
                <a href="UsuarioForm.aspx" class="btn btn-primary px-4 py-2 shadow-sm">
                    <i class="bi bi-plus-lg"></i> Agregar Nuevo Usuario
                </a>
            </div>

        </div>
    </div>
</asp:Content>
