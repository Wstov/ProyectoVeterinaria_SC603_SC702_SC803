CREATE PROCEDURE addCitas
    @CodigoCita VARCHAR(50),
    @FechaHora DATETIME,
    @Dueño VARCHAR(100),
    @MascotaID UNIQUEIDENTIFIER,
    @Motivo VARCHAR(MAX),
    @VeterinarioAsignadoID UNIQUEIDENTIFIER
AS
BEGIN
    INSERT INTO RegistroCitas (CodigoCita, FechaHora, Dueño, MascotaID, Motivo, VeterinarioAsignadoID)
    VALUES (@CodigoCita, @FechaHora, @Dueño, @MascotaID, @Motivo, @VeterinarioAsignadoID);
END;

