CREATE TABLE CodigosPromocion (
    CodigoPromocionID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    CodigoPromocion VARCHAR(50) NOT NULL,
    PorcentajeDescuento DECIMAL(5, 2) NOT NULL
);