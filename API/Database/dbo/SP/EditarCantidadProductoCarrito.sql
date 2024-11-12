CREATE PROCEDURE EditarCantidadProductoCarrito
    @CarritoID UNIQUEIDENTIFIER,
    @ProductoID UNIQUEIDENTIFIER,
    @NuevaCantidad INT
AS
BEGIN
    UPDATE DetallesCarrito
    SET Cantidad = @NuevaCantidad
    WHERE CarritoID = @CarritoID AND ProductoID = @ProductoID;
END;
