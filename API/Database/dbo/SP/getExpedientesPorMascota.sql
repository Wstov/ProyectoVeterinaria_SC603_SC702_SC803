CREATE PROCEDURE getExpedientesPorMascota
    @MascotaID UNIQUEIDENTIFIER
AS
BEGIN
    SELECT *
    FROM Expediente
    WHERE MascotaID = @MascotaID;
END;
GO
