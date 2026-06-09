<%@ Page Title="" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="PacientePag.aspx.cs" Inherits="App_Clinica.PacientePag" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row justify-content-center">
        <div class="col-md-6 d-flex flex-column align-items-center">

            <asp:Label ID="lblMensajeGrilla" runat="server" CssClass="alert alert-success d-block text-center mb-3" Visible="false"></asp:Label>

            <h3 class="mb-3">Pacientes registrados</h3>
            <asp:GridView ID="dgvPaciente" DataKeyNames="IdPaciente" CssClass="table table-striped table-bordered table-hover" OnRowCommand="dgvPaciente_RowCommand" runat="server" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField HeaderText="DNI" DataField="DNI" />
                    <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
                    <asp:BoundField HeaderText="Apellido" DataField="Apellido" />
                    <asp:BoundField HeaderText="Email" DataField="Email" />
                    <asp:BoundField HeaderText="Telefono" DataField="Telefono" />
                    <asp:CheckBoxField HeaderText="Estado" DataField="Estado" />
                    
                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnEditar" runat="server" CssClass="btn btn-sm btn-outline-primary me-2"
                                CommandName="Editar"
                                CommandArgument='<%# Eval("IdPaciente")%>'>
                                <i class="bi bi-pencil-square"></i>
                            </asp:LinkButton>

                            <asp:LinkButton ID="btnEliminar" runat="server" CssClass="btn btn-sm btn-outline-danger"
                                CommandName="Eliminar"
                                CommandArgument='<%# Eval("IdPaciente")%>'
                                OnClientClick="return confirm('⚠️ ¿Está seguro que quiere eliminar el usuario seleccionado?');">
                                <i class="bi bi-trash"></i>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <div>
                <a href="PacienteForm.aspx" class="btn btn-primary py-2 fw-bold">
                    <i></i>Agregar Nuevo Paciente
                </a>
            </div>
        </div>
    </div>


</asp:Content>
