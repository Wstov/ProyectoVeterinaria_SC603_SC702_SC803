﻿CREATE TABLE HistorialCompra (
    HistorialCompraID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    FacturaID UNIQUEIDENTIFIER NOT NULL,
    FechaRegistro DATE NOT NULL,
    FOREIGN KEY (FacturaID) REFERENCES Facturacion(FacturaID)
);