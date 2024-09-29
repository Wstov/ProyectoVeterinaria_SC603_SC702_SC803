CREATE TABLE Campania (
    CampaniaID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    EmpleadoID UNIQUEIDENTIFIER NOT NULL,
    NombreCampania VARCHAR(100) NOT NULL,
    Proposito VARCHAR(MAX) NOT NULL,
    FechaInicio DATETIME NOT NULL,  -- Unificación de Fecha y Hora
    FechaFin DATETIME NOT NULL,     -- Unificación de Fecha y Hora
    CostoCampania DECIMAL(25, 2) NOT NULL,
    FOREIGN KEY (EmpleadoID) REFERENCES Empleado(EmpleadoID)
);
