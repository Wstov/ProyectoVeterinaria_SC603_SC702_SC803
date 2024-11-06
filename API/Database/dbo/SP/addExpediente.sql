CREATE PROCEDURE addExpediente
    @MascotaID UNIQUEIDENTIFIER,
    @Diagnostico VARCHAR(200)
AS
BEGIN
    INSERT INTO Expediente (MascotaID, Diagnostico)
    VALUES (@MascotaID, @Diagnostico);
END;
GO