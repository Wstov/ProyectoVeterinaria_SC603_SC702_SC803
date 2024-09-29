CREATE TABLE RegistroCitas (
    CitaID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    CodigoCita VARCHAR(50) NOT NULL,
    FechaHora DATETIME NOT NULL,  -- Unificación de Fecha y Hora
    Dueño VARCHAR(100) NOT NULL,
    MascotaID UNIQUEIDENTIFIER NOT NULL,   -- Nueva FK entre Mascota y Registro de Citas
    Motivo VARCHAR(MAX) NOT NULL,
    VeterinarioAsignadoID UNIQUEIDENTIFIER NOT NULL,
    FOREIGN KEY (VeterinarioAsignadoID) REFERENCES Empleado(EmpleadoID),
    FOREIGN KEY (MascotaID) REFERENCES Mascota(MascotaID)  -- Nueva FK
);
