CREATE TABLE Capacitaciones (
    CapacitacionID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    EmpleadoID UNIQUEIDENTIFIER NOT NULL,
    NombreCapacitacion VARCHAR(100) NOT NULL,
    Duracion INT NOT NULL,
    Detalle TEXT NOT NULL,
    Costo DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (EmpleadoID) REFERENCES Empleado(EmpleadoID)
);