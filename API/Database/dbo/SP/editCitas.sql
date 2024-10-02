CREATE PROCEDURE editCitas
    @CitaID UNIQUEIDENTIFIER,
    @CodigoCita VARCHAR(50),
    @FechaHora DATETIME,
    @Dueño VARCHAR(100),
    @MascotaID UNIQUEIDENTIFIER,
    @Motivo VARCHAR(MAX),
    @VeterinarioAsignadoID UNIQUEIDENTIFIER
AS
BEGIN
    UPDATE RegistroCitas
    SET CodigoCita = @CodigoCita,
        FechaHora = @FechaHora,
        Dueño = @Dueño,
        MascotaID = @MascotaID,
        Motivo = @Motivo,
        VeterinarioAsignadoID = @VeterinarioAsignadoID
    WHERE CitaID = @CitaID;
END;

