CREATE TABLE Consulta (
    ConsultaID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    ExpedienteID UNIQUEIDENTIFIER NOT NULL,
    Peso DECIMAL(5, 2) NOT NULL,
    Sintomas varchar(100) NOT NULL,
    Tratamiento varchar(100) NOT NULL,
    Costo DECIMAL(10, 2) NOT NULL,
    FechaVisita DATE NOT NULL,
    FOREIGN KEY (ExpedienteID) REFERENCES Expediente(ExpedienteID)
);