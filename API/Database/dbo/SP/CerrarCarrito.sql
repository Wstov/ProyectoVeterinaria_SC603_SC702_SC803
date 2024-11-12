CREATE PROCEDURE CerrarCarrito
    @CarritoID UNIQUEIDENTIFIER
AS
BEGIN
    UPDATE Carrito
    SET Estado = 0
    WHERE CarritoID = @CarritoID;
END;
