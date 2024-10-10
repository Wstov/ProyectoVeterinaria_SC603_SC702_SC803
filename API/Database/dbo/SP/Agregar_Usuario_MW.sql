
CREATE PROCEDURE [dbo].[Agregar_Usuario_MW]
    @Hash varchar(max),
	@Nombre VARCHAR(50),
	@Apellido VARCHAR(150),
	@Direccion VARCHAR(200),
	@Telefono VARCHAR(9),
	@Correo VARCHAR(150)
AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @IdPersona UNIQUEIDENTIFIER;
    SET @IdPersona = NEWID();

    BEGIN TRY
        BEGIN TRANSACTION;

		INSERT INTO Personas(Id, Nombre, Apellido, Direccion, Telefono, Correo)
		VALUES(@IdPersona, @Nombre, @Apellido, @Direccion, @Telefono, @Correo);

        INSERT INTO Usuarios (IdPersona, Hash)
        VALUES (@IdPersona, @Hash);

		INSERT INTO UsuariosxRoles(IdPersona, IdRol)
        VALUES (@IdPersona, 2);

		SELECT @IdPersona;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;