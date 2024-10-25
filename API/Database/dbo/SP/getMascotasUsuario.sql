CREATE PROCEDURE getMascotasUsuario
    @UsuarioID UNIQUEIDENTIFIER
AS
BEGIN
    SELECT * FROM Mascota WHERE UsuarioID = @UsuarioID;
END;
GO