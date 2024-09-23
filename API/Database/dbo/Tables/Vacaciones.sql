CREATE TABLE Vacaciones (
    VacacionesID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    EmpleadoID UNIQUEIDENTIFIER NOT NULL,
    FechaAcreditacion DATE NOT NULL,
    FechaSalida DATE NOT NULL,
    FechaRegreso DATE NOT NULL,
    DiasDisfrutados INT NOT NULL,
    DiasRestantes INT NOT NULL,
    FOREIGN KEY (EmpleadoID) REFERENCES Empleado(EmpleadoID)
);