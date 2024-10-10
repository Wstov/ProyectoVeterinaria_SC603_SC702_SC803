﻿
CREATE PROCEDURE DeleteRolxUsuario
	@IdPersona UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRANSACTION;
    BEGIN TRY
        DELETE FROM UsuariosxRoles
			WHERE IdPersona=@IdPersona

        COMMIT TRANSACTION;

    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;

        THROW;
    END CATCH
END