<%@ Page Title="" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="Usuario.aspx.cs" Inherits="App_Clinica.Usuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2> Administración de Usuarios</h2>

    <div class="row">

        <div class="col-md-8">
            <h2> Usuarios registrados </h2>
            <asp:GridView ID="dgvUsuario" runat="server">
            </asp:GridView>
        </div>

        <div class="col-md-4">
            <h2>Alta de nuevo usuario</h2>

            <div>
                <label>Nombre de usuario:</label>
                <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
            </div>
            <br />

            <div>
                <label>Contraseña:</label>
                <asp:TextBox ID="txtContrasenia" runat="server" TextMode="Password"></asp:TextBox>
            </div>
            <br />

            <div>
                <label>Perfil:</label>
                <asp:DropDownList ID="ddlPerfil" runat="server">
                    <asp:ListItem Text="Administrador" Value="1" />
                    <asp:ListItem Text="Recepcionista" Value="2" />
                    <asp:ListItem Text="Médico" Value="3" />
                </asp:DropDownList>
            </div>
            <br />

            <asp:Button ID="btnGuardar" runat="server" Text="Aceptar" OnClick="btnGuardar_Click"/>
            <br />

            <asp:Label ID="lblMensaje" runat="server"></asp:Label>
        </div>

    </div>
    
</asp:Content>
