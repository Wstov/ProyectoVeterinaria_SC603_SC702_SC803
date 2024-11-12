CREATE TABLE DetallesCarrito (
    DetalleID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    CarritoID UNIQUEIDENTIFIER NOT NULL,
    ProductoID UNIQUEIDENTIFIER NOT NULL,
    Cantidad INT NOT NULL,
    PrecioUnitario DECIMAL(25, 2) NOT NULL,
    MontoTotal AS (Cantidad * PrecioUnitario) PERSISTED,
    FOREIGN KEY (CarritoID) REFERENCES Carrito(CarritoID),
    FOREIGN KEY (ProductoID) REFERENCES Producto(ProductoID)
);
