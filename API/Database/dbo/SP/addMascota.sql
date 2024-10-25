CREATE PROCEDURE addMascota
    @UsuarioID UNIQUEIDENTIFIER,
    @NombreAnimal VARCHAR(100),
    @NombreMascota VARCHAR(100),
    @FechaNacimiento DATETIME,
    @Raza VARCHAR(100),
    @Genero VARCHAR(10),
    @Caracteristicas VARCHAR(MAX)
AS
BEGIN
    INSERT INTO Mascota (UsuarioID, NombreAnimal, NombreMascota, FechaNacimiento, Raza, Genero, Caracteristicas)
    VALUES (@UsuarioID, @NombreAnimal, @NombreMascota, @FechaNacimiento, @Raza, @Genero, @Caracteristicas);
END;
GO
