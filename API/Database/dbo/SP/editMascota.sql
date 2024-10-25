CREATE PROCEDURE editMascota
    @MascotaID UNIQUEIDENTIFIER,
    @NombreAnimal VARCHAR(100),
    @NombreMascota VARCHAR(100),
    @FechaNacimiento DATETIME,
    @Raza VARCHAR(100),
    @Genero VARCHAR(10),
    @Caracteristicas VARCHAR(MAX)
AS
BEGIN
    UPDATE Mascota
    SET NombreAnimal = @NombreAnimal,
        NombreMascota = @NombreMascota,
        FechaNacimiento = @FechaNacimiento,
        Raza = @Raza,
        Genero = @Genero,
        Caracteristicas = @Caracteristicas
    WHERE MascotaID = @MascotaID;
END;
GO