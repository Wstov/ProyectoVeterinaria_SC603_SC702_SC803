CREATE TABLE Carrito (
    CarritoID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    ProductoID UNIQUEIDENTIFIER NOT NULL,
    Cantidad INT NOT NULL,
    CantidadTotal INT NOT NULL,
    MontoTotal DECIMAL(25, 2) NOT NULL,
    Estado BIT NOT NULL DEFAULT 0,
    CodigoPromocionID UNIQUEIDENTIFIER,
    PersonaID UNIQUEIDENTIFIER NOT NULL,  -- Nueva FK entre Carrito y Personas
    FOREIGN KEY (ProductoID) REFERENCES Producto(ProductoID),
    FOREIGN KEY (CodigoPromocionID) REFERENCES CodigosPromocion(CodigoPromocionID),
    FOREIGN KEY (PersonaID) REFERENCES Personas(Id)
);
