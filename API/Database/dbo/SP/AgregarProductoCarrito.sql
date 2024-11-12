CREATE PROCEDURE AgregarProductoCarrito
    @PersonaID UNIQUEIDENTIFIER,
    @ProductoID UNIQUEIDENTIFIER,
    @Cantidad INT
AS
BEGIN
    DECLARE @CarritoID UNIQUEIDENTIFIER;
    DECLARE @PrecioUnitario DECIMAL(25, 2);
    SELECT @CarritoID = CarritoID 
    FROM Carrito 
    WHERE PersonaID = @PersonaID AND Estado = 1;
    IF @CarritoID IS NULL
    BEGIN
        SET @CarritoID = NEWID();
        INSERT INTO Carrito (CarritoID, PersonaID, Estado)
        VALUES (@CarritoID, @PersonaID, 1);
    END
    SELECT @PrecioUnitario = Precio 
    FROM Producto 
    WHERE ProductoID = @ProductoID;
    IF EXISTS (SELECT 1 FROM DetallesCarrito WHERE CarritoID = @CarritoID AND ProductoID = @ProductoID)
    BEGIN
        UPDATE DetallesCarrito
        SET Cantidad = Cantidad + @Cantidad
        WHERE CarritoID = @CarritoID AND ProductoID = @ProductoID;
    END
    ELSE
    BEGIN
        INSERT INTO DetallesCarrito (CarritoID, ProductoID, Cantidad, PrecioUnitario)
        VALUES (@CarritoID, @ProductoID, @Cantidad, @PrecioUnitario);
    END
END;
