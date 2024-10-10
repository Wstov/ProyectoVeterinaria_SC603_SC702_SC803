
CREATE Procedure  RolesxUsuario
@IdPersona UNIQUEIDENTIFIER
AS
BEGIN

SELECT R.Id, R.Tipo, RU.IdPersona
FROM UsuariosxRoles RU
INNER JOIN Roles R ON R.Id=RU.IdRol
WHERE RU.IdPersona=@IdPersona;

END