
CREATE PROCEDURE [dbo].[AddPersona]
    @Nombre VARCHAR(50),
	@Apellido VARCHAR(150),
	@Direccion VARCHAR(200),
    @Telefono NVARCHAR(9),
    @Correo NVARCHAR(150)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Id UNIQUEIDENTIFIER;
    SET @Id = NEWID();

    BEGIN TRANSACTION;
    BEGIN TRY
        INSERT INTO Personas (Id, Nombre, Apellido, Direccion, Telefono, Correo)
		VALUES (@Id, @Nombre, @Apellido, @Direccion, @Telefono, @Correo);

		INSERT INTO Usuarios(IdPersona)
		VALUES (@Id);

		INSERT INTO UsuariosxRoles(IdRol, IdPersona)
		VALUES(2, @Id);

        COMMIT TRANSACTION;

    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;

        THROW;
    END CATCH
END