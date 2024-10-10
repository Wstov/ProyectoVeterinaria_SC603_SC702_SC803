
CREATE PROCEDURE [dbo].[GetPersona]
	@Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
        SELECT Id, Nombre, Apellido, Direccion, Telefono, Correo
		FROM Personas
		WHERE Id=@Id;
END