CREATE TABLE HistorialCompra (
    HistorialCompraID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    FacturaID UNIQUEIDENTIFIER NOT NULL,
    FechaRegistro DATETIME NOT NULL,  -- Unificación de Fecha y Hora
    FOREIGN KEY (FacturaID) REFERENCES Facturacion(FacturaID)
);
