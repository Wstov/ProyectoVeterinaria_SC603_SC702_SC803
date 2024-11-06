CREATE PROCEDURE getExpediente
    @ExpedienteID UNIQUEIDENTIFIER
AS
BEGIN
    SELECT *
    FROM Expediente
    WHERE ExpedienteID = @ExpedienteID;
END;
GO