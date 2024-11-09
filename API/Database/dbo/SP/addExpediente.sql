CREATE PROCEDURE addExpediente
    @MascotaID UNIQUEIDENTIFIER,
    @Diagnostico VARCHAR(200)
AS
BEGIN
    INSERT INTO Expediente (MascotaID, Diagnostico, FechaActu)
    VALUES (@MascotaID, @Diagnostico, GETDATE()); 
END;
GO
