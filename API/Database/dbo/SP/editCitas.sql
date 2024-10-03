CREATE PROCEDURE editCita
    @CitaID UNIQUEIDENTIFIER,
    @FechaHora DATETIME,
    @Dueno VARCHAR(100),
    @MascotaID UNIQUEIDENTIFIER,  
    @Motivo VARCHAR(MAX),
    @VeterinarioAsignadoID UNIQUEIDENTIFIER,
    @PersonaID UNIQUEIDENTIFIER
AS
BEGIN
    UPDATE RegistroCitas
    SET FechaHora = @FechaHora,
        Dueno = @Dueno,
        MascotaID = @MascotaID,
        Motivo = @Motivo,
        VeterinarioAsignadoID = @VeterinarioAsignadoID,
        PersonaID = @PersonaID
    WHERE CitaID = @CitaID;
END;
