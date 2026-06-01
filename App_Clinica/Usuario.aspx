<%@ Page Title="" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="Usuario.aspx.cs" Inherits="App_Clinica.Usuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2> Administración de Usuarios</h2>

    <asp:GridView ID="dgvUsuario" runat="server">
    </asp:GridView>
</asp:Content>
