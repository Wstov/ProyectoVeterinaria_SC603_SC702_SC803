CREATE PROCEDURE getCitas
    @PersonaID UNIQUEIDENTIFIER
AS
BEGIN
    SELECT * 
    FROM RegistroCitas
    WHERE PersonaID = @PersonaID;
END;
