
CREATE PROCEDURE Obtener_Usuario_MW
    @Correo varchar(100)
AS
BEGIN
    SET NOCOUNT ON;

	SELECT U.IdPersona, P.Correo, U.Hash
	FROM Usuarios U
	INNER JOIN Personas P ON U.IdPersona=P.Id
	WHERE @Correo=Correo;
END;