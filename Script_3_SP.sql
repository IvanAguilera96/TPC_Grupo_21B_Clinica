CREATE PROCEDURE SP_AsignarAgendaMedico
    @IdMedico INT,
    @IdEspecialidad INT,
    @DiaDeTrabajo VARCHAR(50),
    @HoraEntrada TIME,
    @HoraSalida TIME
AS
BEGIN

    BEGIN TRANSACTION;

    BEGIN TRY
        DECLARE @IdTurno INT;

        -- Primero busco si existe ese horario para esa la especialidad elegida
        SELECT @IdTurno = IdTurnoTrabajo FROM TurnoTrabajo 
                WHERE DiaDeTrabajo = @DiaDeTrabajo 
                      AND HoraEntrada = @HoraEntrada 
                      AND HoraSalida = @HoraSalida
                      AND IdEspecialidad = @IdEspecialidad;

        -- Si no existe, se inserta
        IF @IdTurno IS NULL
        BEGIN
            INSERT INTO TurnoTrabajo (HoraEntrada, HoraSalida, DiaDeTrabajo, IdEspecialidad)
            VALUES (@HoraEntrada, @HoraSalida, @DiaDeTrabajo, @IdEspecialidad);
            
            -- Recuperamos el ID recién generado
            SET @IdTurno = SCOPE_IDENTITY();
        END

        INSERT INTO AgendaMedico(IdMedico, IdEspecialidad, IdTurnoTrabajo)
        VALUES (@IdMedico, @IdEspecialidad, @IdTurno);

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Si hubo error, se cancela todo
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END