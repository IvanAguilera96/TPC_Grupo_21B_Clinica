<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="App_Clinica.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Ingreso al Sistema - TPC Clínica</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.10.0/font/bootstrap-icons.css" rel="stylesheet" />
</head>
<body style="background-color: #E6F0FA; font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;">
    <form id="form1" runat="server" class="vh-100 d-flex align-items-center justify-content-center">
        
        <div class="card border-0 shadow-sm" style="background-color: #FAF8F5; border-radius: 20px; width: 100%; max-width: 400px; overflow: hidden;">
            
            <div class="text-center py-4" style="background-color: #D0E4F7; color: #1C3345;">
                <i class="bi bi-heart-pulse-fill fs-1" style="color: #4A90E2;"></i>
                <h4 class="fw-bold mt-2 mb-0">TPC Clínica</h4>
                <small style="color: #4A5568;">Gestión Hospitalaria</small>
            </div>

            <div class="card-body p-4">
                
                <asp:Label ID="lblMensajeError" runat="server" 
                    CssClass="alert alert-danger d-block text-center mb-3 fw-medium border-0" 
                    style="background-color: #E2EEF9; color: #2C5282; font-size: 0.9rem; border-radius: 8px;" 
                    Visible="false">
                </asp:Label>

                <div class="mb-3">
                    <label class="form-label fw-semibold text-secondary" style="font-size: 0.85rem;">Usuario</label>
                    <div class="input-group">
                        <span class="input-group-text bg-white border-end-0 text-muted" style="border-radius: 8px 0 0 8px;"><i class="bi bi-person"></i></span>
                        <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control border-start-0" style="border-radius: 0 8px 8px 0; font-size: 0.95rem;"></asp:TextBox>
                    </div>
                </div>

                <div class="mb-4">
                    <label class="form-label fw-semibold text-secondary" style="font-size: 0.85rem;">Contraseña</label>
                    <div class="input-group">
                        <span class="input-group-text bg-white border-end-0 text-muted" style="border-radius: 8px 0 0 8px;"><i class="bi bi-lock"></i></span>
                        <asp:TextBox ID="txtContrasenia" runat="server" TextMode="Password" CssClass="form-control border-start-0" style="border-radius: 0 8px 8px 0; font-size: 0.95rem;"></asp:TextBox>
                    </div>
                </div>

                <asp:Button ID="btnIngresar" runat="server" Text="Ingresar al Sistema" OnClick="btnIngresar_Click"
                    CssClass="btn border-0 w-100 fw-bold rounded-3 py-2" 
                    style="background-color: #BEE3F8; color: #2C5282; font-size: 0.95rem; transition: all 0.2s ease-in-out;"
                    onmouseover="this.style.backgroundColor='#90CDF4'; this.style.color='#2A4365';"
                    onmouseout="this.style.backgroundColor='#BEE3F8'; this.style.color='#2C5282';" />
            </div>

            <div class="text-center pb-3">
                <small class="text-muted" style="font-size: 0.75rem;">© 2026 - Control de Acceso Interno</small>
            </div>
        </div>

    </form>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bundle.min.js"></script>
</body>
</html>