
CREATE PROCEDURE Obtener_RolesxUsuario
    @Correo varchar(100)
AS
BEGIN
    SET NOCOUNT ON;

	SELECT R.Id, R.Tipo
	FROM Roles R
	INNER JOIN UsuariosxRoles UR ON UR.IdRol=R.Id
	INNER JOIN Usuarios U ON U.IdPersona= UR.IdPersona
	INNER JOIN Personas P ON P.Id=U.IdPersona
	WHERE @Correo=P.Correo;
END;