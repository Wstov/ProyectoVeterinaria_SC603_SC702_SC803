CREATE PROCEDURE addCita
    @FechaHora DATETIME,
    @Dueno VARCHAR(100),
    @MascotaID UNIQUEIDENTIFIER,  
    @Motivo VARCHAR(MAX),
    @VeterinarioAsignadoID UNIQUEIDENTIFIER,
    @PersonaID UNIQUEIDENTIFIER
AS
BEGIN
    INSERT INTO RegistroCitas (FechaHora, Dueno, MascotaID, Motivo, VeterinarioAsignadoID, PersonaID)
    VALUES (@FechaHora, @Dueno, @MascotaID, @Motivo, @VeterinarioAsignadoID, @PersonaID);
END;
