CREATE PROCEDURE editExpediente
    @ExpedienteID UNIQUEIDENTIFIER,
    @MascotaID UNIQUEIDENTIFIER,
    @Diagnostico VARCHAR(200)
AS
BEGIN
    UPDATE Expediente
    SET MascotaID = @MascotaID,
        Diagnostico = @Diagnostico
    WHERE ExpedienteID = @ExpedienteID;
END;
GO