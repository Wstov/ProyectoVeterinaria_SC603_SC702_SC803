﻿
CREATE PROCEDURE EditRol
	@Id int,
	@Tipo VARCHAR(13)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRANSACTION;
    BEGIN TRY
        UPDATE Roles
			SET Tipo= @Tipo
		WHERE @Id=Id;

        COMMIT TRANSACTION;

    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;

        THROW;
    END CATCH
END