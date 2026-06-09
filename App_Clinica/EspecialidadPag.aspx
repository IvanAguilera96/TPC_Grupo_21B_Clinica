<%@ Page Title="" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="EspecialidadPag.aspx.cs" Inherits="App_Clinica.EspecialidadPag" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row justify-content-center">
        <div class="col-md-6 d-flex flex-column align-items-center">

            <asp:Label ID="lblMensajeGrilla" runat="server" CssClass="alert alert-success d-block text-center mb-3" Visible="false"></asp:Label>

            <h3 class="mb-3">Especialidades Médicas</h3>

            <asp:GridView ID="dgvEspecialidades" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false" DataKeyNames="IdEspecialidad" OnRowCommand="dgvEspecialidades_RowCommand">
                <Columns>
                    <asp:BoundField DataField="IdEspecialidad" HeaderText="ID" ItemStyle-Width="70px" />
                    <asp:BoundField DataField="Descripcion" HeaderText="Especialidad" />
                    <asp:CheckBoxField HeaderText ="Estado" DataField="Estado" />
                    <asp:TemplateField HeaderText="Acciones">
                        <ItemStyle HorizontalAlign="Center" Width="120px" />
                        <ItemTemplate>
                            <asp:LinkButton ID="btnEditar" runat="server" CssClass="btn btn-sm btn-outline-primary me-2" CommandName="Editar" CommandArgument='<%# Eval("IdEspecialidad") %>'>
                                <i class="bi bi-pencil-square"></i>
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnEliminar" runat="server" CssClass="btn btn-sm btn-outline-danger" CommandName="Eliminar" CommandArgument='<%# Eval("IdEspecialidad") %>' OnClientClick="return confirm('⚠️ ¿Seguro que quiere dar de baja esta especialidad?');">
                                <i class="bi bi-trash"></i>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
           
            <div class="d-flex justify-content-center">
                <a href="EspecialidadForm.aspx" class="btn btn-primary px-4 py-2 shadow-sm">
                <i class="bi bi-plus-lg"></i> Agregar Especialidad
                </a>
            </div>

        </div>
    </div>
</asp:Content>
