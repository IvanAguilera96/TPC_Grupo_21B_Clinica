<%@ Page Title="Usuarios" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="Usuario.aspx.cs" Inherits="App_Clinica.Usuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2> Administración de Usuarios</h2>

    <div class="row">

        <div class="col-md-8">
            <h2> Usuarios registrados </h2>
            <%-- AutogenerateColumns = False para poder cargar las columnas a mano y traer la descripción del perfil (objeto dentro de Usuario) --%>
            <asp:GridView ID="dgvUsuario" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="IdUsuario" HeaderText="ID" />
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="DescripcionPerfil" HeaderText="Perfil" />
                </Columns>
            </asp:GridView>
        </div>

        <div class="col-md-4">
            <h2>Alta de nuevo usuario</h2>

            <div>
                <label>Usuario:</label>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" ></asp:TextBox>
            </div>
            <br />

            <div>
                <label>Contraseña:</label>
                <asp:TextBox ID="txtContrasenia" runat="server" TextMode="Password" CssClass="form-control" ></asp:TextBox> <%-- TextMode Password oculta caracteres al escribir --%>
            </div>
            <br />

            <div>
                <label>Perfil:</label>
                <asp:DropDownList ID="ddlPerfil" runat="server" CssClass="form-select" >
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
