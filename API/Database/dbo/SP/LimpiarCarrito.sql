CREATE PROCEDURE LimpiarCarrito
    @CarritoID UNIQUEIDENTIFIER
AS
BEGIN
    DELETE FROM DetallesCarrito
    WHERE CarritoID = @CarritoID;
END;
