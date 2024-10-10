
CREATE PROCEDURE EditPersona
	@Id UNIQUEIDENTIFIER,
    @Nombre VARCHAR(50),
	@Apellido VARCHAR(150),
	@Direccion VARCHAR(200),
    @Telefono NVARCHAR(9),
    @Correo NVARCHAR(150)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRANSACTION;
    BEGIN TRY
        UPDATE Personas
			SET Nombre = @Nombre,
			Apellido=@Apellido,
			Direccion=@Direccion,
			Telefono = @Telefono,
			Correo=@Correo
		WHERE @Id=Id;

        COMMIT TRANSACTION;

    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;

        THROW;
    END CATCH
END