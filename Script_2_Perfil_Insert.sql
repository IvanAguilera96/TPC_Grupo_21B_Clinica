SET IDENTITY_INSERT [dbo].[Perfil] ON;

INSERT INTO [dbo].[Perfil] ([IdPerfil], [Descripcion]) VALUES 
(1, N'Administrador'),
(2, N'Recepcionista'),
(3, N'Medico');

SET IDENTITY_INSERT [dbo].[Perfil] OFF;