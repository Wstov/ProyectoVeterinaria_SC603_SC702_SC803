CREATE TABLE Carrito (
    CarritoID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    ProductoID UNIQUEIDENTIFIER,
    Cantidad INT NOT NULL,
    CantidadTotal INT NOT NULL,
    MontoTotal DECIMAL(10, 2) NOT NULL,
    CodigoPromocionID UNIQUEIDENTIFIER,
    FOREIGN KEY (ProductoID) REFERENCES Productos(ProductoID),
    FOREIGN KEY (CodigoPromocionID) REFERENCES CodigosPromocion(CodigoPromocionID)
);
