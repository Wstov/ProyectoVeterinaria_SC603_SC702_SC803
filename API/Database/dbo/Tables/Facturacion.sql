﻿CREATE TABLE Facturacion (
    FacturaID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    UsuarioID UNIQUEIDENTIFIER NOT NULL,
    CarritoID UNIQUEIDENTIFIER NOT NULL,
    MetodoPago VARCHAR(50) NOT NULL,
    FOREIGN KEY (UsuarioID) REFERENCES Personas(Id),
    FOREIGN KEY (CarritoID) REFERENCES Carrito(CarritoID)
);
