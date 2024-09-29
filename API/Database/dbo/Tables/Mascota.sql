CREATE TABLE Mascota (
    MascotaID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    UsuarioID UNIQUEIDENTIFIER NOT NULL,
    NombreAnimal VARCHAR(100) NOT NULL,
    NombreMascota VARCHAR(100) NOT NULL,
    FechaNacimiento DATETIME NOT NULL,  -- Unificación de Fecha y Hora
    Raza VARCHAR(100) NOT NULL,
    Genero VARCHAR(10) NOT NULL,
    Características VARCHAR(MAX) NOT NULL,
    FOREIGN KEY (UsuarioID) REFERENCES Personas(Id)
);
