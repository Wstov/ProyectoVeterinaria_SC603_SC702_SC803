﻿CREATE TABLE Gasto (
    GastoID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Monto DECIMAL(10, 2) NOT NULL,
    Detalle TEXT NOT NULL,
    FechaRegistro DATETIME NOT NULL
);