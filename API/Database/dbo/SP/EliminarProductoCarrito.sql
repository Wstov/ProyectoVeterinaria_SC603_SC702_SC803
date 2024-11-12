CREATE PROCEDURE EliminarProductoCarrito
    @CarritoID UNIQUEIDENTIFIER,
    @ProductoID UNIQUEIDENTIFIER
AS
BEGIN
    DELETE FROM DetallesCarrito
    WHERE CarritoID = @CarritoID AND ProductoID = @ProductoID;
END;
