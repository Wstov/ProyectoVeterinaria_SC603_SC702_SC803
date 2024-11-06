CREATE PROCEDURE deleteExpediente
    @ExpedienteID UNIQUEIDENTIFIER
AS
BEGIN
    DELETE FROM Expediente
    WHERE ExpedienteID = @ExpedienteID;
END;
GO