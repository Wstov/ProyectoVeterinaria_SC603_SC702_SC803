CREATE PROCEDURE EditRolxUsuario
    @Id int,
	@IdPersona UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRANSACTION;
    BEGIN TRY
        UPDATE  UsuariosxRoles
        SET IdRol= @Id
		WHERE IdPersona=@IdPersona;

        COMMIT TRANSACTION;

    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;

        THROW;
    END CATCH
END