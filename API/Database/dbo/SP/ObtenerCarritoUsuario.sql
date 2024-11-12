CREATE PROCEDURE ObtenerCarritoUsuario
    @PersonaID UNIQUEIDENTIFIER
AS
BEGIN
    SELECT 
        c.CarritoID,
        dc.ProductoID,
        p.Nombre AS NombreProducto,
        dc.Cantidad,
        dc.PrecioUnitario,
        dc.MontoTotal
    FROM Carrito c
    INNER JOIN DetallesCarrito dc ON c.CarritoID = dc.CarritoID
    INNER JOIN Producto p ON dc.ProductoID = p.ProductoID
    WHERE c.PersonaID = @PersonaID AND c.Estado = 1;
END;
