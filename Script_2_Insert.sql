USE TPC_Clinica;
GO

DELETE FROM [dbo].[Turno];
DELETE FROM [dbo].[AgendaMedico];
DELETE FROM [dbo].[Usuario];
DELETE FROM [dbo].[Medico];
DELETE FROM [dbo].[Paciente];
DELETE FROM [dbo].[TurnoTrabajo];
DELETE FROM [dbo].[Especialidad];
DELETE FROM [dbo].[EstadoTurno];
DELETE FROM [dbo].[Perfil];
GO

--Perfil
SET IDENTITY_INSERT [dbo].[Perfil] ON;

INSERT INTO [dbo].[Perfil] ([IdPerfil], [Descripcion]) VALUES 
(1, N'Administrador'),
(2, N'Recepcionista'),
(3, N'Medico');

SET IDENTITY_INSERT [dbo].[Perfil] OFF;
GO

--Especialidad
INSERT INTO [dbo].Especialidad (Descripcion, Estado) VALUES 
('Pediatría', 1),
('Clínica Médica', 1),
('Cardiología', 1),
('Traumatología', 1),
('Ginecología', 1),
('Dermatología', 1),
('Oftalmología', 1),
('Neurología', 1),
('Psiquiatría', 1),
('Otorrinolaringología', 1),
('Endocrinología', 1),
('Gastroenterología', 1);
GO

--Médico
INSERT INTO [dbo].[Medico] (Dni, Nombre, Apellido, Matricula, Estado) VALUES 
('32111222', 'Carlos', 'Gómez', 114234, 1),
('34555666', 'Laura', 'Rodríguez', 125678, 1),
('29888999', 'Andrés', 'Fernández', 98456, 1),
('31444555', 'Elena', 'Benítez', 103987, 1),
('35222333', 'Mariano', 'López', 131456, 1);
GO

--Paciente
INSERT INTO [dbo].[Paciente] (Apellido, Nombre, Dni, FechaNacimiento, Email, Telefono, Estado) VALUES 
('Pérez', 'Juan', '40111222', '1997-05-12', 'juan.perez@email.com', '1144445555', 1),
('Martínez', 'María', '42333444', '2000-09-24', 'maria.martinez@email.com', '1155556666', 1),
('González', 'Diego', '39888777', '1996-02-15', 'diego.gonzalez@email.com', '1122223333', 1),
('Álvarez', 'Sofía', '41555666', '1999-11-02', 'sofia.alvarez@email.com', '1166667777', 1),
('Romero', 'Lucas', '43222111', '2001-07-19', 'lucas.romero@email.com', '1133334444', 1);
GO

--TurnoTrabajo
INSERT INTO [dbo].[TurnoTrabajo] (DiaDeTrabajo, HoraEntrada, HoraSalida) VALUES 
('Lunes', '08:00', '12:00'),
('Lunes', '13:00', '17:00'),
('Martes', '08:00', '12:00'),
('Martes', '13:00', '17:00'),
('Miércoles', '08:00', '12:00'),
('Miércoles', '13:00', '17:00'),
('Jueves', '08:00', '12:00'),
('Jueves', '13:00', '17:00'),
('Viernes', '08:00', '12:00'),
('Viernes', '13:00', '17:00');
GO

--Usuario
INSERT INTO [dbo].[Usuario] (Nombre, Contrasenia, Estado, IdPerfil) VALUES 
('admin', 'admin123', 1, (SELECT IdPerfil FROM Perfil WHERE Descripcion = 'Administrador')),
('recepcion1', 'recep2026', 1, (SELECT IdPerfil FROM Perfil WHERE Descripcion = 'Recepcionista')),
('carlos.gomez', 'med123', 1, (SELECT IdPerfil FROM Perfil WHERE Descripcion = 'Medico')),
('laura.rod', 'med456', 1, (SELECT IdPerfil FROM Perfil WHERE Descripcion = 'Medico'));
GO

--AgendaMedico
INSERT INTO [dbo].[AgendaMedico] (IdMedico, IdEspecialidad, IdTurnoTrabajo) VALUES 
((SELECT IdMedico FROM Medico WHERE Dni = '32111222'), 
 (SELECT IdEspecialidad FROM Especialidad WHERE Descripcion = 'Pediatría'), 
 (SELECT IdTurnoTrabajo FROM TurnoTrabajo WHERE DiaDeTrabajo = 'Lunes' AND HoraEntrada = '08:00:00')),

((SELECT IdMedico FROM Medico WHERE Dni = '32111222'), 
 (SELECT IdEspecialidad FROM Especialidad WHERE Descripcion = 'Pediatría'), 
 (SELECT IdTurnoTrabajo FROM TurnoTrabajo WHERE DiaDeTrabajo = 'Martes' AND HoraEntrada = '08:00:00')),

((SELECT IdMedico FROM Medico WHERE Dni = '34555666'), 
 (SELECT IdEspecialidad FROM Especialidad WHERE Descripcion = 'Clínica Médica'), 
 (SELECT IdTurnoTrabajo FROM TurnoTrabajo WHERE DiaDeTrabajo = 'Lunes' AND HoraEntrada = '13:00:00')),

((SELECT IdMedico FROM Medico WHERE Dni = '34555666'), 
 (SELECT IdEspecialidad FROM Especialidad WHERE Descripcion = 'Clínica Médica'), 
 (SELECT IdTurnoTrabajo FROM TurnoTrabajo WHERE DiaDeTrabajo = 'Martes' AND HoraEntrada = '13:00:00')),

((SELECT IdMedico FROM Medico WHERE Dni = '29888999'), 
 (SELECT IdEspecialidad FROM Especialidad WHERE Descripcion = 'Cardiología'), 
 (SELECT IdTurnoTrabajo FROM TurnoTrabajo WHERE DiaDeTrabajo = 'Miércoles' AND HoraEntrada = '08:00:00')),

((SELECT IdMedico FROM Medico WHERE Dni = '29888999'), 
 (SELECT IdEspecialidad FROM Especialidad WHERE Descripcion = 'Cardiología'), 
 (SELECT IdTurnoTrabajo FROM TurnoTrabajo WHERE DiaDeTrabajo = 'Jueves' AND HoraEntrada = '08:00:00')),

((SELECT IdMedico FROM Medico WHERE Dni = '31444555'), 
 (SELECT IdEspecialidad FROM Especialidad WHERE Descripcion = 'Traumatología'), 
 (SELECT IdTurnoTrabajo FROM TurnoTrabajo WHERE DiaDeTrabajo = 'Miércoles' AND HoraEntrada = '13:00:00')),

((SELECT IdMedico FROM Medico WHERE Dni = '31444555'), 
 (SELECT IdEspecialidad FROM Especialidad WHERE Descripcion = 'Traumatología'), 
 (SELECT IdTurnoTrabajo FROM TurnoTrabajo WHERE DiaDeTrabajo = 'Jueves' AND HoraEntrada = '13:00:00')),

((SELECT IdMedico FROM Medico WHERE Dni = '35222333'), 
 (SELECT IdEspecialidad FROM Especialidad WHERE Descripcion = 'Ginecología'), 
 (SELECT IdTurnoTrabajo FROM TurnoTrabajo WHERE DiaDeTrabajo = 'Viernes' AND HoraEntrada = '08:00:00')),

((SELECT IdMedico FROM Medico WHERE Dni = '35222333'), 
 (SELECT IdEspecialidad FROM Especialidad WHERE Descripcion = 'Ginecología'), 
 (SELECT IdTurnoTrabajo FROM TurnoTrabajo WHERE DiaDeTrabajo = 'Viernes' AND HoraEntrada = '13:00:00'));
GO

-- EstadoTurno
INSERT INTO EstadoTurno (Descripcion) VALUES 
('Cancelado'),
('Asignado'),
('Nuevo'),
('Reprogramado'),
('No Asistió'),
('Cerrado');