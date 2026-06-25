CREATE DATABASE TPC_Clinica;
GO
USE TPC_Clinica;
GO

CREATE TABLE Medico(
	IdMedico INT PRIMARY KEY IDENTITY(1,1),
	Dni VARCHAR(8) NOT NULL,
	Nombre VARCHAR(50) NOT NULL,
	Apellido VARCHAR(50) NOT NULL,
	Matricula INT UNIQUE NOT NULL,
	Estado BIT NOT NULL DEFAULT 1
);

CREATE TABLE Paciente(
	IdPaciente INT PRIMARY KEY IDENTITY(1,1),
	Dni VARCHAR(8) NOT NULL,
	Nombre VARCHAR(50) NOT NULL,
	Apellido VARCHAR(50) NOT NULL,
	FechaNacimiento DATE NOT NULL,
	Email VARCHAR(50) NOT NULL,
	Telefono VARCHAR(20) NULL,
	Estado BIT NOT NULL DEFAULT 1
);

CREATE TABLE Especialidad(
	IdEspecialidad INT PRIMARY KEY IDENTITY(1,1),
	Descripcion VARCHAR(100) NOT NULL,
	Estado BIT NOT NULL DEFAULT 1
);

CREATE TABLE TurnoTrabajo(
	IdTurnoTrabajo INT PRIMARY KEY IDENTITY(1,1),
	HoraEntrada Time NOT NULL,
	HoraSalida Time NOT NULL,
	DiaDeTrabajo VARCHAR(10) NOT NULL,
);

CREATE TABLE EstadoTurno(
	IdEstadoTurno INT PRIMARY KEY IDENTITY(1,1),
	Descripcion VARCHAR(100) NOT NULL
);

CREATE TABLE AgendaMedico(
	IdAgendaMedico INT PRIMARY KEY IDENTITY(1,1),
	IdMedico INT FOREIGN KEY REFERENCES Medico(IdMedico),
	IdEspecialidad INT FOREIGN KEY REFERENCES Especialidad(IdEspecialidad),
    IdTurnoTrabajo INT FOREIGN KEY REFERENCES TurnoTrabajo(IdTurnoTrabajo),
	CONSTRAINT Uniqe_Medico_Especialidad_TurnoTra UNIQUE (IdMedico, IdEspecialidad, IdTurnoTrabajo)
);

CREATE TABLE Turno(
	IdTurno INT PRIMARY KEY IDENTITY(1,1),
	Fecha DateTime NOT NULL,
	Hora Time NOT NULL,
	IdAgendaMedico INT FOREIGN KEY REFERENCES AgendaMedico(IdAgendaMedico),
	IdPaciente INT FOREIGN KEY REFERENCES Paciente(IdPaciente),
	IdEstadoTurno INT FOREIGN KEY REFERENCES EstadoTurno(IdEstadoTurno),
	Observacion VARCHAR(100) NOT NULL,
	Diagnostico VARCHAR(100) NULL
);

CREATE TABLE Perfil(
	IdPerfil INT PRIMARY KEY IDENTITY(1,1),
	Descripcion VARCHAR(100) NOT NULL
);

CREATE TABLE Usuario(
	IdUsuario INT PRIMARY KEY IDENTITY(1,1),
	Nombre VARCHAR(50) NOT NULL,
	Contrasenia VARCHAR(20) NOT NULL,
	IdPerfil INT FOREIGN KEY REFERENCES Perfil(IdPerfil),
	Estado BIT NOT NULL DEFAULT 1
);

