<%@ Page Title="Usuarios" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="Usuario.aspx.cs" Inherits="App_Clinica.Usuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="d-flex justify-content-between align-items-center my-4">
        <h2>Administración de Usuarios</h2>
    </div>

    <div class="row">

        <div class="col-10 m-auto">

            <h3 class="mb-3"> Usuarios registrados </h3>

            <%-- AutogenerateColumns = False para poder cargar las columnas a mano y traer la descripción del perfil (objeto dentro de Usuario) --%>
            <asp:GridView ID="dgvUsuario" runat="server" CssClass="table table-striped table-bordered table-hover w-auto" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="IdUsuario" HeaderText="ID" />
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="DescripcionPerfil" HeaderText="Perfil" />
                
                    <%-- Agrega columna de acciones para Editar/Eliminar --%>
                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <asp:Button ID="btnEditar" runat="server" Text="Editar" CssClass="btn btn-sm btn-outline-primary me-2"/>
                            <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-sm btn-outline-danger"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
