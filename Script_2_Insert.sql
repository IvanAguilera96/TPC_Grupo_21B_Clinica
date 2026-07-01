USE TPC_Clinica;
GO

-- 1. LIMPIEZA DE DATOS EXISTENTES
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

-- 2. INSERTS DE CONFIGURACIÓN DE PERFILES
SET IDENTITY_INSERT [dbo].[Perfil] ON;
INSERT INTO [dbo].[Perfil] ([IdPerfil], [Descripcion]) VALUES 
(1, N'Administrador'),
(2, N'Recepcionista'),
(3, N'Medico');
SET IDENTITY_INSERT [dbo].[Perfil] OFF;
GO

-- 3. INSERTS DE ESTADOS DE TURNO (Removido el estado 'Nuevo')
INSERT INTO [dbo].[EstadoTurno] (Descripcion) VALUES 
('Cancelado'),    -- Id 1
('Asignado'),     -- Id 2
('Reprogramado'), -- Id 3
('No Asistió'),   -- Id 4
('Cerrado');      -- Id 5
GO

-- 4. INSERTS DE ESPECIALIDADES
INSERT INTO [dbo].[Especialidad] (Descripcion, Estado) VALUES 
('Pediatría', 1),
('Clínica Médica', 1),
('Cardiología', 1),
('Traumatología', 1),
('Dermatología', 1);
GO

-- 5. INSERTS DE FRACCIONES HORARIAS (TurnoTrabajo)
INSERT INTO [dbo].[TurnoTrabajo] (DiaDeTrabajo, HoraEntrada, HoraSalida) VALUES 
('Lunes', '08:00', '12:00'),     -- Id 1
('Lunes', '13:00', '17:00'),     -- Id 2
('Martes', '08:00', '12:00'),    -- Id 3
('Martes', '13:00', '17:00'),    -- Id 4
('Miércoles', '08:00', '12:00'), -- Id 5
('Miércoles', '13:00', '17:00'), -- Id 6
('Jueves', '08:00', '12:00'),    -- Id 7
('Jueves', '13:00', '17:00'),    -- Id 8
('Viernes', '08:00', '12:00'),   -- Id 9
('Viernes', '13:00', '17:00');   -- Id 10
GO

-- 6. INSERTS DE MÉDICOS
INSERT INTO [dbo].[Medico] (Dni, Nombre, Apellido, Matricula, Estado) VALUES 
('32111222', 'Carlos', 'Gómez', 114234, 1),       -- Id 1
('34555666', 'Laura', 'Rodríguez', 125678, 1),    -- Id 2
('29888999', 'Andrés', 'Fernández', 98456, 1),    -- Id 3
('31444555', 'Elena', 'Benítez', 103987, 1),      -- Id 4
('35222333', 'Mariano', 'López', 131456, 1);      -- Id 5
GO

-- 7. INSERTS DE USUARIOS
INSERT INTO [dbo].[Usuario] (Nombre, Contrasenia, Estado, IdPerfil, IdMedico) VALUES 
('admin', 'admin123', 1, 1, NULL),
('recepcion1', 'recep2026', 1, 2, NULL),
('carlos.gomez', 'med123', 1, 3, 1), 
('laura.rod', 'med456', 1, 3, 2),
('andres.fer', 'med789', 1, 3, 3),
('elena.ben', 'med101', 1, 3, 4),
('mariano.lop', 'med202', 1, 3, 5);
GO

-- 8. INSERTS DE PACIENTES
INSERT INTO [dbo].[Paciente] (Apellido, Nombre, Dni, FechaNacimiento, Email, Telefono, Estado) VALUES 
('Pérez', 'Juan', '40111222', '1997-05-12', 'juan.perez@email.com', '1144445555', 1),
('Martínez', 'María', '42333444', '2000-09-24', 'maria.martinez@email.com', '1155556666', 1),
('González', 'Diego', '39888777', '1996-02-15', 'diego.gonzalez@email.com', '1122223333', 1),
('Álvarez', 'Sofía', '41555666', '1999-11-02', 'sofia.alvarez@email.com', '1166667777', 1),
('Romero', 'Lucas', '43222111', '2001-07-19', 'lucas.romero@email.com', '1133334444', 1),
('Blanco', 'Facundo', '45123456', '2004-03-11', 'facu.blanco@email.com', '1188889999', 1);
GO

-- 9. AGENDA DE MÉDICOS
INSERT INTO [dbo].[AgendaMedico] (IdMedico, IdEspecialidad, IdTurnoTrabajo) VALUES 
-- Dr. Carlos Gómez (Pediatría - Lunes Mañana y Martes Mañana)
(1, 1, 1), (1, 1, 3), 
-- Dra. Laura Rodríguez (Clínica Médica - Lunes Tarde y Martes Tarde)
(2, 2, 2), (2, 2, 4), 
-- Dr. Andrés Fernández (Pediatría - COINCIDE Lunes Mañana con Carlos Gómez, y Miércoles Mañana)
(3, 1, 1), (3, 1, 5), 
-- Dra. Elena Benítez (Cardiología - Miércoles Tarde y Jueves Tarde)
(4, 3, 6), (4, 3, 8), 
-- Dr. Mariano López (Traumatología - Viernes Mañana y Viernes Tarde)
(5, 4, 9), (5, 4, 10);
GO

-- 10. INSERTS DE TURNOS DE PRUEBA (IDs de Estado reajustados)
INSERT INTO [dbo].[Turno] (Fecha, Hora, IdAgendaMedico, IdPaciente, IdEstadoTurno, Observacion, Diagnostico) VALUES 
-- Turnos de HOY para el Dr. Carlos Gómez (IdAgendaMedico = 1)
(CAST(GETDATE() AS DATE), '08:30:00', 1, 1, 2, 'Control de niño sano', NULL), -- IdEstadoTurno: 2 (Asignado)
(CAST(GETDATE() AS DATE), '09:15:00', 1, 2, 2, 'Fiebre y dolor de garganta', NULL), -- IdEstadoTurno: 2 (Asignado)
(CAST(GETDATE() AS DATE), '10:00:00', 1, 3, 5, 'Revisión de estudios', 'Paciente evoluciona favorablemente. Alta médica.'), -- IdEstadoTurno: 5 (Cerrado)

-- Turnos de HOY para el Dr. Andrés Fernández (IdAgendaMedico = 5)
(CAST(GETDATE() AS DATE), '08:00:00', 5, 4, 2, 'Dolor estomacal persistente', NULL), -- IdEstadoTurno: 2 (Asignado)
(CAST(GETDATE() AS DATE), '11:00:00', 5, 5, 1, 'Control anual', NULL), -- IdEstadoTurno: 1 (Cancelado)

-- Turno histórico o futuro general
('2026-07-15', '14:00:00', 3, 6, 2, 'Consulta general de rutina', NULL); -- IdEstadoTurno: 2 (Asignado)
GO